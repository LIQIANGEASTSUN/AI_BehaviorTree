﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using LitJson;
using CommonUtils;

public class BehaviorData
{

    #region  BehaviorTree
    private Dictionary<string, BehaviorTreeData> _behaviorDic = new Dictionary<string, BehaviorTreeData>();
    public void LoadData(byte[] loadByteData)
    {
        AnalysisBin.AnalysisData(loadByteData, Analysis);
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

    public BehaviorTreeData GetBehaviorInfo(string handleFile)
    {
        BehaviorTreeData behaviorTreeData = null;
        if (_behaviorDic.TryGetValue(handleFile, out behaviorTreeData))
        {
            return behaviorTreeData;
        }

        return behaviorTreeData;
    }
    #endregion
}

