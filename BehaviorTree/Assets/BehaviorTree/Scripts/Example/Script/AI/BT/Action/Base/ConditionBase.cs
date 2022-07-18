﻿using BehaviorTree;

/// <summary>
/// Custom Condition node extends ActionBase
/// </summary>
public abstract class ConditionBase : NodeCondition, IBTActionOwner
{
    protected ISprite _owner = null;

    public ISprite GetOwner()
    {
        return _owner;
    }

    public void SetOwner(ISprite owner)
    {
        _owner = owner;
    }

    public override ResultType Condition()
    {
        return ResultType.Success;
    }
}
