using UnityEngine;

public class EnemyBaseState
{
    protected Enemy enemy;
    protected string animationName;

    public EnemyBaseState(Enemy enemy, string animationName)
    {
        this.enemy = enemy;
        this.animationName = animationName;
    }

    public virtual void Enter() {}

    public virtual void Exit() {}

    public virtual void LogicUpdate() {}

    public virtual void PhysicsUpdate() {}
}
