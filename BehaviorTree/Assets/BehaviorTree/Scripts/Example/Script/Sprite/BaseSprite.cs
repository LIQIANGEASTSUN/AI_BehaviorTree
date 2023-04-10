using UnityEngine;

public abstract class BaseSprite : IBTOwner
{
    protected int _spriteId;
    // BehaviorTree entity
    protected BTConcrete _bt;

    // Get Behavior Tree entity
    public BTBase BTBase
    {
        get { return _bt; }
    }

    protected GameObject _gameObject;

    public virtual void Init(Vector3 position)
    {
        // Initialize the BehaviorTree instance
        string aiConfig = AIConfigFile();
        _bt = new BTConcrete(this, long.MaxValue, aiConfig);
    }

    public virtual void Update()
    {
        if (null != BTBase)
        {
            BTBase.Update();
        }
    }

    public int SpriteID
    {
        get { return _spriteId; }
        set { _spriteId = value; }
    }

    protected abstract string AIConfigFile();

    /// <summary>
    /// Check whether the AI can be executed. The AI needs to be paused in some cases
    /// For example, opening the UI panel requires pausing the AI
    /// </summary>
    /// <returns></returns>
    public virtual bool CanRunningBT()
    {
        return true;
    }

    public virtual void Release()
    {
        if (null != BTBase) {
            BTBase.Exit();
        }
    }

    public GameObject SpriteGameObject
    {
        get { return _gameObject; }
    }

}