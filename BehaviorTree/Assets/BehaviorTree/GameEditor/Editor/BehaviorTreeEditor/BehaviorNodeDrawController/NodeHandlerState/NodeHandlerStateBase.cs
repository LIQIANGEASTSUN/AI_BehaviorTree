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
                // 如果鼠标位置 包含在 节点的 Rect 范围，则视为可以选择的节点
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


