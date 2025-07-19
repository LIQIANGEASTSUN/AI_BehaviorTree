using System.Collections.Generic;
using BehaviorTree;
using LitJson;
using UnityEngine;

public class BehaviorData
{
    private static Dictionary<string, BehaviorTreeData> _behaviorDic = new Dictionary<string, BehaviorTreeData>();

    #region  BehaviorTree
    public void LoadData(byte[] loadByteData)
    {
        ConfigDataGroup dataGroup = AnalysisData.Analysis(loadByteData);
        foreach(var data in dataGroup.dataList)
        {
            Analysis(data.byteDatas);
        }
    }

    private void Analysis(byte[] byteData)
    {
        string content = System.Text.Encoding.Default.GetString(byteData);
        BehaviorTreeData behaviorTreeData = JsonMapper.ToObject<BehaviorTreeData>(content);
        _behaviorDic[behaviorTreeData.fileName] = behaviorTreeData;

        for (int i = 0; i < behaviorTreeData.nodeList.Count; ++i)
        {
            NodeValue nodeValue = behaviorTreeData.nodeList[i];
            behaviorTreeData.nodeDic.Add(nodeValue.id, nodeValue);
        }
    }

    public static BehaviorTreeData GetBehaviorInfo(string handleFile)
    {
        if (_behaviorDic.TryGetValue(handleFile, out BehaviorTreeData behaviorTreeData))
        {
            return behaviorTreeData;
        }

        return behaviorTreeData;
    }
    #endregion

}

