
namespace GraphicTree
{

    public abstract class AbstractNode
    {

        private int nodeIndex;

        private int nodeId;

        /// <summary>
        /// Entity ID of a node
        /// </summary>
        private int entityId;

        public AbstractNode()
        {
        }

        public int NodeIndex
        {
            get { return nodeIndex; }
            set { nodeIndex = value; }
        }

        public int NodeId
        {
            get { return nodeId; }
            set { nodeId = value; }
        }

        public int EntityId
        {
            get { return entityId; }
            set { entityId = value; }
        }

        public abstract int NodeType();

        /// <summary>
        /// Node entry, which executes the first method of a node
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// Node exit, which is called when the node exits execution
        /// </summary>
        public virtual void OnExit()
        {

        }

    }

}
