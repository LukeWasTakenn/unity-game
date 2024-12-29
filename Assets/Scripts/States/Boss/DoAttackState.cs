using UnityEngine;

public class DoAttackState : BossBaseState
{
    public int damage = 50;
    
    public DoAttackState(Boss boss, string animationName) : base(boss, animationName)
    {
    }

    public override void Enter()
    {
        Debug.Log("attack");
        boss.animator.SetTrigger("attack_finish");
        boss.isAttacking = true;
    }

    public override void PhysicsUpdate()
    {
        if (boss.isAttacking)
            return;
        
        var distance = Vector2.Distance(boss.playerTransform.transform.position, boss.transform.position);

        if (distance > boss.startAttackDistance)
        {
            boss.SwitchState(boss.getToPlayerState);
            return;
        }
        
        boss.SwitchState(boss.startAttackState);
        return;
    }
}
