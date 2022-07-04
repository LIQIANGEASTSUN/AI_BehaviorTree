using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

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
