using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Execute all nodes in parallel
    /// </summary>
    public class NodeParallelAll : NodeComposite
    {
        public static string descript = "并行执行所有节点：依次从头顺次遍历执行所有子节点 \n\n " +
            "当前执行节点返回 Success、 Fail、Running 都继续 \n " +
            "执行下一个节点，分别记录返回三种结果的节点个数 \n\n " +
            "执行完所有节点后 \n " +
            "如果所有节点都返回 Success 向父节点返回 Success \n " +
            "如果所有节点都返回 Fail 向父节点返回 Fail \n " +
            "否则一定有节点返回了Running 向父节点返回 Running \n;";

        private int _runningNode = 0;

        public NodeParallelAll() : base(NODE_TYPE.PARALLEL_ALL)
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
            int failCount = 0;
            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                NodeBase nodeBase = nodeChildList[i];

                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);

                if (resultType == ResultType.Fail)
                {
                    ++failCount;
                }
                else if (resultType == ResultType.Success)
                {
                    ++successCount;
                }
                else if (resultType == ResultType.Running)
                {
                    _runningNode |= (1 << i);
                }
            }

            if (successCount >= nodeChildList.Count)
            {
                resultType = ResultType.Success;
            }
            else if (failCount >= nodeChildList.Count)
            {
                resultType = ResultType.Fail;
            }
            else
            {
                resultType = ResultType.Running;
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }
    }
}



