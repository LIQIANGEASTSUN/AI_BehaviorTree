using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseSprite
{
    public override void Init(Vector3 position)
    {
        CreateSelf(position);
        base.Init(position);

        // 更新环境变量 BTConstant.IsSurvial 的值 为 true
        BTBase.UpdateParameter(BTConstant.IsSurvial, true);
        // 更新环境变量 BTConstant.Energy 的值为 Energy() 返回的结果
        BTBase.UpdateParameter(BTConstant.Energy, Energy());
    }

    protected override string AIConfigFile()
    {
        // Behavior配置文件名
        return "Player";
    }

    public void SetText(string msg)
    {
        _textMesh.text = msg;
    }

    #region Energy
    public float _energy;
    public float _energyFull = 100;
    public Npc _enemyNpc = null;

    public float Energy()
    {
        return _energy;
    }

    public bool ReplenishEnergy(float vlaue)
    {
        _energy += vlaue;
        _energy = Mathf.Min(_energy, _energyFull);
        return _energy >= _energyFull;
    }

    public void SubEnergy(float value)
    {
        _energy -= value;
        BTBase.UpdateParameter(BTConstant.Energy, Energy());
    }

    #endregion

    #region Enemy
    public Npc Enemy
    {
        get { return _enemyNpc; }
        set { _enemyNpc = value; }
    }

    private IMove _moveFollowEnemy;
    private IMove MoveEnemy()
    {
        if (null == _moveFollowEnemy)
        {
            _moveFollowEnemy = new SpriteFollowEnemy(this);
        }
        return _moveFollowEnemy;
    }
    #endregion

    #region Patrol
    private PlayerPatrol _patrol;
    private IMove PatrolMove()
    {
        if (null == _patrol)
        {
            _patrol = new PlayerPatrol();
        }
        return _patrol;
    }

    public void ChangePatrolPos()
    {
        (PatrolMove() as PlayerPatrol).ResetPos();
    }
    #endregion

    public Vector3 Position
    {
        get
        {
            return _gameObject.transform.position;
        }
        set
        {
            _gameObject.transform.position = value;
        }
    }

    public IMove GetIMove(TargetTypeEnum targetType)
    {
        IMove iMove = null;
        if (targetType == TargetTypeEnum.ENEMY)
        {
            iMove = MoveEnemy();
        }
        else if (targetType == TargetTypeEnum.ENERY_SUPPLY)
        {
            iMove = EnergyStation.GetInstance();
        }
        else if (targetType == TargetTypeEnum.PATROL)
        {
            iMove = PatrolMove();
        }

        return iMove;
    }

    private TextMesh _textMesh;
    private void CreateSelf(Vector3 position)
    {
        GameObject go = Resources.Load<GameObject>("Player");
        _gameObject = GameObject.Instantiate<GameObject>(go);
        _gameObject.name = "Player";
        _gameObject.transform.localScale = Vector3.one;
        _gameObject.transform.position = position;

        Transform textTr = _gameObject.transform.Find("Text");
        _textMesh = textTr.gameObject.GetComponent<TextMesh>();
    }
}
