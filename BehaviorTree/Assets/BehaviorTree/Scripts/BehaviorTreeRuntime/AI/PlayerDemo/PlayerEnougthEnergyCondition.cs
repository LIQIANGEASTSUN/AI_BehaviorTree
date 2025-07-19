using BehaviorTree;
using GraphicTree;

/// <summary>
/// Condition node : Enougth Energy?
/// </summary>
public class PlayerEnougthEnergyCondition : ConditionBase
{
    private float _energyMin = 0;
    private Player _player;

    public override void OnEnter()
    {
        base.OnEnter();
        _player = _owner as Player;
        GetEnergyMin();
    }

    public override ResultType Condition()
    {
        ResultType resultType = ResultType.Fail;

        float energy = 0;
        bool result = _player.BTConcrete.GetParameterValue(BTConstant.Energy, ref energy);
        if (!result)
        {
            return resultType;
        }

        resultType = energy > _energyMin ? ResultType.Success : ResultType.Fail;

        return resultType;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void GetEnergyMin()
    {
        for (int i = 0; i < _parameterList.Count; ++i)
        {
            NodeParameter parameter = _parameterList[i];
            if (parameter.parameterName.CompareTo(BTConstant.EnergyMin) == 0)
            {
                _energyMin = parameter.floatValue;
            }
        }
    }

}
