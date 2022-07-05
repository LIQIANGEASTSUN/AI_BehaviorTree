using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System;

namespace BehaviorTree
{
    public class DrawNodeParameter : DrawParameterBase
    {

        public override void Draw(NodeParameter behaviorParameter)
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUIEnableTool.Enable = false;
                    DrawIndex(behaviorParameter);
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
                    GUIEnableTool.Enable = ParameterNameSelectEnableHandle();
                    DrawParameterSelect(behaviorParameter);
                    GUIEnableTool.Enable = true;
                    GUIEnableTool.Enable = ParameterCompareEnableHandle();
                    DrawCompare(behaviorParameter);
                    GUIEnableTool.Enable = true;
                    GUIEnableTool.Enable = ParameterValueEnableHandle();
                    DrawParameterValue(behaviorParameter);
                    GUIEnableTool.Enable = true;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
            GUIEnableTool.Enable = true;
        }

        protected void DrawParameterSelect(NodeParameter behaviorParameter)
        {
            List<NodeParameter> parameterList = BehaviorDataController.Instance.BehaviorTreeData.parameterList;
            string[] parameterArr = new string[parameterList.Count];
            int index = -1;
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter p = parameterList[i];
                parameterArr[i] = p.CNName;
                if (behaviorParameter.parameterName.CompareTo(p.parameterName) == 0)
                {
                    index = i;
                }
            }

            int result = EditorGUILayout.Popup(index, parameterArr, GUILayout.ExpandWidth(true));
            if (result != index)
            {
                behaviorParameter.parameterName = parameterList[result].parameterName;
            }
        }

        private void DrawIndex(NodeParameter behaviorParameter)
        {
            EditorGUILayout.BeginHorizontal();
            {
                behaviorParameter.index = EditorGUILayout.IntField(behaviorParameter.index, GUILayout.Width(30));
            }
            EditorGUILayout.EndHorizontal();
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

        private bool ParameterNameSelectEnableHandle()
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

        private bool ParameterCompareEnableHandle()
        {
            return ParameterNameSelectEnableHandle(); 
        }

        private bool ParameterValueEnableHandle()
        {
            return ParameterNameSelectEnableHandle();
        }

    }
}

