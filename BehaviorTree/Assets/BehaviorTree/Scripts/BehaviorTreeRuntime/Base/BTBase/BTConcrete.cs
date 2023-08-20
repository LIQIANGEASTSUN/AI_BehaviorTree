using BehaviorTree;

public class BTConcrete : BTBase
{
    private IBTOwner _owner;
    private BehaviorTreeData _data;
    private BehaviorTreeDebug _behaviorTreeDebug;

    public BTConcrete(IBTOwner owner, long aiFunction, string aiConfig)
    {
        _owner = owner;
        _data = DataCenter.behaviorData.GetBehaviorInfo(aiConfig);
        SetData(aiFunction, _data);
        Init(_owner);
    }

    protected override void Init(IBTOwner owner)
    {
        base.Init(owner);
        if (null == _behaviorTreeDebug && _owner.SpriteGameObject)
        {
            _behaviorTreeDebug = _owner.SpriteGameObject.AddComponent<BehaviorTreeDebug>();
            _behaviorTreeDebug.SetEntity(_data, _btEntity);
        }
    }

    public override void Release()
    {
        base.Release();
        _owner = null;
        _data = null;
        _behaviorTreeDebug = null;
    }
}
