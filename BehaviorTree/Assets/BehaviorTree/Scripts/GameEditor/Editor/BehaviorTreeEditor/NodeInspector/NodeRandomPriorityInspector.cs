using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// 随机权重节点
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
                    NodeValue childNode = BehaviorDataController.Instance.GetNode(nodeValue.childNodeList[i]);
                    string childNodePriority = Localization.GetInstance().Format("ChildNodePriority");
                    string nodeMsg = string.Format(childNodePriority, childNode.id);
                    childNode.priority = EditorGUILayout.IntField(nodeMsg, childNode.priority);
                    childNode.priority = Mathf.Max(1, childNode.priority);
                }
            }
            EditorGUILayout.EndVertical();
        }

    }

}

