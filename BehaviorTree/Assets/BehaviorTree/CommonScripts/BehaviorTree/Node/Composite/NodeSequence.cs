using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 顺序节点(组合节点)
    /// </summary>
    public class NodeSequence : NodeComposite
    {
        private NodeBase lastRunningNode;
        public static string descript = "顺序节点：依次执行子节点 \n" +
                                        "当前执行节点返回 Success，就继续执行后续节点 \n\n" +

                                        "当前执行节点返回 Fail，退出停止，向父节点 \n" +
                                        "返回 Fail，下次执行直接从第一个节点开始 \n\n" +

                                        "当前执行节点返回 Running, 记录当前节点，向父节 \n" +
                                        "点返回 Running，下次执行直接从该节点开始 \n\n" +

                                        "如果所有节点都返回 Success，向父节点返回 Success \n";

        public NodeSequence() : base(NODE_TYPE.SEQUENCE)
        { }

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
            int index = 0;
            if (lastRunningNode != null)
            {
                index = lastRunningNode.NodeIndex;
            }
            lastRunningNode = null;

            ResultType resultType = ResultType.Fail;
            for (int i = index; i < nodeChildList.Count; ++i)
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

/*

    index = 1
    if != lastRunningNode null then
        index = lastRunningNode.index
    end

    lastRunningNode = null
    for i <- index to N do 
    
        Node node =  GetNode(i);

        result = node.execute()
        
        if result == fail then
           return fail;
        end

        if result == running then
            lastRunningNode = node
            return running
        end

    end

    return success



*/
