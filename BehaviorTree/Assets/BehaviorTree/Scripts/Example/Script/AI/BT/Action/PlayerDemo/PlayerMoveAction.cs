using UnityEngine;
using BehaviorTree;
using GraphicTree;

/// <summary>
/// Action node : Move
/// </summary>
public class PlayerMoveAction : ActionBase
{
    private IMove _iMove;
    private TargetTypeEnum _targetType = TargetTypeEnum.ENEMY;
    private Player _player;

    public override void OnEnter()
    {
        base.OnEnter();
        _player = _owner as Player;
        GetIMove();

        string msg = string.Format("GoTo:{0}", System.Enum.GetName(typeof(TargetTypeEnum), _targetType));
        _player.SetText(msg);
    }

    public override ResultType DoAction()
    {
        if (null == _iMove)
        {
            return ResultType.Fail;
        }

        ResultType resultType = Move();

        return resultType;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void GetIMove()
    {
        bool result = false;
        for (int i = 0; i < _parameterList.Count; ++i)
        {
            NodeParameter parameter = _parameterList[i];
            if (parameter.parameterName.CompareTo(BTConstant.TargetType) == 0)
            {
                _targetType = (TargetTypeEnum)parameter.intValue;
                result = true;
            }
        }

        if (!result)
        {
            return;
        }

        _iMove = _player.GetIMove(_targetType);
    }

    private ResultType Move()
    {
        if (null == _iMove)
        {
            return ResultType.Fail;   
        }

        float speed = 0;
        Vector3 targetPos = Vector3.zero;
        float distance = 0;
        _iMove.Move(ref speed, ref targetPos, ref distance);
        if (Vector3.Distance(_player.Position, targetPos) <= distance)
        {
            return ResultType.Success;
        }

        _player.Position = Vector3.MoveTowards(_player.Position, targetPos, speed * Time.deltaTime);

        _player.SpriteGameObject.transform.LookAt(targetPos);
        return ResultType.Running;
    }

}
