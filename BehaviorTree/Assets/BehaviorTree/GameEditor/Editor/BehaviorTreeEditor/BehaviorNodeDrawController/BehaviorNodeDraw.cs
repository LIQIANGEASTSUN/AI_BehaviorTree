using GraphicTree;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorNodeDraw
    {
        private List<NodeValue> _nodeList;

        public void SetNodeList(List<NodeValue> nodeList)
        {
            _nodeList = nodeList;
        }

        // 绘制节点
        public void DrawNodeWindowsCallBack()
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                NodeValue nodeValue = _nodeList[i];
                GUIEnableTool.Enable = !BehaviorDataController.Instance.CurrentOpenConfigSubTree();
                string name = string.Format("{0}", nodeValue.nodeName);
                Rect rect = GUI.Window(i, RectTool.RectTToRect(nodeValue.position), DrawNodeWindow, new GUIContent(name, name));
                if (!BehaviorDataController.Instance.CurrentOpenConfigSubTree())
                {
                    SyncChildNodePosition(nodeValue, rect);
                }
                GUIEnableTool.Enable = true;

                if (nodeValue.NodeType != (int)NODE_TYPE.SUB_TREE)
                {
                    DrawToChildCurve(nodeValue);
                }
            }

            SortChild(_nodeList);
        }

        private void DrawNodeWindow(int id)
        {
            if (id >= _nodeList.Count)
            {
                return;
            }
            NodeValue nodeValue = _nodeList[id];
            NodeDrawEditor.Draw(nodeValue, BehaviorDataController.Instance.CurrentSelectId);
            GUI.DragWindow();
        }

        private void SyncChildNodePosition(NodeValue nodeValue, Rect rect)
        {
            Vector2 nodePos = new Vector2(nodeValue.position.x, nodeValue.position.y);
            nodeValue.position = RectTool.RectToRectT(rect);
            CheckNodePosition(nodeValue);

            if (nodeValue.moveWithChild)
            {
                Vector2 offset = new Vector2(nodeValue.position.x, nodeValue.position.y) - nodePos;
                MoveChildNodePosition(nodeValue, offset);
            }
        }

        private void MoveChildNodePosition(NodeValue nodeValue, Vector2 offset)
        {
            HashSet<int> childHash = new HashSet<int>();
            Queue<NodeValue> queue = new Queue<NodeValue>();
            queue.Enqueue(nodeValue);
            while (queue.Count > 0)
            {
                NodeValue node = queue.Dequeue();
                for (int i = 0; i < node.childNodeList.Count; ++i)
                {
                    int childId = node.childNodeList[i];
                    NodeValue childNode = BehaviorDataController.Instance.GetNode(childId);
                    childNode.position.x += offset.x;
                    childNode.position.y += offset.y;

                    if (childHash.Contains(childNode.id)) // 检测环
                    {
                        UnityEngine.Debug.LogError(node.id + "    " + childNode.id);
                        break;
                    }
                    childHash.Add(childNode.id);
                    queue.Enqueue(childNode);
                }
            }
        }

        /// 每帧绘制从 节点到所有子节点的连线
        private void DrawToChildCurve(NodeValue nodeValue)
        {
            for (int i = nodeValue.childNodeList.Count - 1; i >= 0; --i)
            {
                int childId = nodeValue.childNodeList[i];
                NodeValue childNode = BehaviorDataController.Instance.GetNode(childId);
                if (null == nodeValue || null == childNode)
                {
                    continue;
                }
                if (BehaviorDataController.Instance.RunTimeInvalidSubTreeHash.Count > 0 && BehaviorDataController.Instance.RunTimeInvalidSubTreeHash.Contains(childNode.id))
                {
                    continue;
                }

                DrawNodeCurveTools.DrawNodeCurve(nodeValue.position, childNode.position);
                DrawLabel(i.ToString(), nodeValue.position, childNode.position);
            }
        }

        public static void DrawLabel(string msg, RectT start, RectT end)
        {
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);

            Vector2 pos = (startPos + endPos) * 0.5f;// + (endPos - startPos) * 0.1f;

            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.green;
            Handles.Label(pos, msg, style);
        }

        private void CheckNodePosition(NodeValue nodeValue)
        {
            float xConst = 330;
            float yConst = 30;
            if (nodeValue.position.x < xConst)
            {
                nodeValue.position.x = xConst;
            }

            if (nodeValue.position.y < yConst)
            {
                nodeValue.position.y = yConst;
            }
        }

        private void SortChild(List<NodeValue> nodeList)
        {
            for (int i = 0; i < nodeList.Count; ++i)
            {
                NodeValue nodeValue = nodeList[i];

                nodeValue.childNodeList.Sort((a, b) =>
                {
                    NodeValue nodeA = BehaviorDataController.Instance.GetNode(a);
                    NodeValue nodeB = BehaviorDataController.Instance.GetNode(b);
                    return (int)(nodeA.position.x - nodeB.position.x);
                });
            }
        }

    }

}
