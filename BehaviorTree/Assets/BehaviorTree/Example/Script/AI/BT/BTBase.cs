using System;
using System.Collections.Generic;
using BehaviorTree;

public abstract class BTBase : IAIPerformer
{
    protected BehaviorTreeEntity _btEntity = null;

    protected void SetData(long aiFunction, BehaviorTreeData data)
    {
        _btEntity = new BehaviorTreeEntity(aiFunction, data);
    }

    protected virtual void Init(ISprite owner)
    {
        InitNode(owner, _btEntity.RootNode);
    }

    private void InitNode(ISprite owner, NodeBase nodeBase)
    {
        BTActionOwnerTool.NodeSetOwner(owner, nodeBase);
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

    public virtual void Release()
    {
        _btEntity.Release();
    }

}
