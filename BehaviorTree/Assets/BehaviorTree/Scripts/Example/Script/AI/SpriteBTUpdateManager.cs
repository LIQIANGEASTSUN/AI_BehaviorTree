using System.Collections.Generic;
using BehaviorTree;

public interface IBTNeedUpdate
{
    BTBase BTBase { get; }
    bool CanRunningBT();
}

//只做驱动BT用，不能做别的
public class SpriteBTUpdateManager
{
    //这两个list里的项，永远对应。不用Dictionary是考虑到遍历时Remove问题
    private List<int> _AiIDList = new List<int>();
    private List<IBTNeedUpdate> _AiList = new List<IBTNeedUpdate>();
    private HashSet<int> _removeHash = new HashSet<int>();

    public SpriteBTUpdateManager()
    {
        BehaviorAnalysis.GetInstance().SetLoadConfigEvent(DataCenter.behaviorData.GetBehaviorInfo);
    }

    public void Release()
    {
        _AiIDList.Clear();
        _AiList.Clear();
        _removeHash.Clear();
    }

    public void Update()
    {
        for (int i = _AiList.Count - 1; i >= 0; --i)
        {
            if (null == _AiList[i] || null == _AiList[i].BTBase)
            {
                continue;
            }
            if (_AiList[i].CanRunningBT())
            {
                _AiList[i].BTBase.Update();
            }
            int spriteId = _AiIDList[i];
            if (_removeHash.Contains(spriteId))
            {
                _AiIDList.RemoveAt(i);
                _AiList.RemoveAt(i);
                _removeHash.Remove(spriteId);
                continue;
            }
        }
    }

    public void AddSprite(int spriteId, IBTNeedUpdate bTNeedUpdate)
    {
        if (_AiIDList.Contains(spriteId))
        {
            return;
        }

        if (bTNeedUpdate != null)
        {
            _AiList.Add(bTNeedUpdate);
            _AiIDList.Add(spriteId);
        }
    }

    public void RemoveSprite(int spriteID)
    {
        _removeHash.Add(spriteID);
    }
}