using BehaviorTree;

/// <summary>
/// Custom Action node extends ActionBase
/// </summary>
public abstract class ActionBase : NodeAction, IBTActionOwner
{
    protected IBTOwner _owner = null;

    public virtual void SetOwner(IBTOwner owner)
    {
        _owner = owner;
    }

    public virtual IBTOwner GetOwner()
    {
        return _owner;
    }

    public override ResultType DoAction()
    {
        return ResultType.Success;
    }

}
