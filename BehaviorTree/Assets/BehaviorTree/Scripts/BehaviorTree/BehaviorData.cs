using System.Collections.Generic;
using BehaviorTree;

public class BehaviorData
{
    private static Dictionary<string, BehaviorTreeData> _behaviorDic = new Dictionary<string, BehaviorTreeData>();
    public static void AddData(BehaviorTreeData data)
    {
        foreach(var node in data.nodeList)
        {
            data.nodeDic.Add(node.id, node);
        }
        _behaviorDic.Add(data.fileName, data);
    }

    public static BehaviorTreeData GetData(string handleFile)
    {
        if (_behaviorDic.TryGetValue(handleFile, out BehaviorTreeData behaviorTreeData))
        {
            return behaviorTreeData;
        }
        return behaviorTreeData;
    }
}