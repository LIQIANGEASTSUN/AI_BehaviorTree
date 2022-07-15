using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    public class BehaviorDrawModel
    {
        public BehaviorDrawModel()
        {   }

        public NodeValue GetCurrentSelectNode()
        {
            return BehaviorDataController.Instance.CurrentNode;
        }

        private List<NodeValue> GetNodeList()
        {
            return BehaviorDataController.Instance.NodeList;
        }

        public List<NodeValue> GetDrawNode()
        {
            List<NodeValue> nodeList = new List<NodeValue>();
            if (BehaviorDataController.Instance.CurrentOpenSubTree >= 0)
            {
                nodeList = GetSubTreeNode(BehaviorDataController.Instance.CurrentOpenSubTree);
            }
            else
            {
                nodeList = GetBaseNode();
            }

            CheckDrawNode(nodeList);

            return nodeList;
        }

        private List<NodeValue> GetBaseNode()
        {
            List<NodeValue> nodeList = new List<NodeValue>();
            List<NodeValue> allNodeList = GetNodeList();
            for (int i = 0; i < allNodeList.Count; ++i)
            {
                NodeValue nodeValue = allNodeList[i];
                if (nodeValue.parentSubTreeNodeId < 0)
                {
                    nodeList.Add(nodeValue);
                }
            }
            return nodeList;
        }

        private List<NodeValue> GetSubTreeNode(int currentOpenSubTreeId)
        {
            List<NodeValue> nodeList = new List<NodeValue>();

            NodeValue subTreeNode = BehaviorDataController.Instance.GetNode(currentOpenSubTreeId);
            if (null == subTreeNode)
            {
                return nodeList;
            }

            if (subTreeNode.subTreeType == (int)SUB_TREE_TYPE.NORMAL)
            {
                List<NodeValue> allNodeList = GetNodeList();
                for (int i = 0; i < allNodeList.Count; ++i)
                {
                    NodeValue nodeValue = allNodeList[i];
                    if (currentOpenSubTreeId >= 0 && nodeValue.parentSubTreeNodeId == currentOpenSubTreeId)
                    {
                        nodeList.Add(nodeValue);
                    }
                }
            }
            else if (subTreeNode.subTreeType == (int)SUB_TREE_TYPE.CONFIG)
            {
                ConfigFileLoad configFileLoad = new ConfigFileLoad();
                BehaviorTreeData data = configFileLoad.ReadFile(subTreeNode.subTreeConfig, false);
                if (null != data)
                {
                    nodeList.AddRange(data.nodeList);
                }
            }

            return nodeList;
        }

        private void CheckDrawNode(List<NodeValue> nodeList)
        {
            if (BehaviorDataController.Instance.RunTimeInvalidSubTreeHash.Count <= 0)
            {
                return;
            }

            for (int i = nodeList.Count - 1; i >= 0; --i)
            {
                NodeValue nodeValue = nodeList[i];
                if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
                {
                    if (BehaviorDataController.Instance.RunTimeInvalidSubTreeHash.Contains(nodeValue.id))
                    {
                        nodeList.RemoveAt(i);
                    }
                }
            }
        }

        public string[] GetOptionArr(Dictionary<string, int> dic)
        {
            List<string> optionList = new List<string>();
            optionList.Add("Base");
            dic["Base"] = -1;

            if (BehaviorDataController.Instance.CurrentOpenSubTree >= 0)
            {
                int nodeId = BehaviorDataController.Instance.CurrentOpenSubTree;
                NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
                while (null != nodeValue && nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
                {
                    string name = GetNodeName(nodeValue);
                    optionList.Insert(1, name);
                    dic[name] = nodeValue.id;

                    if (nodeValue.parentSubTreeNodeId <= 0)
                    {
                        break;
                    }
                    nodeValue = BehaviorDataController.Instance.GetNode(nodeValue.parentSubTreeNodeId);
                }
            }
            return optionList.ToArray();
        }

        private string GetNodeName(NodeValue nodeValue)
        {
            int nodeIndex = EnumNames.GetEnumIndex<NODE_TYPE>((NODE_TYPE)nodeValue.NodeType);
            string name = EnumNames.GetEnumName<NODE_TYPE>(nodeIndex);
            name = Localization.GetInstance().Format(name);
            return string.Format("{0}_{1}", name, nodeValue.id);
        }
    }

}
