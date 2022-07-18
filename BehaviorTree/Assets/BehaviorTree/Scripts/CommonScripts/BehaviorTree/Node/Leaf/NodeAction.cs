using UnityEngine;
using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Action node
    /// </summary>
    public class NodeAction : NodeLeaf, IAction
    {
        protected List<NodeParameter> _parameterList = new List<NodeParameter>();

        public NodeAction() : base()
        {
            SetNodeType(NODE_TYPE.ACTION);
        }

        public override ResultType Execute()
        {
            ResultType resultType = ResultType.Fail;
            resultType = DoAction();

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }

        public void SetParameters(List<NodeParameter> parameterList)
        {
            _parameterList.AddRange(parameterList);
        }

        public virtual ResultType DoAction()
        {
            return ResultType.Success;
        }
    }
}