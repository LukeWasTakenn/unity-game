public class BossBaseState
{
    protected Boss boss;
    protected string animationName;

    public BossBaseState(Boss boss, string animationName)
    {
        this.boss = boss;
        this.animationName = animationName;
    }
    
    public virtual void Enter() {}

    public virtual void Exit() {}

    public virtual void LogicUpdate() {}

    public virtual void PhysicsUpdate() {}
}
