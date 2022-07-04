using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class NumberActionDo4 : ActionBase
{
    private NumberSprite _numberSprite;

    public override void OnEnter()
    {
        base.OnEnter();
        _numberSprite = _owner as NumberSprite;
    }

    public override ResultType DoAction()
    {
        Debug.Log("Do4:");
        return ResultType.Success;
    }

}
