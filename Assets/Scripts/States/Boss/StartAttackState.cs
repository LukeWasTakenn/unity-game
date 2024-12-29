using UnityEngine;

public class StartAttackState : BossBaseState
{
    private float windUpTime = 1f;
    private float timer;
    
    public StartAttackState(Boss boss, string animationName) : base(boss, animationName)
    {
    }

    public override void Enter()
    {
        if (boss.player.isDead)
        {
            boss.animator.Play("Idle");
            return;
        }
        
        boss.animator.SetTrigger("attack_start");
        timer = windUpTime;
    }

    public override void LogicUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            boss.SwitchState(boss.doAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        boss.rb.linearVelocity = Vector2.zero;
    }
}
