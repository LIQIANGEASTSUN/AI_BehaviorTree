using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Repeat execution of the child node 
    /// </summary>
    public class NodeDecoratorRepeat : NodeDecorator
    {
        private int _repeatCount = 0;
        private int _executeCount = 0;

        public static string descript = "修饰节点_重复: \n " +
            "开始执行该节点时，将记录次数清零 \n " +
            "顺序执行所有子节点(记为 1 次)，不关心节点返回结果 \n\n " +
            "如果 执行次数 < 配置执行次数 向父节点返回 Running \n " +
            "如果 执行次数 >= 配置执行次数 向父节点返回 Success \n";

        private int _runningNode = 0;

        public NodeDecoratorRepeat() : base(NODE_TYPE.DECORATOR_REPEAT)
        { }

        public override void OnEnter()
        {
            base.OnEnter();
            _runningNode = 0;
            ReStart();
        }

        public override void OnExit()
        {
            base.OnExit();
            ReStart();

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
            ++_executeCount;

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
            }

            if (_repeatCount == -1 || _executeCount < _repeatCount)
            {
                resultType = ResultType.Running;
            }
            else
            {
                resultType = ResultType.Success;
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);

            return resultType;
        }

        public void ReStart()
        {
            _executeCount = 0;
        }

        public override void SetData(NodeValue nodeValue)
        {
            _repeatCount = nodeValue.repeatTimes;
        }

    }
}