using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class BehaviorData
{
    private static Dictionary<string, BehaviorTreeData> _behaviorDic = new Dictionary<string, BehaviorTreeData>();

    public static BehaviorTreeData GetBehaviorInfo(string handleFile)
    {
        Debug.LogError("GetBehaviourInfo:" + handleFile);
        if (_behaviorDic.TryGetValue(handleFile, out BehaviorTreeData behaviorTreeData))
        {
            return behaviorTreeData;
        }

        return behaviorTreeData;
    }
}