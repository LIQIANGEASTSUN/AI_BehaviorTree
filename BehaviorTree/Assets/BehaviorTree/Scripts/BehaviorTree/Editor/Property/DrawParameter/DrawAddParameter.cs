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
            string english = Localization.GetInstance().Format("English");
            behaviorParameter.parameterName = EditorGUILayout.TextField(english, behaviorParameter.parameterName);
            if (oldName.CompareTo(behaviorParameter.parameterName) != 0)
            {
                bool isNumOrAlp = IsNumOrAlp(behaviorParameter.parameterName);
                if (!isNumOrAlp)
                {
                    string parameterNameRules = Localization.GetInstance().Format("ParameterNameRules");
                    TreeNodeWindow.window.ShowNotification(parameterNameRules);
                    behaviorParameter.parameterName = oldName;
                }
            }
        }

        protected override void DrawParameterCnName(NodeParameter behaviorParameter)
        {
            string chinese = Localization.GetInstance().Format("Chinese");
            behaviorParameter.CNName = EditorGUILayout.TextField(chinese, behaviorParameter.CNName);
        }

        private static bool IsNumOrAlp(string str)
        {
            string pattern = @"^[a-zA-Z_][A-Za-z0-9_]*$";
            Match match = Regex.Match(str, pattern);
            return match.Success;
        }
    }
}