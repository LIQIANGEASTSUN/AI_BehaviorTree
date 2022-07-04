using System.Collections.Generic;

namespace BehaviorTree
{

    public class CheckNodeTool
    {
        private static int[] rootNodeArr = null;
        public static void CheckNode(List<NodeValue> nodeValueList)
        {
            int rootNodeCount = 0;
            NodeValue invalidNodeValue = null;
            rootNodeArr = new int[nodeValueList.Count];

            bool rootNodeHasParent = false;
            for (int i = 0; i < nodeValueList.Count; i++)
            {
                NodeValue nodeValue = nodeValueList[i];
                if (nodeValue.isRootNode)
                {
                    rootNodeArr[rootNodeCount] = nodeValue.id;
                    ++rootNodeCount;
                    if (nodeValue.parentNodeID >= 0)
                    {
                        rootNodeHasParent = true;
                    }
                }

                if (((NODE_TYPE)nodeValue.NodeType == NODE_TYPE.CONDITION || (NODE_TYPE)nodeValue.NodeType == NODE_TYPE.ACTION) && nodeValue.childNodeList.Count > 0)
                {
                    invalidNodeValue = nodeValue;  // 叶节点 不能有子节点
                }
            }

            string meg = string.Empty;
            if (rootNodeCount > 1)
            {
                meg = string.Format("有多个根节点:");
                for (int i = 0; i < rootNodeCount; ++i)
                {
                    meg += string.Format("_{0} ", rootNodeArr[i]);
                }
            }
            else if (rootNodeCount == 0)
            {
                meg = "必须有一个根节点";
            }
            else if (rootNodeHasParent)
            {
                meg = string.Format("跟节点_{0} 不能有父节点", rootNodeArr[0]);
            }

            if (null != invalidNodeValue)
            {
                int index = EnumNames.GetEnumIndex<NODE_TYPE>((NODE_TYPE)invalidNodeValue.NodeType);
                string name = EnumNames.GetEnumName<NODE_TYPE>(index);
                meg = string.Format("节点:{0} {1} 不能有子节点", invalidNodeValue.id, name);
            }

            if (TreeNodeWindow.window != null && !string.IsNullOrEmpty(meg))
            {
                TreeNodeWindow.window.ShowNotification(meg);
            }
        }
    }
}
