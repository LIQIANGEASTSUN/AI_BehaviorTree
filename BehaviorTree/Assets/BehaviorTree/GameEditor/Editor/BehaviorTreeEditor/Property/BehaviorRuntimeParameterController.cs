using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphicTree;

namespace BehaviorTree
{

    public class BehaviorRuntimeParameterController
    {
        private BehaviorRuntimeParameterModel _runtimeParameterModel;
        private BehaviorRuntimeParameterView _runtimeParameterView;

        public BehaviorRuntimeParameterController()
        {
            Init();
        }

        public void Init()
        {
            _runtimeParameterModel = new BehaviorRuntimeParameterModel();
            _runtimeParameterView = new BehaviorRuntimeParameterView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            if (null != BehaviorRunTime.Instance.BehaviorTreeEntity)
            {
                List<NodeParameter> parameterList = BehaviorRunTime.Instance.BehaviorTreeEntity.ConditionCheck.GetAllParameter();
                _runtimeParameterModel.AddParameter(parameterList);
            }

            _runtimeParameterView.Draw(_runtimeParameterModel.ParameterList);
        }

    }

    public class BehaviorRuntimeParameterModel
    {
        private List<NodeParameter> _parameterList = new List<NodeParameter>();

        public BehaviorRuntimeParameterModel()
        {
        }

        public void AddParameter(List<NodeParameter> parameterList)
        {
            _parameterList = parameterList;
        }

        public List<NodeParameter> ParameterList
        {
            get
            {
                return _parameterList;
            }
        }

    }

    public class BehaviorRuntimeParameterView
    {
        private Vector2 scrollPos = Vector2.zero;
        private DrawBehaviorParameter drawBehaviorParameter = new DrawBehaviorParameter();

        public void Draw(List<NodeParameter> parameterList)
        {
            EditorGUILayout.LabelField("运行时变量");

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                EditorGUILayout.LabelField("条件参数");
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
                {
                    GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);
                    for (int i = 0; i < parameterList.Count; ++i)
                    {
                        NodeParameter behaviorParameter = parameterList[i];
                        EditorGUILayout.BeginVertical("box");
                        {
                            drawBehaviorParameter.Draw(behaviorParameter);
                        }
                        EditorGUILayout.EndVertical();
                    }
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

    }

}





