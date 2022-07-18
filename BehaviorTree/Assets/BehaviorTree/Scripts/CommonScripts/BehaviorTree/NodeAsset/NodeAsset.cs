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

    public class NodeValue
    {
        public string identificationName = string.Empty;
        public int id = 0;
        public bool isRootNode = false;                    
        public int NodeType = (int)(NODE_TYPE.SELECT);   
        public int priority = 1;                          
        public int parentNodeID = -1;                      
        public List<int> childNodeList = new List<int>();  
        public List<NodeParameter> parameterList = new List<NodeParameter>();
        public int repeatTimes = 0;
        public string nodeName = string.Empty;
        public string descript = string.Empty;
        public string function = string.Empty;
        public List<ConditionGroup> conditionGroupList = new List<ConditionGroup>();

        #region IfJudgeData
        public List<IfJudgeData> ifJudgeDataList = new List<IfJudgeData>();
        public int defaultResult; 
        #endregion

        #region SubTree
        public int parentSubTreeNodeId = -1;
        public bool subTreeEntry = false;
        public int subTreeType = (int)SUB_TREE_TYPE.NORMAL;
        public string subTreeConfig = string.Empty;
        public long subTreeValue = -1;
        #endregion

        #region Editor use
        public RectT position = new RectT(); // 节点位置（编辑器显示使用）
        public bool moveWithChild = false;  // 同步移动子节点
        #endregion

        public NodeValue Clone()
        {
            NodeValue nodeValue = new NodeValue();

            nodeValue.identificationName = identificationName;
            nodeValue.id = this.id;
            nodeValue.isRootNode = isRootNode;                   
            nodeValue.NodeType = NodeType; 
            nodeValue.priority = priority;                        
            nodeValue.parentNodeID = parentNodeID;                     
            nodeValue.childNodeList.AddRange(childNodeList);  

            for (int i = 0; i < parameterList.Count; ++i)
            {
                nodeValue.parameterList.Add(parameterList[i].Clone());
            }

            nodeValue.repeatTimes = repeatTimes;
            nodeValue.nodeName = nodeName;
            nodeValue.descript = descript;
            nodeValue.function = function;

            for (int i = 0; i < conditionGroupList.Count; ++i)
            {
                nodeValue.conditionGroupList.Add(conditionGroupList[i].Clone());
            }

            for (int i = 0; i < ifJudgeDataList.Count; ++i)
            {
                nodeValue.ifJudgeDataList.Add(ifJudgeDataList[i].Clone());
                nodeValue.defaultResult = defaultResult;
            }

            #region SubTree
            nodeValue.parentSubTreeNodeId = parentSubTreeNodeId;
            nodeValue.subTreeEntry = subTreeEntry;
            nodeValue.subTreeType = subTreeType;
            nodeValue.subTreeConfig = subTreeConfig;
            nodeValue.subTreeValue = subTreeValue;
            #endregion

            #region Editor use
            nodeValue.position = position.Clone(); // Node position）
            nodeValue.moveWithChild = moveWithChild;  // sync move child
            #endregion
            return nodeValue;
        }
    }

    public class IfJudgeData
    {
        public int nodeId;
        public int ifJudegType = (int)NodeIfJudgeEnum.IF;
        public int ifResult = (int)ResultType.Fail;

        public IfJudgeData Clone()
        {
            IfJudgeData ifJudgeData = new IfJudgeData();
            ifJudgeData.nodeId = nodeId;
            ifJudgeData.ifJudegType = ifJudegType;
            ifJudgeData.ifResult = ifResult;
            return ifJudgeData;
        }
    }
   

}
