using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Parallel node
    /// </summary>
    public class NodeParallel : NodeComposite
    {
        public static string descript = "并行节点：依次从头顺次遍历执行所有子节点 \n\n " +
            "当前执行节点返回 Fail，退出停止，向父节点 \n " +
            "返回 Fail \n\n " +
            "当前执行节点返回 Success，记录当前节点，继续 \n " +
            "执行下一个节点，记录所有返回 Success 的节点\n\n " +
            "当前执行节点返回 Running, 记录当前节点，继续 \n " +
            "执行下一个节点，记录所有返回 Running 的节点 \n\n" +
            "如果没有节点返回 Fail， \n " +
            "如果所有节点都返回 Success 向父节点返回 Success \n " +
            "否则向父节点返回 Running \n;";

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
