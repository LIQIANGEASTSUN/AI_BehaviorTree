using GraphicTree;
using System.Collections.Generic;

namespace BehaviorTree
{
    // NodeValue 的操作：添加子节点, 节点添加/删除/修改参数
    public class DataNodeHandler
    {

        public void NodeAddChild(int parentId, int childId)
        {
            NodeValue parentNode = BehaviorDataController.Instance.GetNode(parentId);
            NodeValue childNode = BehaviorDataController.Instance.GetNode(childId);

            if (null == parentNode || null == childNode)
            {
                UnityEngine.Debug.LogError("node is null");
                return;
            }

            int findId = parentNode.childNodeList.Find((a) => {
                return a == childId;
            });
            if (findId == childId)
            {
                if (childNode.parentNodeID != parentId)
                {
                    parentNode.childNodeList.Remove(childId);
                }
                else
                {
                    return;
                }
            }

            string msg = string.Empty;
            bool result = true;
            if (childNode.parentNodeID >= 0)
            {
                result = false;
                if (childNode.parentNodeID != parentId)
                {
                    msg = "已经有父节点";
                }
                else
                {
                    msg = "不能重复添加父节点";
                }
            }

            if (parentNode.parentNodeID >= 0 && parentNode.parentNodeID == childNode.id)
            {
                msg = "不能添加父节点作为子节点";
                result = false;
            }

            if (!result && TreeNodeWindow.window != null)
            {
                TreeNodeWindow.window.ShowNotification(msg);
            }

            if (!result)
            {
                return;
            }

            // 修饰节点只能有一个子节点
            if (parentNode.NodeType >= (int)NODE_TYPE.DECORATOR_INVERTER && parentNode.NodeType <= (int)NODE_TYPE.DECORATOR_UNTIL_SUCCESS)
            {
                for (int i = 0; i < parentNode.childNodeList.Count; ++i)
                {
                    NodeValue node = BehaviorDataController.Instance.GetNode(parentNode.childNodeList[i]);
                    if (null != node)
                    {
                        node.parentNodeID = -1;
                    }
                }
                parentNode.childNodeList.Clear();
            }

            parentNode.childNodeList.Add(childNode.id);
            childNode.parentNodeID = parentNode.id;
        }

        public void NodeAddParameter(NodeValue nodeValue, NodeParameter parameter)
        {
            DataParameterHandler dataParameterHandler = new DataParameterHandler();
            dataParameterHandler.AddParameter(nodeValue.parameterList, parameter);
        }

        public void NodeDelParameter(int nodeId, NodeParameter parameter)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
            if (null != nodeValue)
            {
                DataParameterHandler dataParameterHandler = new DataParameterHandler();
                dataParameterHandler.DelParameter(nodeValue.parameterList, parameter);
                for (int i = 0; i < nodeValue.parameterList.Count; ++i)
                {
                    nodeValue.parameterList[i].index = i;
                }
            }
        }

        public void NodeParameterChange(int nodeId, string oldParameter, string newParameter)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
            if (null == nodeValue)
            {
                return;
            }

            NodeParameter parameter = null;
            for (int i = 0; i < BehaviorDataController.Instance.BehaviorTreeData.parameterList.Count; ++i)
            {
                NodeParameter temp = BehaviorDataController.Instance.BehaviorTreeData.parameterList[i];
                if (temp.parameterName.CompareTo(newParameter) == 0)
                {
                    parameter = temp;
                }
            }

            if (null == parameter)
            {
                return;
            }

            for (int i = 0; i < nodeValue.parameterList.Count; ++i)
            {
                NodeParameter temp = nodeValue.parameterList[i];
                if (temp.parameterName.CompareTo(parameter.parameterName) == 0)
                {
                    nodeValue.parameterList[i] = parameter.Clone();
                    break;
                }
            }

            for (int i = 0; i < nodeValue.parameterList.Count; ++i)
            {
                nodeValue.parameterList[i].index = i;
            }
        }

        public void NodeAddConditionGroup(int nodeId)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
            if (null == nodeValue)
            {
                return;
            }

            for (int i = 0; i < nodeValue.conditionGroupList.Count + 1; ++i)
            {
                ConditionGroup conditionGroup = nodeValue.conditionGroupList.Find(a => a.index == i);
                if (null == conditionGroup)
                {
                    conditionGroup = new ConditionGroup();
                    conditionGroup.index = i;
                    nodeValue.conditionGroupList.Add(conditionGroup);
                    break;
                }
            }

            if (nodeValue.conditionGroupList.Count <= 0)
            {
                ConditionGroup conditionGroup = new ConditionGroup();
                conditionGroup.index = 0;
                nodeValue.conditionGroupList.Add(conditionGroup);
            }
        }

        public void NodeDelConditionGroup(int nodeId, int groupId)
        {
            NodeValue nodeValue = BehaviorDataController.Instance.GetNode(nodeId);
            if (null == nodeValue)
            {
                return;
            }

            for (int i = 0; i < nodeValue.conditionGroupList.Count; ++i)
            {
                if (nodeValue.conditionGroupList[i].index == groupId)
                {
                    nodeValue.conditionGroupList.RemoveAt(i);
                }
            }
        }

    }
}
