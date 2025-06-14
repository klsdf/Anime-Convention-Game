using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    [Header("基本设置")]
    public float moveSpeed = 2f;
    public float runSpeed = 4f;
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float chaseRange = 5f;
    public float attackCooldown = 1f;
    
    [Header("生命系统")]
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject healthBarPrefab; // 可选：用于显示血条
    public float deathDelay = 2f;      // 死亡后销毁的延迟时间
    public GameObject deathEffectPrefab; // 可选：死亡特效
    public AudioClip hurtSound;        // 受伤音效
    public AudioClip deathSound;       // 死亡音效
    
    [Header("行为控制")]
    public float idleTime = 2f;
    public float walkTime = 2f;
    public float changeDirectionTime = 2f;
    
    // 内部变量
    private float lastAttackTime;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;
    private float directionTimer;
    private float stateTimer;
    private bool isDead = false;
    private Transform healthBar;
    private AudioSource audioSource;
    
    // 状态控制
    private enum State { Idle, Walk, Run, Attack, Die }
    private State currentState = State.Idle;
    
    // Animator参数名称
    private readonly string IsWalking = "isWalking";
    private readonly string PlayerInChaseRange = "playerInChaseRange";
    private readonly string PlayerInAttackRange = "playerInAttackRange";
    private readonly string IsDead = "isDead";  // 新增死亡参数
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && (hurtSound != null || deathSound != null))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // 初始化生命值
        currentHealth = maxHealth;
        
        // 创建血条（如果提供了预制体）
        if (healthBarPrefab != null)
        {
            GameObject healthBarObj = Instantiate(healthBarPrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
            healthBarObj.transform.SetParent(transform);
            healthBar = healthBarObj.transform;
            UpdateHealthBar();
        }
        
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        // 初始化计时器
        directionTimer = changeDirectionTime;
        stateTimer = idleTime;
        
        // 确保动画器参数初始状态
        animator.SetBool(IsWalking, false);
        animator.SetBool(PlayerInChaseRange, false);
        animator.SetBool(PlayerInAttackRange, false);
        animator.SetBool(IsDead, false);
    }
    
    void Update()
    {
        // 如果已死亡，不执行其他逻辑
        if (isDead) return;
        
        // 优先检查玩家是否在范围内
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            // 检查是否在攻击范围内
            if (distanceToPlayer <= attackRange)
            {
                ChangeState(State.Attack);
                FaceTarget(player.position);
                return;
            }
            // 检查是否在追击范围内
            else if (distanceToPlayer <= chaseRange)
            {
                ChangeState(State.Run);
                ChasePlayer();
                return;
            }
        }
        
        // 如果玩家不在范围内，执行普通行为
        stateTimer -= Time.deltaTime;
        
        if (stateTimer <= 0f)
        {
            // 在idle和walk之间切换
            if (currentState == State.Idle)
            {
                ChangeState(State.Walk);
                PickRandomDirection();
                stateTimer = walkTime;
            }
            else if (currentState == State.Walk)
            {
                ChangeState(State.Idle);
                StopMoving();
                stateTimer = idleTime;
            }
        }
        
        // 行走时随机改变方向
        if (currentState == State.Walk)
        {
            directionTimer -= Time.deltaTime;
            if (directionTimer <= 0f)
            {
                PickRandomDirection();
                directionTimer = changeDirectionTime;
            }
            
            // 执行移动
            MoveInDirection();
        }
    }
    
    void ChangeState(State newState)
    {
        // 如果状态没变，不做任何事
        if (currentState == newState)
            return;
        
        // 离开当前状态的清理
        switch (currentState)
        {
            case State.Idle:
                // 无特殊清理
                break;
            case State.Walk:
            case State.Run:
                // 停止移动
                rb.velocity = Vector2.zero;
                break;
            case State.Attack:
                // 攻击结束
                break;
            case State.Die:
                // 不应该从死亡状态转换到其他状态
                return;
        }
        
        // 设置新状态
        currentState = newState;
        
        // 更新动画器参数
        animator.SetBool(IsWalking, currentState == State.Walk);
        animator.SetBool(PlayerInChaseRange, currentState == State.Run);
        animator.SetBool(PlayerInAttackRange, currentState == State.Attack);
        animator.SetBool(IsDead, currentState == State.Die);
        
        // 进入新状态的初始化
        switch (currentState)
        {
            case State.Idle:
                stateTimer = idleTime;
                break;
            case State.Walk:
                stateTimer = walkTime;
                break;
            case State.Run:
                // 无需特殊初始化
                break;
            case State.Attack:
                Attack();
                break;
            case State.Die:
                Die();
                break;
        }
    }
    
    void PickRandomDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: 
                movement = Vector2.up; 
                break;
            case 1: 
                movement = Vector2.down; 
                break;
            case 2: 
                movement = Vector2.left; 
                break;
            case 3: 
                movement = Vector2.right; 
                break;
        }
        
        // 面向移动方向
        FaceDirection(movement);
    }
    
    void MoveInDirection()
    {
        rb.velocity = movement * moveSpeed;
    }
    
    void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }
    
    void ChasePlayer()
    {
        if (player != null)
        {
            // 计算朝向玩家的方向
            Vector2 direction = (player.position - transform.position).normalized;
            
            // 朝玩家移动
            rb.velocity = direction * runSpeed;
            
            // 面向玩家
            FaceDirection(direction);
        }
    }
    
    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // 执行攻击
            Debug.Log("Monster attacks player for " + attackDamage + " damage!");
            
            // 检查玩家是否有可以接收伤害的组件
            if (player != null)
            {
                // 示例: 如果玩家有PlayerHealth组件
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
            }
            
            lastAttackTime = Time.time;
            
            // 攻击后短暂停留，然后返回到之前状态
            Invoke("ResetAfterAttack", attackCooldown);
        }
    }
    
    void ResetAfterAttack()
    {
        // 攻击后检查玩家位置，决定下一个状态
        if (player != null && !isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            if (distanceToPlayer <= chaseRange)
            {
                ChangeState(State.Run);
            }
            else
            {
                ChangeState(State.Idle);
            }
        }
        else if (!isDead)
        {
            ChangeState(State.Idle);
        }
    }
    
    void FaceDirection(Vector2 direction)
    {
        // 根据方向翻转Sprite
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(
                direction.x > 0 ? 1 : -1, 
                transform.localScale.y, 
                transform.localScale.z
            );
        }
    }
    
    void FaceTarget(Vector2 target)
    {
        // 面向目标
        Vector2 direction = target - (Vector2)transform.position;
        FaceDirection(direction);
    }
    
    // 生命系统功能
    
    // 受到伤害的公共方法
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;
        
        // 播放受伤音效
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }
        
        // 更新血条
        UpdateHealthBar();
        
        // 检查是否死亡
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            ChangeState(State.Die);
        }
        else
        {
            // 可以添加受伤动画或效果
            // animator.SetTrigger("Hurt");
        }
    }
    
    // 死亡处理
    void Die()
    {
        isDead = true;
        
        // 停止所有移动和行为
        StopMoving();
        
        // 禁用碰撞体（可选）
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        
        // 播放死亡音效
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        
        // 生成死亡特效（如果有）
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }
        
        // 延迟销毁对象
        Destroy(gameObject, deathDelay);
    }
    
    // 更新血条显示
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // 假设血条有一个子对象作为填充部分
            Transform fill = healthBar.Find("Fill");
            if (fill != null)
            {
                // 根据当前血量调整填充的宽度
                Vector3 scale = fill.localScale;
                scale.x = (float)currentHealth / maxHealth;
                fill.localScale = scale;
            }
        }
    }
    
    // 在Unity编辑器中可视化范围（辅助调试）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

// 为了完整性，这里添加一个简单的PlayerHealth类示例
// 您应该创建一个单独的脚本文件来实现这个类
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage! Current health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Player died!");
        // 实现玩家死亡逻辑
    }
}