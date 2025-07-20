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
        public static string descript = "并行节点：依次从头顺次遍历执行所有子节点 \n\n " +
            "当前执行节点返回 Fail，退出停止，向父节点 \n " +
            "返回 Fail \n\n " +
            "当前执行节点返回 Success，记录当前节点，继续 \n " +
            "执行下一个节点，记录所有返回 Success 的节点\n\n " +
            "当前执行节点返回 Running, 记录当前节点，继续 \n " +
            "执行下一个节点，记录所有返回 Running 的节点 \n\n " +
            "如果没有节点返回 Fail， \n " +
            "如果所有节点都返回 Success 向父节点返回 Success \n " +
            "否则向父节点返回 Running \n;";

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


