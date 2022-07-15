using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class NodeHandlerStateNormal : NodeHandlerStateBase
    {

        public NodeHandlerStateNormal() : base(NodeHandlerState.Normal)
        {

        }

        public override void OnExecute(NodeValue currentNode, List<NodeValue> nodeList)
        {
            base.OnExecute(currentNode, nodeList);
            Event _event = Event.current;
            if (_event.type != EventType.MouseDown)
            {
                return;
            }

            mousePosition = _event.mousePosition;
            if (_event.button == 0)  // 鼠标左键
            {
                NodeValue nodeValue = GetMouseInNode(nodeList);
                ClickNode(nodeValue);
            }
            else if (_event.button == 1)  // 鼠标右键
            {
                NodeValue nodeValue = GetMouseInNode(nodeList);
                ShowMenu(currentNode, nodeValue);
            }
        }

        private int _lastClickNodeTime = 0;
        private void ClickNode(NodeValue nodeValue)
        {
            if (null == nodeValue)
            {
                return;
            }

            BehaviorDataController.behaviorChangeSelectId(nodeValue.id);

            if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
            {
                int currentTime = (int)(Time.realtimeSinceStartup * 1000);
                if (currentTime - _lastClickNodeTime <= 200)
                {
                    BehaviorDataController.Instance.CurrentOpenSubTree = nodeValue.id;
                }
                _lastClickNodeTime = currentTime;
            }
        }

        private void ShowMenu(NodeValue currentNode, NodeValue nodeValue)
        {
            if (null == nodeValue)  // 鼠标按下位置没有节点
            {
                MouseRightDownEmptyNode();
            }
            else
            {
                MouseRightDownOnNode(currentNode, nodeValue);
            }
            Event.current.Use();
        }

        private void MouseRightDownEmptyNode()
        {
            GenericMenu menu = new GenericMenu();
            List<Node_Draw_Info> nodeList = BehaviorNodeDrawInfoController.GetInstance().InfoList;
            for (int i = 0; i < nodeList.Count; ++i)
            {
                Node_Draw_Info draw_Info = nodeList[i];
                for (int j = 0; j < draw_Info._nodeArr.Count; ++j)
                {
                    KeyValuePair<string, Node_Draw_Info_Item> kv = draw_Info._nodeArr[j];
                    string itemName = string.Format("{0}", kv.Key);
                    GenericMenuAddItem(menu, new GUIContent(itemName), AddNodeCallBack, kv.Value);
                }
            }

            menu.ShowAsContext();
        }

        private void AddNodeCallBack(object userData)
        {
            DataHandler dataHandler = new DataHandler();
            dataHandler.AddNode((Node_Draw_Info_Item)userData, mousePosition, BehaviorDataController.Instance.CurrentOpenSubTree);
        }

        private void MouseRightDownOnNode(NodeValue currentNode, NodeValue nodeValue)
        {
            GenericMenu menu = new GenericMenu();
            if (null != currentNode && nodeValue.id == currentNode.id && (NODE_TYPE)nodeValue.NodeType < NODE_TYPE.CONDITION)
            {
                // 连线子节点
                string makeTransition = Localization.GetInstance().Format("Make Transition");
                GenericMenuAddItem(menu, new GUIContent(makeTransition), MakeTransition, nodeValue.id);
                menu.AddSeparator("");
            }

            // 删除节点
            string deleteNode = Localization.GetInstance().Format("Delete Node");
            GenericMenuAddItem(menu, new GUIContent(deleteNode), DeleteNode, nodeValue.id);

            if (nodeValue.parentNodeID >= 0 && !nodeValue.subTreeEntry)
            {
                string removeParent = Localization.GetInstance().Format("Remove Parent");
                GenericMenuAddItem(menu, new GUIContent(removeParent), RemoveParentNode, nodeValue.id);
            }
            menu.ShowAsContext();
        }

        private void MakeTransition(object userData)
        {
            _changeState = NodeHandlerState.MakeTransition;
        }

        private void GenericMenuAddItem(GenericMenu menu, GUIContent content, GenericMenu.MenuFunction2 func, object userData)
        {
            if (!BehaviorDataController.Instance.CurrentOpenConfigSubTree())
            {
                menu.AddItem(content, false, func, userData);
            }
            else
            {
                menu.AddDisabledItem(content);
            }
        }

        // 删除节点
        private void DeleteNode(object userData)
        {
            string tips = Localization.GetInstance().Format("Tips");
            string deleteConfirm = Localization.GetInstance().Format("DeleteNodeConfirm");
            string yes = Localization.GetInstance().Format("Yes");
            string no = Localization.GetInstance().Format("No");
            if (EditorUtility.DisplayDialog(tips, deleteConfirm, yes, no))
            {
                int nodeId = (int)userData;
                DataHandler dataHandler = new DataHandler();
                dataHandler.DeleteNode(nodeId);
            }
        }

        // 移除父节点
        private void RemoveParentNode(object userData)
        {
            string tips = Localization.GetInstance().Format("Tips");
            string deleteConfirm = Localization.GetInstance().Format("DeleteParentConfirm");
            string yes = Localization.GetInstance().Format("Yes");
            string no = Localization.GetInstance().Format("No");
            if (EditorUtility.DisplayDialog(tips, deleteConfirm, yes, no))
            {
                int nodeId = (int)userData;
                DataHandler dataHandler = new DataHandler();
                dataHandler.RemoveParentNode(nodeId);
            }
        }
    }

}
