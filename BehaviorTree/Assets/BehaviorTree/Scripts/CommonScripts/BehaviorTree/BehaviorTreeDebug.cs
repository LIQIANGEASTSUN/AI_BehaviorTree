using UnityEngine;
using BehaviorTree;

/// <summary>
/// use in the Debug Mode
/// in the runtime, select a GameObject with BehaviorTreeDebug added to it in Unity's Hierarchy panel, 
/// then open Window->BehaviorTree: views node execution in real time
/// </summary>
public class BehaviorTreeDebug : MonoBehaviour
{
    private BehaviorTreeData _behaviorTreeData;
    private BehaviorTreeEntity _entity;

    public delegate void BehaviorTreeDebugEvent(BehaviorTreeData behaviorTreeData, BehaviorTreeEntity entity);
    private static BehaviorTreeDebugEvent _treeDebugEvent;

    public void OnSelect(bool value)
    {
        if (value)
        {
            if (null != _treeDebugEvent)
            {
                _treeDebugEvent(_behaviorTreeData, _entity);
            }
        }
    }

    public void SetEntity(BehaviorTreeData behaviorTreeData, BehaviorTreeEntity entity)
    {
        _behaviorTreeData = behaviorTreeData;
        _entity = entity;
    }

    public static void AddTreeDebugEvent(BehaviorTreeDebugEvent treeDebugEvent)
    {
        _treeDebugEvent += treeDebugEvent;
    }

    public static void RemoveTreeDebugEvent(BehaviorTreeDebugEvent treeDebugEvent)
    {
        _treeDebugEvent -= treeDebugEvent;
    }
}
