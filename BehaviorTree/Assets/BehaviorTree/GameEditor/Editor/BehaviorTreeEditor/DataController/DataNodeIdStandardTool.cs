
namespace BehaviorTree
{
    // NodeId 标准化
    public class DataNodeIdStandardTool
    {
        public static BehaviorTreeData StandardID(BehaviorTreeData data)
        {
            data.rootNodeId = IdTransition(data.fileName, data.rootNodeId);
            for (int i = 0; i < data.nodeList.Count; ++i)
            {
                NodeValue nodeValue = data.nodeList[i];

                nodeValue.id = IdTransition(data.fileName, nodeValue.id);
                if (nodeValue.parentNodeID >= 0)
                {
                    nodeValue.parentNodeID = IdTransition(data.fileName, nodeValue.parentNodeID);
                }

                if (nodeValue.parentSubTreeNodeId >= 0)
                {
                    nodeValue.parentSubTreeNodeId = IdTransition(data.fileName, nodeValue.parentSubTreeNodeId);
                }

                for (int j = 0; j < nodeValue.childNodeList.Count; ++j)
                {
                    nodeValue.childNodeList[j] = IdTransition(data.fileName, nodeValue.childNodeList[j]);
                }

                for (int j = 0; j < nodeValue.ifJudgeDataList.Count; ++j)
                {
                    nodeValue.ifJudgeDataList[j].nodeId = IdTransition(data.fileName, nodeValue.ifJudgeDataList[j].nodeId);
                }
            }

            return data;
        }

        public static int IdTransition(string fileName, int id)
        {
            char[] charArr = fileName.ToCharArray();
            int assic = 0;
            for (int i = 0; i < charArr.Length; ++i)
            {
                assic += (int)charArr[i];
            }

            id = assic * 10000 + id % 10000;
            return id;
        }

    }
}
