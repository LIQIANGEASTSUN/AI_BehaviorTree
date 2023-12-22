using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeHandlerState
    {
        None,
        Normal,
        MakeTransition,
    }

    public class NodeHandlerStateMachine
    {
        private Dictionary<NodeHandlerState, NodeHandlerStateBase> _stateDic = new Dictionary<NodeHandlerState, NodeHandlerStateBase>();
        private NodeHandlerStateBase _currentState;

        public NodeHandlerStateMachine()
        {
            _stateDic[NodeHandlerState.Normal] = new NodeHandlerStateNormal();
            _stateDic[NodeHandlerState.MakeTransition] = new NodeHandlerStateMakeTransition();

            ChangeState(NodeHandlerState.Normal);
        }

        public void OnExecute(NodeValue currentNode, List<NodeValue> nodeList)
        {
            if (null != _currentState)
            {
                _currentState.OnExecute(currentNode, nodeList);
                NodeHandlerState changeState = _currentState.ChangeToState();
                ChangeState(changeState);
            }
        }

        public void ChangeState(NodeHandlerState state)
        {
            if (!_stateDic.ContainsKey(state))
            {
                return;
            }

            if (null != _currentState)
            {
                _currentState.OnExit();
            }

            _currentState = _stateDic[state];
            _currentState.OnEnter();
        }

    }
}

