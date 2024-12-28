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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!enemy.CheckForPlayer())
        {
            enemy.SwitchState(enemy.patrolState);
            return;
        }

        
        if (Time.time >= nextAttackTime)
        {
            enemy.animator.SetTrigger("attack");
            nextAttackTime = Time.time + 1 / attackRate;
            Debug.Log("Attack!");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.rb.linearVelocity = Vector2.zero;
    }
}
