using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// if 判断并行节点
    /// </summary>
    public class NodeIfJudgeParallel : NodeIfJudge
    {
        public static string descript = "IfJudgeParallelNodeFunctionDescript";

        public NodeIfJudgeParallel() : base(NODE_TYPE.IF_JUDEG_PARALLEL)
        { }

        /// <summary>
        /// NodeDescript.GetDescript(NODE_TYPE);
        /// </summary>
        public override ResultType Execute()
        {
            if (nodeChildList.Count <= 0)
            {
                return ResultType.Fail;
            }

            ResultType resultType = ResultType.Fail;
            // 条件节点
            NodeBase ifNode = nodeChildList[0];
            resultType = ExecuteNode(ifNode, true);
            if (resultType == ResultType.Running)
            {
                return ResultType.Fail;
            }

            NodeBase nodeBase = GetBaseNode(resultType);
            if (null != nodeBase)
            {
                if (null != lastRunningNode && lastRunningNode.NodeId != nodeBase.NodeId)
                {
                    lastRunningNode.Postposition(ResultType.Fail);
                    lastRunningNode = null;
                }
                resultType = ExecuteNode(nodeBase, false);
            }
            else
            {
                resultType = _defaultResult;
            }

            return resultType;
        }
    }
}


