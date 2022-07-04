using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class BehaviorDrawView
    {
        private BehaviorDrawController _drawController = null;
        private BehaviorDrawModel _behaviorDrawModel = null;

        private Vector3 scrollPos = Vector2.zero;
        private Rect scrollRect = new Rect(0, 0, 1500, 1000);
        private Rect contentRect = new Rect(0, 0, 3000, 2000);

        private NodeHandlerStateMachine _nodeHandlerStateMachine;
        private BehaviorNodeDraw _behaviorNodeDraw;

        public void Init(BehaviorDrawController drawController, BehaviorDrawModel model)
        {
            _drawController = drawController;
            _behaviorDrawModel = model;
            _nodeHandlerStateMachine = new NodeHandlerStateMachine();
            _behaviorNodeDraw = new BehaviorNodeDraw();
        }

        public void Draw(NodeValue currentNode, List<NodeValue> nodeList)
        {
            DrawTielt();

            Rect rect = GUILayoutUtility.GetRect(0f, 0, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            scrollRect = rect;

            contentRect.x = rect.x;
            contentRect.y = rect.y;

            _behaviorNodeDraw.SetNodeList(nodeList);
            //创建 scrollView  窗口  
            scrollPos = GUI.BeginScrollView(scrollRect, scrollPos, contentRect);
            {
                _nodeHandlerStateMachine.OnExecute(currentNode, nodeList);
                // 开始绘制节点 
                // 注意：必须在  BeginWindows(); 和 EndWindows(); 之间 调用 GUI.Window 才能显示
                TreeNodeWindow._drawWindowCallBack(_behaviorNodeDraw.DrawNodeWindowsCallBack);
                ResetScrollPos(nodeList);
            }
            GUI.EndScrollView();  //结束 ScrollView 窗口  
        }

        private void DrawTielt()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string[] optionArr = _behaviorDrawModel.GetOptionArr(dic);
            int option = GUILayout.Toolbar((optionArr.Length - 1), optionArr, EditorStyles.toolbarButton, GUILayout.Width(optionArr.Length * 200));
            if (option != (optionArr.Length - 1))
            {
                int nodeId = dic[optionArr[option]];
                BehaviorDataController.Instance.CurrentOpenSubTree = nodeId;
            }
        }

        private void ResetScrollPos(List<NodeValue> nodeList)
        {
            float maxRight = -1;
            float maxBottom = -1;
            for (int i = 0; i < nodeList.Count; ++i)
            {
                NodeValue nodeValue = nodeList[i];
                float right = nodeValue.position.x + nodeValue.position.width + 50;
                if (right > maxRight)
                {
                    maxRight = right;
                    contentRect.width = maxRight;
                }

                float bottom = nodeValue.position.y + nodeValue.position.height + 50;
                if (bottom > maxBottom)
                {
                    maxBottom = bottom;
                    contentRect.height = maxBottom;
                }
            }
        }
    }

}