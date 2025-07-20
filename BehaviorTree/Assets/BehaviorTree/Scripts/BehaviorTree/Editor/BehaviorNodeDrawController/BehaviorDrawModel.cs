using System.Collections.Generic;

namespace BehaviorTree
{
    public class BehaviorDrawModel
    {
        public BehaviorDrawModel()
        {   }

        public NodeValue GetCurrentSelectNode()
        {
            return DataController.Instance.CurrentNode;
        }

        private List<NodeValue> GetNodeList()
        {
            return DataController.Instance.NodeList;
        }

        public List<NodeValue> GetDrawNode()
        {
            List<NodeValue> nodeList = new List<NodeValue>();
            if (DataController.Instance.CurrentOpenSubTree >= 0)
            {
                nodeList = GetSubTreeNode(DataController.Instance.CurrentOpenSubTree);
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

            NodeValue subTreeNode = DataController.Instance.GetNode(currentOpenSubTreeId);
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
                BehaviorTreeData data = DataController.Instance.ConfigDataDic[subTreeNode.subTreeConfig];
                if (null != data)
                {
                    nodeList.AddRange(data.nodeList);
                }
            }

            return nodeList;
        }

        private void CheckDrawNode(List<NodeValue> nodeList)
        {
            if (DataController.Instance.RunTimeInvalidSubTreeHash.Count <= 0)
            {
                return;
            }

            for (int i = nodeList.Count - 1; i >= 0; --i)
            {
                NodeValue nodeValue = nodeList[i];
                if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
                {
                    if (DataController.Instance.RunTimeInvalidSubTreeHash.Contains(nodeValue.id))
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

            if (DataController.Instance.CurrentOpenSubTree >= 0)
            {
                int nodeId = DataController.Instance.CurrentOpenSubTree;
                NodeValue nodeValue = DataController.Instance.GetNode(nodeId);
                while (null != nodeValue && nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
                {
                    string name = GetNodeName(nodeValue);
                    optionList.Insert(1, name);
                    dic[name] = nodeValue.id;

                    if (nodeValue.parentSubTreeNodeId <= 0)
                    {
                        break;
                    }
                    nodeValue = DataController.Instance.GetNode(nodeValue.parentSubTreeNodeId);
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
