using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{

    public class BehaviorDescriptView
    {

        public void Draw(BehaviorTreeData data)
        {
            Rect rect = GUILayoutUtility.GetRect(0f, 0, GUILayout.ExpandWidth(true));
            EditorGUILayout.BeginVertical("box");
            {
                data.descript = EditorGUILayout.TextArea(data.descript, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }
            EditorGUILayout.EndVertical();
        }
    }

}