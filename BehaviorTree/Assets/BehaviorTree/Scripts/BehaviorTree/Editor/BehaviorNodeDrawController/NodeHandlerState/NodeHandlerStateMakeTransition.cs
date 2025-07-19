using GraphicTree;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class NodeHandlerStateMakeTransition : NodeHandlerStateBase
    {
        public NodeHandlerStateMakeTransition() : base(NodeHandlerState.MakeTransition)
        {

        }

        public override void OnExecute(NodeValue currentNode, List<NodeValue> nodeList)
        {
            base.OnExecute(currentNode, nodeList);

            Event _event = Event.current;
            mousePosition = _event.mousePosition;

            if (_event.type == EventType.MouseDown && (_event.button == 0))  // The left mouse button
            {
                NodeValue nodeValue = GetMouseInNode(nodeList);
                // If you hold down the mouse and a node is selected,Add the newly selected root node as a child node of the selectNode
                if (null != nodeValue && currentNode.id != nodeValue.id)
                {
                    DataNodeHandler dataNodeHandler = new DataNodeHandler();
                    dataNodeHandler.NodeAddChild(currentNode.id, nodeValue.id);
                }
                _changeState = NodeHandlerState.Normal;
            }

            if (currentNode != null)
            {
                RectT mouseRect = new RectT(mousePosition.x, mousePosition.y, 10, 10);
                DrawNodeCurveTools.DrawNodeCurve(currentNode.position, mouseRect);
            }
        }

    }

}
