using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 条件节点(叶节点)
    /// </summary>
    public abstract class NodeCondition : NodeLeaf, ICondition
    {
        protected List<NodeParameter> _parameterList;
        private List<ConditionGroup> _conditionGroupList;
        protected ConditionParameter conditionParameter = null;
        protected IConditionCheck _iconditionCheck = null;

        public NodeCondition() : base()
        {
            SetNodeType(NODE_TYPE.CONDITION);
            conditionParameter = new ConditionParameter();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            conditionParameter.Init(_conditionGroupList, _parameterList);
        }

        public override ResultType Execute()
        {
            ResultType resultType = Condition();
            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }

        public void SetConditionCheck(IConditionCheck iConditionCheck)
        {
            _iconditionCheck = iConditionCheck;
        }

        public void SetData(List<NodeParameter> parameterList, List<ConditionGroup> conditionGroupList)
        {
            _parameterList = parameterList;
            _conditionGroupList = conditionGroupList;
        }

        /// <summary>
        /// condition node need to implement this method
        /// </summary>
        /// <returns></returns>
        public abstract ResultType Condition();

    }
}