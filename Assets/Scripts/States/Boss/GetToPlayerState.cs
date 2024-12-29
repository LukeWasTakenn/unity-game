using System;
using UnityEngine;

public class GetToPlayerState : BossBaseState
{
    public GetToPlayerState(Boss boss, string animationName) : base(boss, animationName) {}

    private float distance;

    public override void Enter()
    {
        boss.animator.Play("Walk");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        distance = Vector2.Distance(boss.playerTransform.transform.position, boss.transform.position);

        if (distance <= boss.startAttackDistance)
        {
            boss.SwitchState(boss.startAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();


        if (distance > boss.startAttackDistance)
        {
            boss.rb.linearVelocity = new Vector2(Mathf.Sign((boss.playerTransform.position.x - boss.transform.position.x)) * boss.moveSpeed, boss.rb.linearVelocity.y);
        }
    }
    
}
