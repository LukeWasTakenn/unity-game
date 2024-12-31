using UnityEngine;

public class BossDeathState : BossBaseState
{
    public BossDeathState(Boss boss, string animationName) : base(boss, animationName)
    {
    }
    
    public override void Enter()
    {
        boss.collision.enabled = false;
    }

    public override void PhysicsUpdate()
    {
        boss.rb.linearVelocity = Vector2.zero;
    }
}