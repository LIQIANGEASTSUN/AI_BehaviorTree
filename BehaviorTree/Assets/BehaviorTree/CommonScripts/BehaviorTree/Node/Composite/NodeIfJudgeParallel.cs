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
        public static string descript = "if判断并行节点：\n" +
                                        "只能有 二或者三个子节点\n" +
                                        "第一个为判断节点只能返回Success、Fail \n\n" +

                                        "因为是并行节点，每次执行都会先执行第一个节点\n" +
                                        "根据第一个节点返回结果选择执行第二个、第三个节点 \n\n" +

                                        "如果上次执行的是第二、三个节点中的某一个 \n" +
                                        "当前要执行的节点跟上次相同，则会直接执行 Execute \n" +
                                        "如果当前要执行的节点跟上次不同，则会执行上次节点 \n" +
                                        "OnExit , 新节点则走 OnEnter、Execute"
                                        ;

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


