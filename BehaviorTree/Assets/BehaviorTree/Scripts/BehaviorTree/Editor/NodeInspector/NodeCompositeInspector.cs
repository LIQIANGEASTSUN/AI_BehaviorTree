using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// Composite nodes: No special handling is implemented here
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
            bool value = EditorGUILayout.Toggle(new GUIContent("有多个根节点"), nodeValue.isRootNode/*, GUILayout.Width(50)*/);
            if (value && !nodeValue.isRootNode)
            {
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
                string functionDescript = NodeDescript.GetFunction((NODE_TYPE)nodeValue.NodeType);
                functionDescript = functionDescript.Replace("\\n", "\n");
                EditorGUILayout.TextArea(functionDescript, GUILayout.Height(350), GUILayout.Width(300));
            }
        }
    }
}
