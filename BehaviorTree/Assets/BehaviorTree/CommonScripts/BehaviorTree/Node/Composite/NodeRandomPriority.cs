using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 随机权重节点(组合节点)
    /// </summary>
    public class NodeRandomPriority : NodeRandom
    {
        private NodeBase lastRunningNode;
        private int _totalPriotity = 0;
        public static string descript = "RandomPriorityNodeFunctionDescript";

        private System.Random _random;
        public NodeRandomPriority() : base(NODE_TYPE.RANDOM_PRIORITY)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _random = new System.Random();
            lastRunningNode = null;
        }

        public override void OnExit()
        {
            base.OnExit();

            if (null != lastRunningNode)
            {
                lastRunningNode.Postposition(ResultType.Fail);
                lastRunningNode = null;
            }
        }

        public override ResultType Execute()
        {
            int index = -1;
            if (lastRunningNode != null)
            {
                index = lastRunningNode.NodeIndex;
            }
            lastRunningNode = null;

            ResultType resultType = ResultType.Fail;

            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                if (index < 0)
                {
                    index = GetRandom();
                }
                NodeBase nodeBase = nodeChildList[index];
                index = -1;


                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);

                if (resultType == ResultType.Fail)
                {
                    continue;
                }

                if (resultType == ResultType.Success)
                {
                    break;
                }

                if (resultType == ResultType.Running)
                {
                    lastRunningNode = nodeBase;
                    break;
                }
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }

        protected override int GetRandom()
        {
            BehaviorRandom.Check();

            _totalPriotity = 0;
            IEnumerable<int> ie = BehaviorRandom.GetRemainder();
            foreach (var index in ie)
            {
                _totalPriotity += nodeChildList[index].Priority;
            }

            int randomValue = _random.Next(0, _totalPriotity + 1);
            int priority = 0;

            int result = 0;
            ie = BehaviorRandom.GetRemainder();
            foreach (var index in ie)
            {
                priority += nodeChildList[index].Priority;
                if (priority >= randomValue)
                {
                    result = index;
                    BehaviorRandom.Remove(index);
                    break;
                }
            }

            return result;
        }
    }

}

