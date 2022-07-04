using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

/// <summary>
/// 行为节点：攻击
/// </summary>
public class PlayerAttackAction : ActionBase
{
    private float _attackInterval = 2;
    private float _lastAttackTime;
    private Player _player;

    public override void OnEnter()
    {
        base.OnEnter();

        _player = _owner as Player;
        string msg = "Attack Enemy";
        _player.SetText(msg);
    }

    public override ResultType DoAction()
    {
        if (null == _player.Enemy)
        {
            return ResultType.Fail;
        }

        _player.SubEnergy(0.05f);

        if (Time.realtimeSinceStartup - _lastAttackTime <= _attackInterval)
        {
            return ResultType.Running;
        }
        _lastAttackTime = Time.realtimeSinceStartup;

        BulletData bulletData = new BulletData();
        bulletData.startPos = _player.Position;
        bulletData.target = _player.Enemy.transform;
        bulletData.speed = 5f;
        bulletData.damage = 5;
        BulletManager.GetInstance().AddBullet(bulletData);

        return ResultType.Success;
    }

    public override void OnExit()
    {
        base.OnExit();
    }


}
