using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System.Text.RegularExpressions;

namespace BehaviorTree
{
    public class DrawBehaviorAddParameter : DrawParameterBase
    {

        public override void Draw(NodeParameter behaviorParameter)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawParameterType(behaviorParameter);
                EditorGUILayout.BeginHorizontal();
                {
                    DrawParameterName(behaviorParameter);
                    DrawParameterValue(behaviorParameter);
                }
                EditorGUILayout.EndHorizontal();
                DrawParameterCnName(behaviorParameter);
            }
            EditorGUILayout.EndHorizontal();
        }

        protected override void DrawParameterName(NodeParameter behaviorParameter)
        {
            string oldName = behaviorParameter.parameterName;
            behaviorParameter.parameterName = EditorGUILayout.TextField("英文:", behaviorParameter.parameterName);
            if (oldName.CompareTo(behaviorParameter.parameterName) != 0)
            {
                bool isNumOrAlp = IsNumOrAlp(behaviorParameter.parameterName);
                if (!isNumOrAlp)
                {
                    string msg = string.Format("参数名只能包含:数字、字母、下划线，且数字不能放在第一个字符位置");
                    TreeNodeWindow.window.ShowNotification(msg);
                    behaviorParameter.parameterName = oldName;
                }
            }
        }

        protected override void DrawParameterCnName(NodeParameter behaviorParameter)
        {
            behaviorParameter.CNName = EditorGUILayout.TextField("中文", behaviorParameter.CNName);
        }

        private static bool IsNumOrAlp(string str)
        {
            string pattern = @"^[a-zA-Z_][A-Za-z0-9_]*$";
            Match match = Regex.Match(str, pattern);
            return match.Success;
        }

    }
}

