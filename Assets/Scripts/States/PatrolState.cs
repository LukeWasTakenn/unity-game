using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public PatrolState(Enemy enemy, string animationName) : base(enemy, animationName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.CheckForPlayer())
            enemy.SwitchState(enemy.playerDetectedState);
        
        if (enemy.CheckForObstacles())
            Rotate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        enemy.rb.linearVelocity = new Vector2(enemy.facingRight ? enemy.moveSpeed : -enemy.moveSpeed, enemy.rb.linearVelocity.y);
    }
    
    private void Rotate()
    {
        enemy.transform.Rotate(0, 180, 0);
        enemy.facingRight = !enemy.facingRight;
    }
}
