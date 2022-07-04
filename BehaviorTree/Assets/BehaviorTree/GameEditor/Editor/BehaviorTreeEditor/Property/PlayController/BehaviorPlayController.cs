
namespace BehaviorTree
{
    public enum BehaviorPlayType
    {
        INVALID = -1,
        PLAY    = 0,
        PAUSE   = 1,
        STOP    = 2,
    }

    public class BehaviorPlayController
    {
        private BehaviorPlayView _playView;

        public BehaviorPlayController()
        {
            Init();
        }

        private void Init()
        {
            _playView = new BehaviorPlayView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            _playView.Draw();
        }
        
    }

}


