
namespace BehaviorTree
{

    public class BehaviorFileHandleController
    {
        private BehaviorFileHandleView _fileHandleView;


        public BehaviorFileHandleController()
        {
            Init();
        }

        public void Init()
        {
            _fileHandleView = new BehaviorFileHandleView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            _fileHandleView.Draw();
        }
    }
}
