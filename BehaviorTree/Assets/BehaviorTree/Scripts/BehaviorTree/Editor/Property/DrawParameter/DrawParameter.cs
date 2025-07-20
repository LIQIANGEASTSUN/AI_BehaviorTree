using GraphicTree;
using UnityEditor;

namespace BehaviorTree
{
    public class DrawParameter : DrawParameterBase
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
                string fileNameReadOnly = Localization.GetInstance().Format("FileNameReadOnly");
                TreeNodeWindow.window.ShowNotification(fileNameReadOnly);
            }
        }

        private bool DelEnableHandle()
        {
            if (DataController.Instance.CurrentOpenConfigSubTree())
            {
                return false;
            }
            BehaviorPlayType type = DataController.Instance.PlayState;
            if (type == BehaviorPlayType.PLAY || type == BehaviorPlayType.PAUSE)
            {
                return false;
            }
            return true;
        }

        private bool ParameterValueEnableHandle()
        {
            if (DataController.Instance.CurrentOpenConfigSubTree())
            {
                return false;
            }
            BehaviorPlayType type = DataController.Instance.PlayState;
            if (type == BehaviorPlayType.PLAY || type == BehaviorPlayType.PAUSE)
            {
                return false;
            }
            return true;
        }
    }
}