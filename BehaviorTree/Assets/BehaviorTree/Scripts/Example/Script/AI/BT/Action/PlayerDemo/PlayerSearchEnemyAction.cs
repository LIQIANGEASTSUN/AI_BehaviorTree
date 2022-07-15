using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// 行为节点：搜索敌人
/// </summary>
public class PlayerSearchEnemyAction : ActionBase
{
    private Player _player;

    public override void OnEnter()
    {
        base.OnEnter();
        _player = _owner as Player;
    }

    public override ResultType DoAction()
    {
        bool result = SearchEnemy();

        _player.BTBase.UpdateParameter(BTConstant.HasEneny, result);

        if (!result)
        {
            return ResultType.Fail;
        }
        return ResultType.Success;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private bool SearchEnemy()
    {
        Npc enemy = _player.Enemy;
        if (null != enemy)
        {
            return true;
        }

        GameObject npc = GameObject.Find("Npc");
        if (!npc)
        {
            return false; 
        }

        enemy = npc.GetComponent<Npc>();
        _player.Enemy = enemy;

        return true;
    }
}
