﻿using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Execute the child node and return Success
    /// </summary>
    public class NodeDecoratorReturnSuccess : NodeDecoratorReturnConst
    {
        public static string descript = "修饰_返回Success：\n " +
            "执行节点，无论节点返回 Success、Fail、Running \n " +
            "执行结束后永远向父节点返回 Success  \n";

        private int _runningNode = 0;

        public NodeDecoratorReturnSuccess() : base(NODE_TYPE.DECORATOR_RETURN_SUCCESS)
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

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)ResultType.Success, Time.realtimeSinceStartup);

            return ResultType.Success;
        }

        public override void Update()
        {

        }
    }
}