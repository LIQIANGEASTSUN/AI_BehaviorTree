using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;

namespace BehaviorTree
{
    public class DrawBehaviorParameter : DrawParameterBase
    {

        public override void Draw(NodeParameter behaviorParameter)
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUIEnableTool.Enable = false;
                    DrawParameterType(behaviorParameter);
                    DrawParameterName(behaviorParameter);
                    GUIEnableTool.Enable = true;

                    GUIEnableTool.Enable = DelEnableHandle();
                    DrawDelBtn(DelCallBack);
                    GUIEnableTool.Enable = true;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    GUIEnableTool.Enable = false;
                    DrawParameterCnName(behaviorParameter);
                    GUIEnableTool.Enable = true;

                    GUIEnableTool.Enable = ParameterValueEnableHandle();
                    DrawParameterValue(behaviorParameter);
                    GUIEnableTool.Enable = true;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();

        }

        protected override void DrawParameterCnName(NodeParameter behaviorParameter)
        {
            string cnName = EditorGUILayout.TextField(behaviorParameter.CNName);
            if (cnName.CompareTo(behaviorParameter.CNName) != 0)
            {
                TreeNodeWindow.window.ShowNotification("此处字段名只支持查看，不能修改");
            }
        }

        private bool DelEnableHandle()
        {
            if (BehaviorDataController.Instance.CurrentOpenConfigSubTree())
            {
                return false;
            }
            BehaviorPlayType type = BehaviorDataController.Instance.PlayState;
            if (type == BehaviorPlayType.PLAY || type == BehaviorPlayType.PAUSE)
            {
                return false;
            }
            return true;
        }

        private bool ParameterValueEnableHandle()
        {
            if (BehaviorDataController.Instance.CurrentOpenConfigSubTree())
            {
                return false;
            }
            BehaviorPlayType type = BehaviorDataController.Instance.PlayState;
            if (type == BehaviorPlayType.PLAY || type == BehaviorPlayType.PAUSE)
            {
                return false;
            }
            return true;
        }
    }

}
