using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// composite abstract node
    /// </summary>
    public abstract class NodeComposite : NodeBase
    {
        //cache child nodes
        protected List<NodeBase> nodeChildList = new List<NodeBase>();

        public NodeComposite(NODE_TYPE nodeType) : base()
        {
            SetNodeType(nodeType);
        }

        /// <summary>
        /// add child node
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(NodeBase node)
        {
            int count = nodeChildList.Count;
            node.NodeIndex = count;
            nodeChildList.Add(node);
        }

        /// <summary>
        /// get all child nodes
        /// </summary>
        /// <returns></returns>
        public List<NodeBase> GetChilds()
        {
            return nodeChildList;
        }

        public void ClearChild()
        {
            nodeChildList.Clear();
        }

        public override ResultType Execute()
        {
            return ResultType.Fail;
        }
    }
}