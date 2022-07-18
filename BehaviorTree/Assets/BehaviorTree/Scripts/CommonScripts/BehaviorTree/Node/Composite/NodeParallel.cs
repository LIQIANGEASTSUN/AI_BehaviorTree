using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Parallel node
    /// </summary>
    public class NodeParallel : NodeComposite
    {
        public static string descript = "ParallelNodeFunctionDescript";
        private int _runningNode = 0;

        public NodeParallel() : base(NODE_TYPE.PARALLEL)
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

            int successCount = 0;
            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                NodeBase nodeBase = nodeChildList[i];

                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);

                if (resultType == ResultType.Fail)
                {
                    break;
                }

                if (resultType == ResultType.Success)
                {
                    ++successCount;
                    continue;
                }

                if (resultType == ResultType.Running)
                {
                    _runningNode |= (1 << i);
                    continue;
                }
            }

            if (resultType != ResultType.Fail)
            {
                resultType = (successCount >= nodeChildList.Count) ? ResultType.Success : ResultType.Running;
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }
    }
}
