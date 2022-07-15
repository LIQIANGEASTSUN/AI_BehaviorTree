
namespace GraphicTree
{

    public abstract class AbstractNode
    {

        /// <summary>
        /// 节点序列
        /// </summary>
        private int nodeIndex;

        /// <summary>
        /// 节点Id
        /// </summary>
        private int nodeId;

        /// <summary>
        /// EntityId
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
        /// 进入节点
        /// </summary>
        public virtual void OnEnter()
        {
            //ProDebug.Logger.LogError("OnEnter:" + NodeId);
        }

        /// <summary>
        /// 退出节点
        /// </summary>
        public virtual void OnExit()
        {
            //ProDebug.Logger.LogError("OnExit:" + NodeId);
        }

    }

}
