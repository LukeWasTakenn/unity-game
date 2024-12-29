using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 4f;
    
    public Transform playerTransform;
    public Rigidbody2D rb;
    public Animator animator;
    public Player player;
    
    public Transform attackCollider;
    public float attackRange = 1.5f;
    public LayerMask playerLayer;
    public float startAttackDistance = 2f;

    public BossBaseState currentState;
    public GetToPlayerState getToPlayerState;
    public StartAttackState startAttackState;
    public DoAttackState doAttackState;
    
    public bool isAttacking = false;
        
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = playerTransform.GetComponent<Player>();
        
        getToPlayerState = new GetToPlayerState(this, "walk");
        startAttackState = new StartAttackState(this, "start_attack");
        doAttackState = new DoAttackState(this, "attack_finish");
        
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCollider.position, attackRange);
    }
}
