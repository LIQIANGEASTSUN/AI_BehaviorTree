
namespace BehaviorTree
{
    public class BehaviorDescriptController
    {
        private BehaviorDescriptModel _descriptModel;
        private BehaviorDescriptView _descriptView;

        public BehaviorDescriptController()
        {
            Init();
        }

        public void Init()
        {
            _descriptModel = new BehaviorDescriptModel();
            _descriptView = new BehaviorDescriptView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            BehaviorTreeData data = _descriptModel.GetData();
            _descriptView.Draw(data);
        }
    }
}