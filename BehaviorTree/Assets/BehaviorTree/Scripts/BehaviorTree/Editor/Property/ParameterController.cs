﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using GraphicTree;

namespace BehaviorTree
{
    public class ParameterController
    {
        private BehaviorParameterModel _parameterModel;
        private BehaviorParameterView _parameterView;

        public ParameterController()
        {
            Init();
        }

        public void Init()
        {
            _parameterModel = new BehaviorParameterModel();
            _parameterView = new BehaviorParameterView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            List<NodeParameter> parameterList = _parameterModel.ParameterList;
            _parameterView.Draw(parameterList);
        }
    }

    public class BehaviorParameterModel
    {
        public BehaviorParameterModel()
        {
        }

        public List<NodeParameter> ParameterList
        {
            get
            {
                return DataController.Instance.BehaviorTreeData.parameterList;
            }
        }
    }

    public class BehaviorParameterView
    {
        private Vector2 scrollPos = Vector2.zero;
        private DrawParameter drawBehaviorParameter = new DrawParameter();
        public void Draw(List<NodeParameter> parameterList)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("全局变量", GUILayout.Width(100));
                if (GUILayout.Button("导入变量"))
                {
                    DataImportParameter behaviorDataImportParameter = new DataImportParameter();
                    behaviorDataImportParameter.ImportParameter(DataController.Instance.BehaviorTreeData);
                }
                if (GUILayout.Button("删除未使用变量"))
                {
                    DataRemoveUnUseParameter behaviorDataRemoveUnUseParameter = new DataRemoveUnUseParameter();
                    behaviorDataRemoveUnUseParameter.RemoveParameter(DataController.Instance.BehaviorTreeData);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                EditorGUILayout.LabelField("条件参数");
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
                {
                    GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);
                    for (int i = 0; i < parameterList.Count; ++i)
                    {
                        NodeParameter behaviorParameter = parameterList[i];

                        Action DelCallBack = () =>
                        {
                            DataHandler dataHandler = new DataHandler();
                            dataHandler.DataDelGlobalParameter(behaviorParameter);
                        };

                        drawBehaviorParameter.SetDelCallBack(DelCallBack);

                        EditorGUILayout.BeginVertical("box");
                        {
                            //behaviorParameter = DrawParameter.Draw(behaviorParameter, DrawParameterType.BEHAVIOR_PARAMETER, DelCallBack);
                            drawBehaviorParameter.Draw(behaviorParameter);
                        }
                        EditorGUILayout.EndVertical();
                    }
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");
            {
                DrawAddParameter();
            }
            EditorGUILayout.EndVertical();
        }

        private NodeParameter newAddParameter = new NodeParameter();
        private DrawAddParameter _drawBehaviorAddParameter = new DrawAddParameter();
        private void DrawAddParameter()
        {
            if (null == newAddParameter)
            {
                newAddParameter = new NodeParameter();
            }

            EditorGUILayout.BeginVertical("box");
            {
                //newAddParameter = DrawParameter.Draw(newAddParameter, DrawParameterType.BEHAVIOR_PARAMETER_ADD, null);
                _drawBehaviorAddParameter.Draw(newAddParameter);
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);

            if (GUILayout.Button("添加条件"))
            {
                DataHandler dataHandler = new DataHandler();
                dataHandler.DataAddGlobalParameter(newAddParameter);
            }
        }
    }
}