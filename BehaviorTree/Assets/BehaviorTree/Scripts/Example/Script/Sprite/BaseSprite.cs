using UnityEngine;

public abstract class BaseSprite : ISprite, IBTNeedUpdate
{
    protected int _spriteId;
    // BehaviorTree 实例
    protected BTConcrete _bt;

    // 获取Behavior Tree 实例
    public BTBase BTBase
    {
        get { return _bt; }
    }

    protected GameObject _gameObject;

    public virtual void Init(Vector3 position)
    {
        // 初始化 BehaviorTree 实例，将 (BaseSprite)this传递进去
        string aiConfig = AIConfigFile();
        _bt = new BTConcrete(this, long.MaxValue, aiConfig);
    }

    public virtual void Update()
    {

    }

    public int SpriteID
    {
        get { return _spriteId; }
        set { _spriteId = value; }
    }

    protected abstract string AIConfigFile();

    // 判断能否执行 AI，当一些情况需要暂停 AI，如打开UI面板需要暂停AI 
    public virtual bool CanRunningBT()
    {
        return true;
    }

    public virtual void Release()
    {

    }

    public GameObject SpriteGameObject
    {
        get { return _gameObject; }
    }

}