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
        /// Draw 中的方法调用顺序不能改
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
                string identificationName = string.Format("类标识_{0}", nodeValue.identificationName);
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
                string parentName = string.Format("父节点_{0}", nodeValue.parentNodeID);
                EditorGUILayout.LabelField(parentName);
                string parentSubTreeNodeId = string.Format("所属SubTreeID:{0}", nodeValue.parentSubTreeNodeId);
                EditorGUILayout.LabelField(parentSubTreeNodeId);
            }
        }

        protected virtual void Remark()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("备注", GUILayout.Width(30));
                nodeValue.descript = EditorGUILayout.TextArea(nodeValue.descript, GUILayout.ExpandWidth(true));
            }
            EditorGUILayout.EndHorizontal();
        }

        // 具体节点特有的一些变量
        protected virtual void Common()
        {

        }
    }
}

