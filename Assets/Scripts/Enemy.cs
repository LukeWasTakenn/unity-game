using SmallHedge.SoundManager;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("States")]
    public EnemyBaseState currentState;
    public PatrolState patrolState;
    public PlayerDetectedState playerDetectedState;
    public DeathState deathState;
 
    [Header("Ledge detection")]
    public Transform ledgeDetector;
    public LayerMask groundLayer;
    public float obstacleRayDistance = 1.5f;

    public bool facingRight = true;
    
    [Header("Movement")]
    public Rigidbody2D rb;
    public BoxCollider2D collision;
    public float moveSpeed = 5f;
    public Animator animator;
    
    [Header("Player detection")]
    public float playerDetectionRange = 1.3f;
    public Transform playerDetector;
    public LayerMask playerLayer;
    
    public EnemyHealth health;
    
    public AudioSource audioSource;
    
    public void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        health = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        
        patrolState = new PatrolState(this, "patrol");
        playerDetectedState = new PlayerDetectedState(this, "playerDetected");
        deathState = new DeathState(this, "death");
        
        currentState = patrolState;
        currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.LogicUpdate();
    }
    
    void FixedUpdate()
    {
        currentState.PhysicsUpdate();
        
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
    }

    public void SwitchState(EnemyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public Collider2D CheckForPlayer()
    {
        var hit = Physics2D.OverlapCircle(playerDetector.position, playerDetectionRange, playerLayer);
        return hit;
    }

    public bool CheckForObstacles()
    {
        var hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, obstacleRayDistance, groundLayer);
        return hit.collider is null;
    }

    public void RemoveSelf()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ledgeDetector.position, playerDetectionRange);
    }

    public void OnAttackAnimationFinish()
    {
        SoundManager.PlaySound(SoundType.EnemyAttack);
        var playerCollider = CheckForPlayer();

        if (!playerCollider) return;
        
        var player = playerCollider.transform.GetComponent<Player>();

        if (player.isDead) return;
        
        player.TakeDamage(34);
    }

    public void OnTakeDamage()
    {
        SoundManager.PlaySound(SoundType.EnemyHit, null, 0.3f);
        if (currentState != playerDetectedState)
        {
            animator.SetTrigger("hurt");
        }
    }

    public void OnDeath()
    {
        animator.SetBool("isDead", true);
        SwitchState(deathState);
    }

    public void PlayExplosionSound()
    {
        SoundManager.PlaySound(SoundType.EnemyExplode, null, 0.2f);
    }
    
    public void PlayFootstepSound()
    {
        SoundManager.PlaySound(SoundType.EnemyFootstep, audioSource, 0.3f);
    }
}
