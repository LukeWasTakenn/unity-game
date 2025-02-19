using SmallHedge.SoundManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    
    [Header("Jumping")]
    public float jumpPower = 10f;

    public int maxJumps = 2;
    private int jumpsRemaining;
    
    private Rigidbody2D rb;
    private float horizontalMovement;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Attack")]
    public Transform attackPoint;
    public LayerMask enemyMask;

    private bool canAttack = true;
    private bool facingRight = true;
    
    [Header("Health")]
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead = false;

    [Header("Misc")] public GameObject pauseMenu;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Gravity();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;

        facingRight = horizontalMovement switch
        {
            > 0 when !facingRight => true,
            < 0 when facingRight => false,
            _ => facingRight
        };

        Flip();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining <= 0) return;
        
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpsRemaining--;
            animator.SetBool("isGrounded", false);
            animator.SetTrigger("jump");
            SoundManager.PlaySound(SoundType.Jump);
        }
        else if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            jumpsRemaining--;
            animator.SetBool("isGrounded", false);
            animator.SetTrigger("jump");
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack)
        {
            animator.SetTrigger("attack");
            SoundManager.PlaySound(SoundType.Attack);
            canAttack = false;
            Invoke("SetCanAttack", 0.3f);
            
            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, 1.3f, enemyMask);
            
            foreach (var target in hit)
            {
                var health = target.gameObject.GetComponent<EnemyHealth>();
                health.TakeDamage(35);
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        var hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.1f, 1f), 0, Vector2.zero);

        if (hits.Length <= 0) return;

        foreach (var hit in hits)
        {
            var interactable = hit.collider.gameObject.GetComponent<Interactable>();

            if (interactable)
            {
                interactable.Interact();
                return;
            }
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            GetComponent<PlayerInput>().enabled = false;
        }
    }

    private void SetCanAttack()
    {
        canAttack = true;
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0f, groundLayer))
        {
            jumpsRemaining = maxJumps;
            animator.SetBool("isGrounded", true);
        }
    }

    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.DrawWireSphere(attackPoint.position, 1.3f);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        
        if (currentHealth <= 0)
            currentHealth = 0;

        healthBar.SetHealth(currentHealth);
        
        if (currentHealth > 0) return;
        
        isDead = true;
        animator.SetTrigger("die");
        
        GetComponent<PlayerInput>().enabled = false;
        Invoke("DeathScene", 3f);
    }

    public void PlayFootstepSound()
    {
        SoundManager.PlaySound(SoundType.Footstep, null, 0.4f);
    }
    
    private void DeathScene()
    {
        GameManager.WineCollectedThisLife = 0;
        GameManager.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Scenes/PlayerDied");
    }
    
}
