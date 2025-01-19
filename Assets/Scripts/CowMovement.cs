using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the random horizontal movement of a cow within a specified range in a 2D scene.
/// </summary>
public class CowMovement : MonoBehaviour
{
    /// <summary>
    /// The speed at which the cow moves.
    /// </summary>
    public float moveSpeed = 2f;

    /// <summary>
    /// The range within which the cow can move.
    /// </summary>
    public float moveRange = 5f;

    /// <summary>
    /// The time interval between direction changes.
    /// </summary>
    public float changeDirectionInterval = 3f;

    /// <summary>
    /// The original position of the cow.
    /// </summary>
    private Vector2 originalPosition;

    /// <summary>
    /// The current target position for the cow to move towards.
    /// </summary>
    private Vector2 targetPosition;

    /// <summary>
    /// The sprite renderer for flipping the cow's sprite.
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// The interaction sign GameObject.
    /// </summary>
    public GameObject interactionSign;

    /// <summary>
    /// The distance at which the interaction sign appears.
    /// </summary>
    public float interactionDistance = 2f;

    /// <summary>
    /// The main character's transform.
    /// </summary>
    private bool hasTriggered = false;

    /// <summary>
    /// 判断物体不会重复出现
    /// </summary>
    public Transform prefabSpawnPoint;

    /// <summary>
    /// 物体生成
    /// </summary>

    public Transform mainCharacter;

    /// <summary>
    /// Initializes the cow's movement.
    /// </summary>
    private void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeDirectionRoutine());

        // Ensure the interaction sign is initially hidden
        //if (interactionSign != null)
        //{
         //   interactionSign.SetActive(false);
       // }
    }
   
    private void Update()
    {
        MoveCow();
        CheckForInteraction();
        
    }

    /// <summary>
    /// Moves the cow towards the target position.
    /// </summary>
    private void MoveCow()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the cow reaches the target position, choose a new target
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChooseNewTargetPosition();
        }

        // Flip the sprite based on movement direction
        if (targetPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = false; // Facing left
        }
        else if (targetPosition.x > transform.position.x)
        {
            spriteRenderer.flipX = true; // Facing right
        }
    }

    /// <summary>
    /// Chooses a new random target position within the move range on the x-axis.
    /// </summary>
    private void ChooseNewTargetPosition()
    {
        float randomX = Random.Range(-moveRange, moveRange);
        targetPosition = originalPosition + new Vector2(randomX, 0);
    }

    /// <summary>
    /// Coroutine to change the cow's direction at regular intervals.
    /// </summary>
    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            ChooseNewTargetPosition();
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    /// <summary>
    /// Checks if the main character is within interaction distance and shows the interaction sign.
    /// </summary>
    private void CheckForInteraction()
    {
        if (mainCharacter != null && interactionSign != null)
        {
            float distance = Vector2.Distance(transform.position, mainCharacter.position);

            if (distance <= interactionDistance && !hasTriggered)
            {
                GenerateInteractionPrefab();
                hasTriggered = true; // 确保只触发一次
            }
            else if (distance > interactionDistance)
            {
                hasTriggered = false; // 重置标志，允许再次触发
            }
        }
    }
    private void GenerateInteractionPrefab()
    {
        // 在指定位置生成 Prefab
        GameObject instance = Instantiate(interactionSign, prefabSpawnPoint.position, Quaternion.identity);

        // 将生成的物体设置为 prefabSpawnPoint 的子物体
        instance.transform.SetParent(prefabSpawnPoint);

        // 停留两秒后销毁
        Destroy(instance, 2f);

        Debug.Log("Generated a prefab at position: " + prefabSpawnPoint.position);
    }
} 