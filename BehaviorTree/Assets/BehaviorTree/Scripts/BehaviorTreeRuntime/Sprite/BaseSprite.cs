using UnityEngine;

public abstract class BaseSprite : IBTOwner
{
    public int SpriteId {get; set;}

    // BehaviorTree entity
    protected BTConcrete _bt;

    // Get Behavior Tree entity
    public BTConcrete BTConcrete
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
        if (null != BTConcrete)
        {
            BTConcrete.Update();
        }
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
        if (null != BTConcrete) {
            BTConcrete.Exit();
        }
    }

    public GameObject SpriteGameObject
    {
        get { return _gameObject; }
    }
}