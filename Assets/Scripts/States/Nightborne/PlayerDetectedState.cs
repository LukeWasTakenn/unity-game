using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerDetectedState : EnemyBaseState
{
    public PlayerDetectedState(Enemy enemy, string animationName) : base(enemy, animationName) {}

    private float nextAttackTime = 0f;
    private float attackRate = 0.5f;

    public override void Enter()
    {
        base.Enter();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        var playerHit = enemy.CheckForPlayer();
        
        if (!playerHit)
        {
            enemy.SwitchState(enemy.patrolState);
            return;
        }
        
        if (Time.time >= nextAttackTime)
        {
            Attack(playerHit);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.rb.linearVelocity = Vector2.zero;
    }

    private void Attack(Collider2D playerHit)
    {
        var player = playerHit.gameObject.GetComponent<Player>();

        if (player.isDead) return;
        
        enemy.animator.SetTrigger("attack");
        nextAttackTime = Time.time + 1 / attackRate;
        
        player.TakeDamage(34);
    }
}
