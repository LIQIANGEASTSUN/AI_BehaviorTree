﻿using UnityEngine;
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

        public static string descript = "DecoratorRepeatNodeFunctionDescript";
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