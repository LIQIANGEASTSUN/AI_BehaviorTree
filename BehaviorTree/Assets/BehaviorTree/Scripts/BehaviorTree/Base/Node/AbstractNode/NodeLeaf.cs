
namespace BehaviorTree
{
    /// <summary>
    /// leaf node
    /// </summary>
    [System.Serializable]
    public class NodeLeaf : NodeBase
    {
        public NodeLeaf():base()
        {
        }

        public override ResultType Execute()
        {
            return ResultType.Fail;
        }
    }
}