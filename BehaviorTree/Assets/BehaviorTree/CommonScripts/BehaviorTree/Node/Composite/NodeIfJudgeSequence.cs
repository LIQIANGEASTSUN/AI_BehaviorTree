using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// if 判断顺序节点
    /// </summary>
    public class NodeIfJudgeSequence : NodeIfJudge
    {
        public static string descript = "if判断顺序节点：\n" +
                                        "只能有 二或者三个子节点\n" +
                                        "第一个为判断节点只能返回Success、Fail \n\n" +

                                        "因为是顺序节点，每次执行时 \n" +
                                        "如果当前有正在执行的第二、第三个节点则\n" +
                                        "直接执行它的 Execute \n\n" +

                                        "如果没有，则执行第一个节点，根据第一个节点返回\n" +
                                        "结果 Success、Fail，选择执行第二、第三个节点"
                                        ;

        public NodeIfJudgeSequence() : base(NODE_TYPE.IF_JUDEG_SEQUENCE)
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

            if (null != lastRunningNode)
            {
                resultType = lastRunningNode.Execute();
            }
            else
            {
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


