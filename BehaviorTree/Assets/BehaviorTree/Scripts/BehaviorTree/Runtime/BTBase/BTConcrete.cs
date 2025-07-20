using BehaviorTree;

/// <summary>
/// 行为树 AI 实例
/// </summary>
public class BTConcrete : IAIPerformer
{
    private IBTOwner _owner;
    private BehaviorTreeData _data;
    protected BehaviorTreeEntity _btEntity = null;
    private BehaviorTreeDebug _behaviorTreeDebug;

    public BTConcrete(IBTOwner owner, long aiFunction, string aiConfig)
    {
        Init(owner, aiFunction, aiConfig);
    }

    protected void Init(IBTOwner owner, long aiFunction, string aiConfig)
    {
        _owner = owner;
        _data = BehaviorData.GetData(aiConfig);
        SetData(aiFunction, _data);
        BTActionOwnerTool.NodeSetOwner(owner, _btEntity.RootNode);
        if (null == _behaviorTreeDebug && _owner.SpriteGameObject)
        {
            _behaviorTreeDebug = _owner.SpriteGameObject.AddComponent<BehaviorTreeDebug>();
            _behaviorTreeDebug.SetEntity(_data, _btEntity);
        }
    }

    protected void SetData(long aiFunction, BehaviorTreeData data)
    {
        _btEntity = new BehaviorTreeEntity(aiFunction, data);
    }


    public void UpdateParameter(string name, bool para)
    {
        _btEntity.ConditionCheck.SetParameter(name, para);
    }

    public void UpdateParameter(string name, int para)
    {
        _btEntity.ConditionCheck.SetParameter(name, para);
    }

    public void UpdateParameter(string name, long para)
    {
        _btEntity.ConditionCheck.SetParameter(name, para);
    }

    public void UpdateParameter(string name, float para)
    {
        _btEntity.ConditionCheck.SetParameter(name, para);
    }

    public void UpdateParameter(string name, string para)
    {
        _btEntity.ConditionCheck.SetParameter(name, para);
    }

    public bool GetParameterValue(string parameterName, ref int value)
    {
        return _btEntity.ConditionCheck.GetParameterValue(parameterName, ref value);
    }

    public bool GetParameterValue(string parameterName, ref long value)
    {
        return _btEntity.ConditionCheck.GetParameterValue(parameterName, ref value);
    }

    public bool GetParameterValue(string parameterName, ref float value)
    {
        return _btEntity.ConditionCheck.GetParameterValue(parameterName, ref value);
    }

    public bool GetParameterValue(string parameterName, ref bool value)
    {
        return _btEntity.ConditionCheck.GetParameterValue(parameterName, ref value);
    }

    public bool GetParameterValue(string parameterName, ref string value)
    {
        return _btEntity.ConditionCheck.GetParameterValue(parameterName, ref value);
    }

    public virtual void Update()
    {
        if (_btEntity != null)
        {
            _btEntity.Execute();
        }
    }

    public virtual void Exit()
    {
        if (null != _btEntity)
        {
            _btEntity.Exit();
        }
    }

    public void Release()
    {
        _btEntity.Release();
        _owner = null;
        _data = null;
        _behaviorTreeDebug = null;
    }
}
