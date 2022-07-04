using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 选择节点(组合节点)
    /// </summary>
    public class NodeSelect : NodeComposite
    {
        private NodeBase lastRunningNode;
        public static string descript = "选择节点：依次从头顺次遍历执行所有子节点 \n" +
                                        "当前执行节点返回 Success，退出停止，向父节点 \n" +
                                        "返回 Success \n\n" +

                                        "当前执行节点返回 Fail，退出当前节点 \n" +
                                        "继续执行下一个节点 \n\n" +

                                        "当前执行节点返回 Running, 记录当前节点，向父节 \n" +
                                        "点返回 Running，下次执行直接从该节点开始 \n\n" +

                                        "如果所有节点都返回Fail，执行完所有节点后 \n" +
                                        "向父节点返回 Fail; \n\n";

        public NodeSelect() : base(NODE_TYPE.SELECT)
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

        /// <summary>
        /// NodeDescript.GetDescript(NODE_TYPE);
        /// </summary>
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
           continue;
        end

        if result == success then
            return success
        end

        if result == running then
            lastRunningNode = node
            return running
        end

    end

    return fail

*/
