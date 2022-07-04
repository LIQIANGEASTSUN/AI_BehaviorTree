using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 修饰节点_返回Fail
    /// </summary>
    public class NodeDecoratorReturnFail : NodeDecoratorReturnConst
    {
        public static string descript = "修饰_返回Fail：\n" +
                                        "执行节点，无论节点返回 Success、Fail、Running \n" +
                                        "执行结束后永远向父节点返回 Fail  \n";

        private int _runningNode = 0;

        public NodeDecoratorReturnFail() : base(NODE_TYPE.DECORATOR_RETURN_FAIL)
        {
            SetConstResult(ResultType.Success);
        }

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
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)ResultType.Fail, Time.realtimeSinceStartup);
            return ResultType.Fail;
        }

        public override void Update()
        {

        }
    }
}