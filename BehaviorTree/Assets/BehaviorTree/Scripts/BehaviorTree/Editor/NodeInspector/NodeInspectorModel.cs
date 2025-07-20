
namespace BehaviorTree
{
    public class NodeInspectorModel
    {
        public NodeValue GetCurrentSelectNode()
        {
            return DataController.Instance.CurrentNode;
        }
    }
}