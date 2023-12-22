using BehaviorTree;

/// <summary>
/// Custom Condition node extends ActionBase
/// </summary>
public abstract class ConditionBase : NodeCondition, IBTActionOwner
{
    protected IBTOwner _owner = null;

    public IBTOwner GetOwner()
    {
        return _owner;
    }

    public void SetOwner(IBTOwner owner)
    {
        _owner = owner;
    }

    public override ResultType Condition()
    {
        return ResultType.Success;
    }
}
