using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Transform player;
    private float changeDirectionTime = 2f;
    private float directionTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        PickRandomDirection();
        directionTimer = changeDirectionTime;
    }

    void Update()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f)
        {
            PickRandomDirection();
            directionTimer = changeDirectionTime;
        }
        HandleMovement();
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
        }
    }

    void PickRandomDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: movement = Vector2.up; break;
            case 1: movement = Vector2.down; break;
            case 2: movement = Vector2.left; break;
            case 3: movement = Vector2.right; break;
        }
    }

    void HandleMovement()
    {
        rb.velocity = movement * moveSpeed;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Implement your attack logic here, e.g., reduce player health
            Debug.Log("Monster attacks player for " + attackDamage + " damage!");
            lastAttackTime = Time.time;
        }
    }
} 