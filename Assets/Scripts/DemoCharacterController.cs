using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoCharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;

    public int side = 1;


    [Header("Interact")]
    public GameObject interactTips;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public float jumpHeight = 1f; // 跳跃的高度
    public float jumpTime = 0.5f; // 跳跃的时间

    private bool isJumping = false; // 标记角色是否正在跳跃
    private Vector3 originalPosition; // 角色原始位置



    private bool canInteract = false;
    private GameObject interactObject;
    public void SetInteractObject(GameObject obj)
    {
        // print("SetInteractObject");
        interactObject = obj;
        canInteract = true;
        interactTips.SetActive(true);
    }
    public void ClearInteractObject()
    {
        interactObject = null;
        canInteract = false;
        interactTips.SetActive(false);
    }

    private void CheckInteract()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactObject.GetComponent<InteractObjBase>().Interact();
            }
        }else
        {
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        var dir = new Vector2(x, y);

        Walk(dir);

        if (x > 0)
        {
            side = 1;
            spriteRenderer.flipX = false;
            animator.SetBool("isRun", true);
        }
        if (x < 0)
        {
            side = -1;
            spriteRenderer.flipX = true;
            animator.SetBool("isRun", true);
        }
        if (x == 0)
        {
            animator.SetBool("isRun", false);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            originalPosition = transform.position;
            StartCoroutine(Jump());
        }
        CheckInteract();
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }


    IEnumerator Jump()
    {
        isJumping = true;
        float timer = 0;

        while (timer <= jumpTime)
        {
            timer += Time.deltaTime;

            Vector3 newPosition = originalPosition;
            float y = Mathf.Sin((Mathf.PI / jumpTime) * timer);
            newPosition.y += jumpHeight * y;

            //当下落时才进行跳板的检测
            if (timer > jumpTime / 2)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, 0.5f, LayerMask.GetMask("Ground"));
                foreach (RaycastHit2D hit in hits)
                {
                    // print("无敌");
                    if (hit.collider != null)
                    {
                        Debug.Log("Ground Detected!");
                        timer = 2 * jumpTime;//遇到跳板时，把跳跃中止。

                    }
                }
            }

            // 更新角色的位置
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);


            yield return null;
        }

        // 重置角色的位置和跳跃状态
        // transform.position = originalPosition;
        isJumping = false;
    }

}
