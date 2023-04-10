using UnityEngine;
using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Action node
    /// </summary>
    public abstract class NodeAction : NodeLeaf, IAction
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

        /// <summary>
        /// action node need to implement this method
        /// </summary>
        /// <returns></returns>
        public abstract ResultType DoAction();

        public override void SetData(NodeValue nodeValue)
        {
            _parameterList.AddRange(nodeValue.parameterList);
        }
    }
}