using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// if node
    /// </summary>
    public abstract class NodeIfJudge : NodeComposite
    {
        protected NodeBase lastRunningNode;
        protected List<IfJudgeData> _ifJudgeDataList;
        protected ResultType _defaultResult;

        public NodeIfJudge(NODE_TYPE nodeType) : base(nodeType)
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
            return ResultType.Fail;
        }

        protected ResultType ExecuteNode(NodeBase nodeBase, bool isFirstNode)
        {
            ResultType resultType = ResultType.Fail;
            if (null != nodeBase)
            {
                nodeBase.Preposition();
                resultType = nodeBase.Execute();
                nodeBase.Postposition(resultType);
            }

            if (!isFirstNode && resultType == ResultType.Running)
            {
                lastRunningNode = nodeBase;
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }

        public void SetDefaultResult(ResultType defaultResult)
        {
            _defaultResult = defaultResult;
        }

        public override void SetData(NodeValue nodeValue)
        {
            _ifJudgeDataList = nodeValue.ifJudgeDataList;
            SetDefaultResult((ResultType)nodeValue.defaultResult);
        }

        public void SetData(List<IfJudgeData> ifJudgeDataList)
        {
            _ifJudgeDataList = ifJudgeDataList;
        }

        protected IfJudgeData GetData(int nodeId)
        {
            for (int i = 0; i < _ifJudgeDataList.Count; ++i)
            {
                IfJudgeData data = _ifJudgeDataList[i];
                if (data.nodeId == nodeId)
                {
                    return data;
                }
            }
            return null;
        }

        protected NodeBase GetBaseNode(ResultType resultType)
        {
            for (int i = 0; i < nodeChildList.Count; ++i)
            {
                NodeBase node = nodeChildList[i];
                IfJudgeData judgeData = GetData(node.NodeId);
                if (judgeData.ifJudegType == (int)NodeIfJudgeEnum.IF)
                {
                    continue;
                }

                if (judgeData.ifResult == (int)resultType)
                {
                    return node;
                }
            }

            return null;
        }

    }

}

