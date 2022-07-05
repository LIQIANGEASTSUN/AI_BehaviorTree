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
            string rootNode = Localization.GetInstance().Format("RootNode");
            bool value = EditorGUILayout.Toggle(new GUIContent(rootNode), nodeValue.isRootNode/*, GUILayout.Width(50)*/);
            if (value && !nodeValue.isRootNode)
            {
                Debug.LogError(rootNode);
                nodeValue.isRootNode = true;
                DataHandler dataHandler = new DataHandler();
                dataHandler.ChangeRootNode(nodeValue.id);
            }
            nodeValue.subTreeEntry = false;
        }

        private void SubTreeChildNode()
        {
            string subTreeEntryNode = Localization.GetInstance().Format("SubTreeEntryNode");
            bool value = EditorGUILayout.Toggle(new GUIContent(subTreeEntryNode), nodeValue.subTreeEntry/*, GUILayout.Width(50)*/);
            if (value && !nodeValue.subTreeEntry)
            {
                Debug.LogError(subTreeEntryNode);
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
                string syncMoveChild = Localization.GetInstance().Format("SyncMoveChild");
                nodeValue.moveWithChild = EditorGUILayout.Toggle(new GUIContent(syncMoveChild), nodeValue.moveWithChild);
            }
        }

        protected virtual void NodeFunctionDescript()
        {
            if ((nodeValue.NodeType != (int)NODE_TYPE.CONDITION && nodeValue.NodeType != (int)NODE_TYPE.ACTION))
            {
                GUILayout.Space(5);
                string functionComposite = Localization.GetInstance().Format("FunctionComposite");
                EditorGUILayout.LabelField(functionComposite);
                nodeValue.function = EditorGUILayout.TextArea(nodeValue.function, GUILayout.Height(250), GUILayout.Width(300));
            }
        }

    }

}
