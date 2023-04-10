using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class NumberActionDo1 : ActionBase
{
    private NumberSprite _numberSprite;

    public override void OnEnter()
    {
        base.OnEnter();
        _numberSprite = _owner as NumberSprite;
    }

    public override ResultType DoAction()
    {
        Debug.Log("Do1:");
        return ResultType.Success;
    }

}
