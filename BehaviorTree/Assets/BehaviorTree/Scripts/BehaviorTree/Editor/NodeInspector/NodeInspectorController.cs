
namespace BehaviorTree
{

    public class NodeInspectorController
    {
        private NodeInspectorModel _nodeInspectorModel;
        private NodeInspectorView _nodeInspectorView;

        public NodeInspectorController()
        {
            Init();
        }

        public void Init()
        {
            _nodeInspectorModel = new NodeInspectorModel();
            _nodeInspectorView = new NodeInspectorView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            NodeValue nodeValue = _nodeInspectorModel.GetCurrentSelectNode();
            _nodeInspectorView.Draw(nodeValue);
        }
    }

}