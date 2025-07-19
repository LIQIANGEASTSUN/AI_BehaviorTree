using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    public class BehaviorTreeData
    {
        public string fileName = string.Empty;
        public int rootNodeId = -1;
        public List<NodeValue> nodeList = new List<NodeValue>();
        public List<NodeParameter> parameterList = new List<NodeParameter>();
        public string descript = string.Empty;
        // nodeDic is cleared for storage and used at RunTime
        public Dictionary<int, NodeValue> nodeDic = new Dictionary<int, NodeValue>();
    }
}
