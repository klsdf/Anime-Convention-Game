using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 角色控制器，处理玩家的移动、跳跃和交互等行为
/// </summary>
public class DemoCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("角色移动速度")]
    public float moveSpeed = 10f;
    [Tooltip("角色朝向（1为右，-1为左）")]
    public int facingDirection = 1;

    [Header("Jump Settings")]
    [Tooltip("跳跃高度")]
    public float jumpHeight = 1f;
    [Tooltip("跳跃持续时间")]
    public float jumpDuration = 0.5f;

    [Header("Interaction")]
    [Tooltip("交互提示UI")]
    public GameObject interactionTips;

    [Header("Audio")]
    [Tooltip("是否启用脚步声")]
    [SerializeField] private bool enableFootstepSound = true;
    private AudioSource footstepAudioSource;

    // 组件引用
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Camera mainCamera;

    // 状态标记
    private bool isJumping = false;
    private bool isMoving = false;
    private bool canInteract = false;
    private Vector3 jumpStartPosition;
    private GameObject currentInteractable;

    private void Start()
    {
        // 获取组件引用
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        // 初始化脚步声
        InitializeFootstepAudio();
    }

    private void InitializeFootstepAudio()
    {
        footstepAudioSource = gameObject.AddComponent<AudioSource>();
        footstepAudioSource.loop = true;
        footstepAudioSource.playOnAwake = false;
        var footstepClip = AudioPlayerController.Instance.GetAudioClip(SoundType.Footstep);
        if (footstepClip != null)
        {
            footstepAudioSource.clip = footstepClip;
        }
    }

    /// <summary>
    /// 设置可交互对象
    /// </summary>
    public void SetInteractObject(GameObject obj)
    {
        currentInteractable = obj;
        canInteract = true;
        interactionTips.SetActive(true);
    }

    /// <summary>
    /// 清除当前交互对象
    /// </summary>
    public void ClearInteractObject()
    {
        currentInteractable = null;
        canInteract = false;
        interactionTips.SetActive(false);
    }

    private void Update()
    {
        // 获取输入
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);

        // 检测是否在移动
        isMoving = (moveDirection != Vector2.zero) && !isJumping;
        
        // 处理移动
        HandleMovement(moveDirection);
        // 处理动画
        UpdateAnimation(horizontalInput);
        // 处理脚步声
        UpdateFootstepSound();
        // 处理交互
        HandleInteraction();

        // 处理跳跃
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            jumpStartPosition = transform.position;
            StartCoroutine(PerformJump());
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        rigidBody.velocity = direction * moveSpeed;
    }

    private void UpdateAnimation(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            facingDirection = 1;
            spriteRenderer.flipX = false;
            animator.SetBool("isRun", true);
        }
        else if (horizontalInput < 0)
        {
            facingDirection = -1;
            spriteRenderer.flipX = true;
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }

    private void UpdateFootstepSound()
    {
        if (!enableFootstepSound) return;
        
        if (isMoving && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
        else if (!isMoving && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }
    }

    private void HandleInteraction()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.GetComponent<InteractObjBase>().Interact();
        }
    }

    private System.Collections.IEnumerator PerformJump()
    {
        isJumping = true;
        float elapsedTime = 0;

        while (elapsedTime <= jumpDuration)
        {
            elapsedTime += Time.deltaTime;

            // 计算跳跃高度
            float jumpProgress = (Mathf.PI / jumpDuration) * elapsedTime;
            float heightOffset = jumpHeight * Mathf.Sin(jumpProgress);
            Vector3 newPosition = jumpStartPosition;
            newPosition.y += heightOffset;

            // 检测地面碰撞
            if (elapsedTime > jumpDuration / 2)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, 0.5f, LayerMask.GetMask("Ground"));
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        elapsedTime = jumpDuration * 2; // 提前结束跳跃
                        break;
                    }
                }
            }

            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
            yield return null;
        }

        isJumping = false;
    }
}
