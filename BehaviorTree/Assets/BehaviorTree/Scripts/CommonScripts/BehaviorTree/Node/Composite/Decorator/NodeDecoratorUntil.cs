using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// execute child node until the child node returns a fixed result
    /// </summary>
    public abstract class NodeDecoratorUntil : NodeDecorator
    {
        private ResultType _desiredResult = ResultType.Fail;
        private int _runningNode = 0;

        public NodeDecoratorUntil(NODE_TYPE nodeType) : base(nodeType)
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
            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                NodeBase nodeBase = nodeChildList[i];
                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);

                if (resultType == ResultType.Running)
                {
                    _runningNode |= (1 << i);
                }

                if (resultType == _desiredResult)
                {
                    return ResultType.Success;
                }
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)ResultType.Running, Time.realtimeSinceStartup);
            return ResultType.Running;
        }

        public void SetDesiredResult(ResultType resultType)
        {
            _desiredResult = resultType;
        }

    }
}