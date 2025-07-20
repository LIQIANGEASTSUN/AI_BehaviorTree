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
                    invalidNodeValue = nodeValue;  // Leaf nodes cannot have children
                }
            }

            string meg = string.Empty;
            if (rootNodeCount > 1)
            {
                meg = "有多个根节点";
                for (int i = 0; i < rootNodeCount; ++i)
                {
                    meg = $"{meg}_{rootNodeArr[i]}";
                }
            }
            else if (rootNodeCount == 0)
            {
                meg = "必须有一个根节点";
            }
            else if (rootNodeHasParent)
            {
                meg = $"跟节点:{rootNodeArr[0]}  不能有父节点";
            }

            if (null != invalidNodeValue)
            {
                int index = EnumNames.GetEnumIndex<NODE_TYPE>((NODE_TYPE)invalidNodeValue.NodeType);
                string name = EnumNames.GetEnumName<NODE_TYPE>(index);
                meg = $"节点:{invalidNodeValue.id} {name} 不能有子节点";
            }

            if (TreeNodeWindow.window != null && !string.IsNullOrEmpty(meg))
            {
                TreeNodeWindow.window.ShowNotification(meg);
            }
        }
    }
}
