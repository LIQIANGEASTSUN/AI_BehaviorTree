using GraphicTree;
using UnityEditor;
using System.Text.RegularExpressions;

namespace BehaviorTree
{
    public class DrawAddParameter : DrawParameterBase
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
            behaviorParameter.parameterName = EditorGUILayout.TextField("English", behaviorParameter.parameterName);
            if (oldName.CompareTo(behaviorParameter.parameterName) != 0)
            {
                bool isNumOrAlp = IsNumOrAlp(behaviorParameter.parameterName);
                if (!isNumOrAlp)
                {
                    TreeNodeWindow.window.ShowNotification("参数名只能包含:数字、字母、下划线，且数字不能放在第一个字符位置");
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