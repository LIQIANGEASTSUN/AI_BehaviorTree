
namespace BehaviorTree
{

    public class BehaviorNodeInspectorController
    {
        private BehaviorNodeInspectorModel _nodeInspectorModel;
        private BehaviorNodeInspectorView _nodeInspectorView;

        public BehaviorNodeInspectorController()
        {
            Init();
        }

        public void Init()
        {
            _nodeInspectorModel = new BehaviorNodeInspectorModel();
            _nodeInspectorView = new BehaviorNodeInspectorView();
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