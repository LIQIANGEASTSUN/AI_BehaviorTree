using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// parallel select node
    /// </summary>
    public class NodeParallelSelect : NodeComposite
    {
        public static string descript = "并行选择节点：依次从头顺次遍历执行所有子节点 \n\n " +
            "当前执行节点返回 Success，退出停止，向父节点 \n " +
            "返回 Success \n\n " +
            "当前执行节点返回 Running, 记录当前节点，继续 \n " +
            "执行下一个节点，记录所有返回 Running 的节点 \n\n " +
            "当前执行节点返回 Fail，继续执行下一个节点 \n\n " +
            "如果没有节点返回 Success， \n " +
            "如果 Running 的节点数大于 0 向父节点返回 Running \n " +
            "否则向父节点返回 Fail \n;";

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
