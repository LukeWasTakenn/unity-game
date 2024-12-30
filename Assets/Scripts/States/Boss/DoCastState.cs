using UnityEngine;

public class DoCastState : BossBaseState
{
    public DoCastState(Boss boss, string animationName) : base(boss, animationName)
    {
    }

    public override void Enter()
    {
        boss.animator.SetTrigger("cast_finish");
        boss.castTimer = boss.castCooldown;
    }
    public override void PhysicsUpdate()
    {
        boss.rb.linearVelocity = Vector2.zero;
    }
}