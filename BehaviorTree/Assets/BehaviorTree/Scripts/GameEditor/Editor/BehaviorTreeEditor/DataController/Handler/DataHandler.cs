using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    // BehaviorData 的操作：增加节点、删除节点、修改跟节点、子树操作
    public class DataHandler
    {

        // 添加节点
        public void AddNode(Node_Draw_Info_Item info, Vector3 mousePosition, int openSubTreeId)
        {
            NodeValue newNodeValue = new NodeValue();
            newNodeValue.id = GetNewNodeId();

            if (BehaviorDataController.Instance.BehaviorTreeData.rootNodeId < 0)
            {
                BehaviorDataController.Instance.BehaviorTreeData.rootNodeId = newNodeValue.id;
                newNodeValue.isRootNode = true;
            }

            newNodeValue.nodeName = info._nodeName;
            newNodeValue.identificationName = info._identificationName;
            newNodeValue.NodeType = (int)info._nodeType;
            newNodeValue.parentNodeID = -1;

            RectT rectT = new RectT();
            Rect rect = new Rect(mousePosition.x, mousePosition.y, rectT.width, rectT.height);
            newNodeValue.position = RectTool.RectToRectT(rect);

            newNodeValue.parentSubTreeNodeId = openSubTreeId;

            List<NodeValue> NodeList = BehaviorDataController.Instance.NodeList;
            NodeList.Add(newNodeValue);

            if (openSubTreeId >= 0)
            {
                bool hasEntryNode = false;
                for (int i = 0; i < NodeList.Count; ++i)
                {
                    if (NodeList[i].parentSubTreeNodeId == openSubTreeId
                        && (NodeList[i].subTreeEntry))
                    {
                        hasEntryNode = true;
                        break;
                    }
                }
                if (!hasEntryNode)
                {
                    ChangeSubTreeEntryNode(newNodeValue.parentSubTreeNodeId, newNodeValue.id);
                }
            }

            AddDefaultParameter(newNodeValue, newNodeValue.identificationName);
        }

        private void AddDefaultParameter(NodeValue nodeValue, string identificationName)
        {
            CustomIdentification customIdentification = BehaviorConfigNode.Instance.GetIdentification(identificationName);
            if (customIdentification.DefaultParameterList.Count <= 0)
            {
                return;
            }

            if (null == BehaviorDataController.Instance.BehaviorTreeData.parameterList)
            {
                Debug.LogError("没有配置参数");
                return;
            }
            foreach (var parameterName in customIdentification.DefaultParameterList)
            {
                NodeParameter behaviorParameter = BehaviorDataController.Instance.BehaviorTreeData.parameterList.Find((a) => { return a.parameterName == parameterName; });
                if (null != behaviorParameter)
                {
                    DataNodeHandler dataNodeHandler = new DataNodeHandler();
                    dataNodeHandler.NodeAddParameter(nodeValue, behaviorParameter);
                }
                else
                {
                    Debug.LogError("变量:" + parameterName + "  not found");
                }
            }
        }

        /// <summary>
        /// NodeId 规则：文件名所有字符的 Assic码相加 * 10000 + 0 - N
        /// 如名字 abc，则第2个ID 为 ((int)a + (int)b + (int)c) * 1000 + 2 = (61 + 62 + 63) * 1000 + 2 = 1860002
        /// </summary>
        /// <returns></returns>
        private int GetNewNodeId()
        {
            int id = -1;
            int index = -1;
            while (id == -1)
            {
                ++index;
                id = index;
                List<NodeValue> NodeList = BehaviorDataController.Instance.NodeList;
                for (int i = 0; i < NodeList.Count; ++i)
                {
                    int value = NodeList[i].id % 10000;
                    if (value == index)
                    {
                        id = -1;
                    }
                }
            }

            id = DataNodeIdStandardTool.IdTransition(BehaviorDataController.Instance.FileName, id);
            return id;
        }

        // 删除节点
        public void DeleteNode(int nodeId)
        {
            List<NodeValue> NodeList = BehaviorDataController.Instance.NodeList;
            for (int i = 0; i < NodeList.Count; ++i)
            {
                NodeValue nodeValue = NodeList[i];
                if (nodeValue.id != nodeId)
                {
                    continue;
                }

                RemoveParentNode(nodeValue.id);
                for (int j = 0; j < nodeValue.childNodeList.Count; ++j)
                {
                    int childId = nodeValue.childNodeList[j];
                    NodeValue childNode = BehaviorDataController.Instance.GetNode(childId);
                    childNode.parentNodeID = -1;
                }

                if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
                {
                    DeleteSubTreeChild(nodeValue.id);
                }

                NodeList.Remove(nodeValue);
                break;
            }
        }

        public void DeleteSubTreeChild(int subTreeNodeId)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(subTreeNodeId);
            if (nodeValue.childNodeList.Count <= 0)
            {
                return;
            }
            nodeValue.childNodeList.Clear();

            List<NodeValue> NodeList = BehaviorDataController.Instance.NodeList;
            for (int j = NodeList.Count - 1; j >= 0; --j)
            {
                NodeValue node = NodeList[j];
                if (node.parentSubTreeNodeId == subTreeNodeId)
                {
                    NodeList.RemoveAt(j);
                }
            }
        }

        public void RemoveParentNode(int nodeId)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
            if (nodeValue.parentNodeID < 0)
            {
                return;
            }

            NodeValue parentNode = BehaviorDataController.Instance.GetNode(nodeValue.parentNodeID);
            if (null != parentNode)
            {
                for (int i = 0; i < parentNode.childNodeList.Count; ++i)
                {
                    int childId = parentNode.childNodeList[i];
                    NodeValue childNode = BehaviorDataController.Instance.GetNode(childId);
                    if (childNode.id == nodeValue.id)
                    {
                        parentNode.childNodeList.RemoveAt(i);
                        break;
                    }
                }
            }

            nodeValue.parentNodeID = -1;
        }

        public void ChangeSubTreeEntryNode(int subTreeNodeId, int nodeId)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
            if (null == nodeValue)
            {
                return;
            }

            NodeValue subTreeNode = BehaviorDataController.Instance.GetNode(subTreeNodeId);
            if (null == subTreeNode)
            {
                return;
            }

            List<NodeValue> nodeList = BehaviorDataController.Instance.NodeList;
            for (int i = 0; i < nodeList.Count; ++i)
            {
                if (nodeList[i].parentSubTreeNodeId == nodeValue.parentSubTreeNodeId)
                {
                    if (nodeList[i].subTreeEntry)
                    {
                        RemoveParentNode(nodeList[i].id);
                    }

                    nodeList[i].subTreeEntry = (nodeList[i].id == nodeId);
                    if (nodeList[i].subTreeEntry)
                    {
                        subTreeNode.childNodeList.Clear();
                        subTreeNode.childNodeList.Add(nodeId);
                    }
                }
            }

            DataNodeHandler dataNodeHandler = new DataNodeHandler();
            dataNodeHandler.NodeAddChild(subTreeNodeId, nodeValue.id);
        }

        public void ChangeRootNode(int rootNodeId)
        {
            BehaviorDataController.Instance.BehaviorTreeData.rootNodeId = rootNodeId;
            List<NodeValue> NodeList = BehaviorDataController.Instance.NodeList;
            for (int i = 0; i < NodeList.Count; ++i)
            {
                NodeList[i].isRootNode = (NodeList[i].id == rootNodeId);
            }
        }

        public void DataAddGlobalParameter(NodeParameter parameter)
        {
            BehaviorTreeData behaviorTreeData = BehaviorDataController.Instance.BehaviorTreeData;
            DataParameterHandler dataParameterHandler = new DataParameterHandler();
            dataParameterHandler.AddParameter(behaviorTreeData.parameterList, parameter);
        }

        public void DataDelGlobalParameter(NodeParameter parameter)
        {
            BehaviorTreeData behaviorTreeData = BehaviorDataController.Instance.BehaviorTreeData;
            DataParameterHandler dataParameterHandler = new DataParameterHandler();
            dataParameterHandler.DelParameter(behaviorTreeData.parameterList, parameter);
        }

    }
}