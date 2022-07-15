using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 节点超类
    /// </summary>
    public abstract class NodeBase : AbstractNode
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        protected NODE_TYPE nodeType;

        /// <summary>
        /// 权重
        /// </summary>
        private int priority;

        protected NODE_STATUS nodeStatus = NODE_STATUS.READY;

        /// <summary>
        /// 执行节点抽象方法
        /// </summary>
        /// <returns>返回执行结果</returns>
        public virtual ResultType Execute()
        {
            return ResultType.Fail;
        }

        //执行 Execute 的前置方法，在 Execute() 方法的第一行调用
        public void Preposition()
        {
            if (nodeStatus == NODE_STATUS.READY)
            {
                nodeStatus = NODE_STATUS.RUNNING;
                OnEnter();
            }
        }

        /// <summary>
        ///  执行 Execute 的后置方法，在 Execute() 方法的 returen 前调用
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
    }
}