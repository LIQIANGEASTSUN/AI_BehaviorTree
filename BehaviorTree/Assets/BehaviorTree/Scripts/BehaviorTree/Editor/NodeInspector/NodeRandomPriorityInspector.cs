using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// Random priority node
    /// </summary>
    public class NodeRandomPriorityInspector : NodeCompositeInspector
    {

        protected override void Common()
        {
            base.Common();

            if (nodeValue.childNodeList.Count <= 0)
            {
                return;
            }

            EditorGUILayout.BeginVertical("box");
            {
                for (int i = 0; i < nodeValue.childNodeList.Count; ++i)
                {
                    NodeValue childNode = DataController.Instance.GetNode(nodeValue.childNodeList[i]);
                    string nodeMsg = $"子节点:{childNode.id} 权值:";
                    childNode.priority = EditorGUILayout.IntField(nodeMsg, childNode.priority);
                    childNode.priority = Mathf.Max(1, childNode.priority);
                }
            }
            EditorGUILayout.EndVertical();
        }

    }

}

