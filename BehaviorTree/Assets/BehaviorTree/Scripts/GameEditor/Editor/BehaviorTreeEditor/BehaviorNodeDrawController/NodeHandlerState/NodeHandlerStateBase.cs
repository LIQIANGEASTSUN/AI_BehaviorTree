using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class NodeHandlerStateBase
    {
        protected NodeHandlerState _state;
        protected Vector2 mousePosition;
        protected List<NodeValue> _nodeList;
        protected NodeHandlerState _changeState = NodeHandlerState.None;

        public NodeHandlerStateBase(NodeHandlerState state)
        {
            _state = state;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExecute(NodeValue currentNode, List<NodeValue> nodeList)
        {
            _nodeList = nodeList;
        }

        public virtual void OnExit()
        {
            _changeState = NodeHandlerState.None;
        }

        // 获取鼠标所在位置的节点
        protected NodeValue GetMouseInNode(List<NodeValue> nodeList)
        {
            NodeValue selectNode = null;
            for (int i = 0; i < nodeList.Count; i++)
            {
                NodeValue nodeValue = nodeList[i];
                // If the mouse position is included in the Rect range of the node, it is considered a selectable node
                if (RectTool.RectTToRect(nodeValue.position).Contains(mousePosition))
                {
                    selectNode = nodeValue;
                    break;
                }
            }

            return selectNode;
        }

        public NodeHandlerState ChangeToState()
        {
            return _changeState;
        }


    }
}


