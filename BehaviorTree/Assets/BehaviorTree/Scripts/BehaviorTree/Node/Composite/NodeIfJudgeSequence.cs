using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// If sequence node
    /// </summary>
    public class NodeIfJudgeSequence : NodeIfJudge
    {

        public static string descript = "IfJudgeSequenceNodeFunctionDescript";

        public NodeIfJudgeSequence() : base(NODE_TYPE.IF_JUDEG_SEQUENCE)
        { }

        public override ResultType Execute()
        {
            if (nodeChildList.Count <= 0)
            {
                return ResultType.Fail;
            }

            ResultType resultType = ResultType.Fail;

            if (null != lastRunningNode)
            {
                resultType = lastRunningNode.Execute();
            }
            else
            {
                // The first node is the conditional node
                NodeBase ifNode = nodeChildList[0];
                resultType = ExecuteNode(ifNode, true);
                if (resultType == ResultType.Running)
                {
                    return ResultType.Fail;
                }
                NodeBase nodeBase = GetBaseNode(resultType);
                if (null != nodeBase)
                {
                    resultType = ExecuteNode(nodeBase, false);
                }
                else
                {
                    resultType = _defaultResult;
                }
            }

            return resultType;
        }
    }
}


