using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// node abstract superclass
    /// </summary>
    public abstract class NodeBase : AbstractNode
    {
        /// <summary>
        /// node type
        /// </summary>
        protected NODE_TYPE nodeType;
        private int priority;

        /// <summary>
        /// node execution status
        /// </summary>
        protected NODE_STATUS nodeStatus = NODE_STATUS.READY;

        /// <summary>
        /// Execute the node abstract method
        /// </summary>
        /// <returns>Return execution result</returns>
        public virtual ResultType Execute()
        {
            return ResultType.Fail;
        }

        /// <summary>
        /// Called on the first line of the Execute() method
        /// </summary>
        public void Preposition()
        {
            if (nodeStatus == NODE_STATUS.READY)
            {
                nodeStatus = NODE_STATUS.RUNNING;
                OnEnter();
            }
        }

        /// <summary>
        ///  Called before returen of the Execute() method
        /// </summary>
        public void Postposition(ResultType resultType)
        {
            if (resultType != ResultType.Running)
            {
                nodeStatus = NODE_STATUS.READY;
                OnExit();
            }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }


        protected void SetNodeType(NODE_TYPE nodeType)
        {
            this.nodeType = nodeType;
        }

        public override int NodeType()
        {
            return (int)nodeType;
        }

        public virtual void SetData(NodeValue nodeValue)
        {

        }
    }
}