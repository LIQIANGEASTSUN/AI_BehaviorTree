using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    public class ConfigFileSaveSubTree
    {

        public void SaveSubTree(string subTreeConfigName, int subTreeNodeId)
        {
            UnityEngine.Debug.LogError("SaveSubTree:" + subTreeConfigName + "     " + subTreeNodeId);

            NodeValue subTreeNode = BehaviorDataController.Instance.GetNode(subTreeNodeId);
            if (null == subTreeNode || subTreeNode.NodeType != (int)NODE_TYPE.SUB_TREE)
            {
                return;
            }
            if (subTreeNode.subTreeType != (int)SUB_TREE_TYPE.NORMAL)
            {
                return;
            }

            BehaviorTreeData subTreeData = new BehaviorTreeData();
            subTreeData.fileName = subTreeConfigName;

            List<NodeValue> nodeList = BehaviorDataController.Instance.FindChild(BehaviorDataController.Instance.BehaviorTreeData, subTreeNodeId);

            List<NodeValue> newNodeList = new List<NodeValue>();
            for (int i = 0; i < nodeList.Count; ++i)
            {
                NodeValue childNode = nodeList[i];
                NodeValue newNodeValue = childNode.Clone();

                if (newNodeValue.subTreeEntry)
                {
                    newNodeValue.isRootNode = true;
                    newNodeValue.parentNodeID = -1;
                    subTreeData.rootNodeId = newNodeValue.id;
                }
                newNodeValue.parentSubTreeNodeId = -1;

                subTreeData.nodeList.Add(newNodeValue);
            }

            for (int i = 0; i < BehaviorDataController.Instance.BehaviorTreeData.parameterList.Count; ++i)
            {
                NodeParameter parameter = BehaviorDataController.Instance.BehaviorTreeData.parameterList[i];
                subTreeData.parameterList.Add(parameter.Clone());
            }

            subTreeData = DataNodeIdStandardTool.StandardID(subTreeData);

            ConfigFileSave configFileSave = new ConfigFileSave();
            configFileSave.SaveFile(subTreeConfigName, subTreeData);
        }

    }
}
