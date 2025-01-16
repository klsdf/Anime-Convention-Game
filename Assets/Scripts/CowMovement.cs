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
        if (interactionSign != null)
        {
            interactionSign.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the cow's position each frame.
    /// </summary>
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
            if (distance <= interactionDistance)
            {
                interactionSign.SetActive(true);
                // Position the interaction sign above the cow
                interactionSign.transform.position = transform.position + new Vector3(0, 1, 0);
            }
            else
            {
                interactionSign.SetActive(false);
            }
        }
    }
} 