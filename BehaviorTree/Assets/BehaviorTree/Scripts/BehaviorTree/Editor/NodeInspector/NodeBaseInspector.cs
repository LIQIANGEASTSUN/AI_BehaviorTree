using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class NodeBaseInspector
    {
        protected NodeValue nodeValue;
        public void SetNodeValue(NodeValue nodeValue)
        {
            this.nodeValue = nodeValue;
        }

        /// <summary>
        /// The order of method calls cannot be changed
        /// </summary>
        public void Draw()
        {
            NodeName();
            NodeIdentification();
            EntryNode();
            ParentNode();
            Remark();
            Common();
        }

        protected virtual void NodeName()
        {
            string nodeName = nodeValue.nodeName;
            string msg = string.Format("{0}_{1}", nodeName, nodeValue.id);
            EditorGUILayout.LabelField(msg);
        }

        protected virtual void NodeIdentification()
        {
            if (!string.IsNullOrEmpty(nodeValue.identificationName))
            {
                string identificationName = $"类标识_{nodeValue.identificationName}";
                EditorGUILayout.LabelField(identificationName);
            }
        }

        protected virtual void EntryNode()
        {

        }

        protected virtual void ParentNode()
        {
            if (nodeValue.parentNodeID >= 0)
            {
                string parentName = $"父节点_{nodeValue.parentNodeID}";
                EditorGUILayout.LabelField(parentName);
                string parentSubTreeNodeId = $"所属子树ID:{nodeValue.parentSubTreeNodeId}";
                EditorGUILayout.LabelField(parentSubTreeNodeId);
            }
        }

        protected virtual void Remark()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("备注", GUILayout.Width(60));
                nodeValue.descript = EditorGUILayout.TextArea(nodeValue.descript, GUILayout.ExpandWidth(true));
            }
            EditorGUILayout.EndHorizontal();
        }

        // Some variables specific to the node
        protected virtual void Common()
        {

        }
    }
}

