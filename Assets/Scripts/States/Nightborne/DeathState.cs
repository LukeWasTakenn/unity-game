using UnityEngine;

public class DeathState : EnemyBaseState
{
    public DeathState(Enemy enemy, string animationName) : base(enemy, animationName) {}

    public override void Enter()
    {
        enemy.collision.enabled = false;
    }

    public override void PhysicsUpdate()
    {
        enemy.rb.linearVelocity = Vector2.zero;
    }
}