using BehaviorTree;
using System;

/// <summary>
/// Conditional parameters can be added dynamically as required
/// </summary>
public sealed class NodeConditionCustom : NodeCondition
{

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override ResultType Condition()
    {
        // Compares configured condition parameters
        bool result = _iconditionCheck.Condition(conditionParameter);
        ResultType resultType = result ? ResultType.Success : ResultType.Fail;
        return resultType;
    }

}