using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// 组合节点:没有特殊处理的在这里实现
    /// </summary>
    public class NodeCompositeInspector : NodeBaseInspector
    {

        protected override void EntryNode()
        {
            if (nodeValue.parentSubTreeNodeId < 0)
            {
                ConfigNode();
            }
            else
            {
                SubTreeChildNode();
            }
        }

        private void ConfigNode()
        {
            bool value = EditorGUILayout.Toggle(new GUIContent("根节点"), nodeValue.isRootNode/*, GUILayout.Width(50)*/);
            if (value && !nodeValue.isRootNode)
            {
                Debug.LogError("跟节点");
                nodeValue.isRootNode = true;
                DataHandler dataHandler = new DataHandler();
                dataHandler.ChangeRootNode(nodeValue.id);
            }
            nodeValue.subTreeEntry = false;
        }

        private void SubTreeChildNode()
        {
            bool value = EditorGUILayout.Toggle(new GUIContent("子树入口节点"), nodeValue.subTreeEntry/*, GUILayout.Width(50)*/);
            if (value && !nodeValue.subTreeEntry)
            {
                Debug.LogError("子树入口节点");
                nodeValue.subTreeEntry = value;
                DataHandler dataHandler = new DataHandler();
                dataHandler.ChangeSubTreeEntryNode(nodeValue.parentSubTreeNodeId, nodeValue.id);
            }
            nodeValue.isRootNode = false;
        }

        protected override void Common()
        {
            AsyncChild();
            NodeFunctionDescript();
        }

        protected void AsyncChild()
        {
            if (nodeValue.childNodeList.Count > 0)
            {
                nodeValue.moveWithChild = EditorGUILayout.Toggle(new GUIContent("同步移动子节点"), nodeValue.moveWithChild);
            }
        }

        protected virtual void NodeFunctionDescript()
        {
            if ((nodeValue.NodeType != (int)NODE_TYPE.CONDITION && nodeValue.NodeType != (int)NODE_TYPE.ACTION))
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField("组合节点功能描述");
                nodeValue.function = EditorGUILayout.TextArea(nodeValue.function, GUILayout.Height(250), GUILayout.Width(300));
            }
        }

    }

}
