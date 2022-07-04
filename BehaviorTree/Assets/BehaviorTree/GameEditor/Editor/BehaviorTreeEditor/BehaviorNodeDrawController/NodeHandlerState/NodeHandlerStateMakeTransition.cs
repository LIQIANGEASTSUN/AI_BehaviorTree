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

            if (_event.type == EventType.MouseDown && (_event.button == 0))  // 鼠标左键
            {
                NodeValue nodeValue = GetMouseInNode(nodeList);
                // 如果按下鼠标时，选中了一个节点，则将 新选中根节点 添加为 selectNode 的子节点
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
