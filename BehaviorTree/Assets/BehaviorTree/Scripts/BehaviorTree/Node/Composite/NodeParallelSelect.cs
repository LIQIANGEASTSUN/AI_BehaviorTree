using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// parallel select node
    /// </summary>
    public class NodeParallelSelect : NodeComposite
    {
        public static string descript = "ParallelSelectNodeFunctionDescript";
        private int _runningNode = 0;

        public NodeParallelSelect() : base(NODE_TYPE.PARALLEL_SELECT)
        { }

        public override void OnEnter()
        {
            base.OnEnter();
            _runningNode = 0;
        }

        public override void OnExit()
        {
            base.OnExit();
            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                int value = (1 << i);
                if ((_runningNode & value) > 0)
                {
                    NodeBase nodeBase = nodeChildList[i];
                    nodeBase.Postposition(ResultType.Fail);
                }
            }
        }

        public override ResultType Execute()
        {
            ResultType resultType = ResultType.Fail;
            int failCount = 0;
            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                NodeBase nodeBase = nodeChildList[i];

                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);

                if (resultType == ResultType.Success)
                {
                    break;
                }

                if (resultType == ResultType.Running)
                {
                    _runningNode |= (1 << i);
                    continue;
                }

                if (resultType == ResultType.Fail)
                {
                    ++failCount;
                    continue;
                }
            }

            if (resultType != ResultType.Success)
            {
                resultType = (_runningNode > 0) ? ResultType.Running : ResultType.Fail;
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }
    }
}
