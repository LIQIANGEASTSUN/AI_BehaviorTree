using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorDataRemoveUnUseParameter
    {
        private HashSet<string> useHash = new HashSet<string>();
        public void RemoveParameter(BehaviorTreeData behaviorData)
        {
            useHash.Clear();
            GetUseHash(behaviorData);

            for (int i = behaviorData.parameterList.Count - 1; i >= 0; --i)
            {
                NodeParameter nodeParameter = behaviorData.parameterList[i];
                if (!useHash.Contains(nodeParameter.parameterName))
                {
                    behaviorData.parameterList.RemoveAt(i);
                }
            }
        }

        public void GetUseHash(BehaviorTreeData behaviorData)
        {
            HashSet<string> useHash = new HashSet<string>();

            foreach(var nodeValue in behaviorData.nodeList)
            {
                GetNodeUseParameter(nodeValue);
            }
        }

        public void GetNodeUseParameter(NodeValue nodeValue)
        {
            foreach (var parameter in nodeValue.parameterList)
            {
                AddParameterName(parameter.parameterName);
            }

            foreach (var conditionGroup in nodeValue.conditionGroupList)
            {
                foreach (var parameterName in conditionGroup.parameterList)
                {
                    AddParameterName(parameterName);
                }
            }
        }

        private void AddParameterName(string parameterName)
        {
            if (!useHash.Contains(parameterName))
            {
                useHash.Add(parameterName);
            }
        }

    }
}