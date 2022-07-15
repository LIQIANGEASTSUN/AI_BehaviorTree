using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// 行为节点：巡逻
/// </summary>
public class PlayerPatrolAction : ActionBase
{
    private float _lastTime;
    private float _interval = 3;

    private Player _player;

    public override void OnEnter()
    {
        base.OnEnter();
        _player = _owner as Player;
        string msg = "Patrol";
        _player.SetText(msg);
    }

    public override ResultType DoAction()
    {
        if (Time.realtimeSinceStartup - _lastTime > _interval)
        {
            _player.ChangePatrolPos();
            _lastTime = Time.realtimeSinceStartup;
        }

        _player.SubEnergy(0.03f);
        return ResultType.Success;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
