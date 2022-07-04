using System.Collections.Generic;

namespace BehaviorTree
{

    public class BehaviorDrawController
    {
        public BehaviorDrawModel _behaviorDrawModel = null;
        private BehaviorDrawView _behaviorDrawView = null;

        public void Init()
        {
            _behaviorDrawModel = new BehaviorDrawModel();
            _behaviorDrawView = new BehaviorDrawView();
            _behaviorDrawView.Init(this, _behaviorDrawModel);
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            NodeValue currentNode = _behaviorDrawModel.GetCurrentSelectNode();
            List<NodeValue> nodeList = _behaviorDrawModel.GetDrawNode();
            _behaviorDrawView.Draw(currentNode, nodeList);
        }

    }

}

