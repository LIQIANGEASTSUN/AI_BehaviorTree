using System.Text;

namespace BehaviorTree
{
    public class DebugNodeParentInfoTool
    {

        public void DebugNodeParentInfo(int nodeId)
        {
            if (nodeId < 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            NodeValue nodeValue = DataController.Instance.GetNode(nodeId);
            while (null != nodeValue)
            {
                sb.AppendFormat("{0}->", nodeValue.id);

                NodeValue parentNode = null;
                if (nodeValue.parentNodeID < 0 && nodeValue.parentSubTreeNodeId > 0)
                {
                    parentNode = DataController.Instance.GetNode(nodeValue.parentSubTreeNodeId);
                }
                else
                {
                    parentNode = DataController.Instance.GetNode(nodeValue.parentNodeID);
                }
                nodeValue = parentNode;
            }

            UnityEngine.Debug.LogError("ParentInfo:" + sb.ToString());
        }

    }

}