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
                string classIdentification = Localization.GetInstance().Format("ClassIdentification");
                string identificationName = string.Format(classIdentification, nodeValue.identificationName);
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
                string parentNode = Localization.GetInstance().Format("ParentNode");
                string parentName = string.Format(parentNode, nodeValue.parentNodeID);
                EditorGUILayout.LabelField(parentName);
                string belongSubTree = Localization.GetInstance().Format("BelongSubTree");
                string parentSubTreeNodeId = string.Format(belongSubTree, nodeValue.parentSubTreeNodeId);
                EditorGUILayout.LabelField(parentSubTreeNodeId);
            }
        }

        protected virtual void Remark()
        {
            EditorGUILayout.BeginHorizontal();
            {
                string remark = Localization.GetInstance().Format("Remark");
                EditorGUILayout.LabelField(remark, GUILayout.Width(60));
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

