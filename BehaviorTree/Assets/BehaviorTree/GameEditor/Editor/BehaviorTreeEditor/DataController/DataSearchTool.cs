using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class DataSearchTool
    {

        public static NodeValue GetNode(int nodeId)
        {
            NodeValue nodeValue = GetNode(BehaviorDataController.Instance.BehaviorTreeData, nodeId);
            if (null != nodeValue)
            {
                return nodeValue;
            }

            foreach (var kv in BehaviorDataController.Instance.ConfigDataDic)
            {
                BehaviorTreeData treeData = kv.Value;
                nodeValue = GetNode(treeData, nodeId);
                if (null != nodeValue)
                {
                    break;
                }
            }

            return nodeValue;
        }

        public static NodeValue GetNode(BehaviorTreeData treeData, int nodeId)
        {
            NodeValue nodeValue = treeData.nodeList.Find((a) => {
                return a.id == nodeId;
            });
            return nodeValue;
        }

        public static List<NodeValue> FindChild(BehaviorTreeData treeData, int nodeId)
        {
            List<NodeValue> nodeList = new List<NodeValue>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(nodeId);
            while (queue.Count > 0)
            {
                int id = queue.Dequeue();
                NodeValue nodeValue = GetNode(treeData, id);
                if (null == nodeValue)
                {
                    continue;
                }

                if (id != nodeId)
                {
                    nodeList.Add(nodeValue);
                }

                foreach(var node in nodeValue.childNodeList)
                {
                    queue.Enqueue(node);
                }
            }

            return nodeList;
        }

    }
}

