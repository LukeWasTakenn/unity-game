using UnityEngine;

public class StartCastState : BossBaseState
{
    private float windUpTime = 2f;
    private float timer;
    
    public StartCastState(Boss boss, string animationName) : base(boss, animationName)
    {
    }

    public override void Enter()
    {
        if (boss.player.isDead)
        {
            boss.animator.Play("Idle");
            return;
        }
        
        boss.animator.SetTrigger("cast_start");
        timer = windUpTime;
    }
    
    public override void LogicUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            boss.SwitchState(boss.doCastState);
        }
    }

    
    public override void PhysicsUpdate()
    {
        boss.rb.linearVelocity = Vector2.zero;
    }
}