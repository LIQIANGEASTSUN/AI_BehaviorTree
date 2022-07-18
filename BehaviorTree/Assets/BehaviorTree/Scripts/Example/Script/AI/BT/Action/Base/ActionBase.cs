using BehaviorTree;

/// <summary>
/// Custom Action node extends ActionBase
/// </summary>
public abstract class ActionBase : NodeAction, IBTActionOwner
{
    protected ISprite _owner = null;

    public virtual void SetOwner(ISprite owner)
    {
        _owner = owner;
    }

    public virtual ISprite GetOwner()
    {
        return _owner;
    }

    public override ResultType DoAction()
    {
        return ResultType.Success;
    }

}
