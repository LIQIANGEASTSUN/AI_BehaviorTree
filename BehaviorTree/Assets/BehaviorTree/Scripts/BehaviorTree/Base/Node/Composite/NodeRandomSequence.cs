using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// random sequence node
    /// </summary>
    public class NodeRandomSequence : NodeRandom
    {
        private NodeBase lastRunningNode;
        public static string descript = "随机顺序节点：(参考顺序节点) \n" +
            "每次随机一个未执行的节点，总随机次数为子节点个数 \n\n" +
            "当前执行节点返回 Success，继续随机一个未执行的节点 \n\n" +
            "当前执行节点返回 Fail，退出停止 \n" +
            "向父节点返回 Fail \n\n" +
            "当前执行节点返回 Running, 记录当前节点 \n" +
            "向父节点返回 Running，下次执行直接从该节点开始 \n\n" +
            "如果所有节点都返回 Success \n" +
            "向父节点返回 Success \n\n";

        public NodeRandomSequence() : base(NODE_TYPE.RANDOM_SEQUEUECE)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
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
                    break;
                }

                if (resultType == ResultType.Success)
                {
                    continue;
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
    }
}

