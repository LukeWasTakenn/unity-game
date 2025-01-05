using SmallHedge.SoundManager;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public float moveSpeed = 4f;
    
    public Transform playerTransform;
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D collision;
    public Player player;
    public GameObject spell;
    private AudioSource audioSource;
    
    public Transform attackCollider;
    public float attackRange = 1.5f;
    public LayerMask playerLayer;
    public float startAttackDistance = 2f;

    public BossBaseState currentState;
    public GetToPlayerState getToPlayerState;
    public StartAttackState startAttackState;
    public DoAttackState doAttackState;
    public StartCastState startCastState;
    public DoCastState doCastState;
    public BossDeathState deathState;
    
    public bool isAttacking = false;

    public float startCastDistance = 10f;
    public float castCooldown = 3f;
    public float castTimer = 0f;
        
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = playerTransform.GetComponent<Player>();
        collision = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        
        getToPlayerState = new GetToPlayerState(this, "walk");
        startAttackState = new StartAttackState(this, "start_attack");
        doAttackState = new DoAttackState(this, "attack_finish");
        startCastState = new StartCastState(this, "start_cast");
        doCastState = new DoCastState(this, "cast_finish");
        deathState = new BossDeathState(this, "die");
        
        currentState = getToPlayerState;
        currentState.Enter();
    }
    
    public void SwitchState(BossBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
    // Update is called once per frame
    void Update()
    {
        castTimer -= Time.deltaTime;
        
        currentState.LogicUpdate();
        
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
    }

    void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    public void AttackDealDamage()
    {
        var hit = Physics2D.OverlapCircle(attackCollider.position, attackRange, playerLayer);
        
        if (!hit) return;

        player.TakeDamage(50);
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }

    public void OnFinishCastAnimationEnd()
    {
        SwitchState(getToPlayerState);
        
        var pos = new Vector2(player.transform.position.x, player.transform.position.y + 1.9f);
        
        Instantiate(spell, pos, Quaternion.identity);
        SoundManager.PlaySound(SoundType.Spell, null, 0.7f);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCollider.position, attackRange);
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

    public void OnDeath()
    {
        animator.SetTrigger("die");
        SwitchState(deathState);
    }

    public void PlayFootstepSound()
    {
        SoundManager.PlaySound(SoundType.EnemyFootstep, audioSource, 0.3f);
    }

    public void OnTakeDamage()
    {
        return;
    }
}
