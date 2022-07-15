using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// 补充能量
/// </summary>
public class PlayerReplenishEnergyAction : ActionBase
{
    private Player _player;

    public override void OnEnter()
    {
        base.OnEnter();
        _player = _owner as Player;

        string msg = "ReplenishEnergy";
        _player.SetText(msg);
    }

    public override ResultType DoAction()
    {
        float value = EnergyStation.GetInstance().Execute();
        bool isDone = _player.ReplenishEnergy(value);

        ResultType resultType = isDone ? ResultType.Success : ResultType.Running;

        return resultType;
    }

    public override void OnExit()
    {
        base.OnExit();
        _player.BTBase.UpdateParameter(BTConstant.Energy, _player.Energy());
    }
}
