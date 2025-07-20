using UnityEngine;
using UnityEditor;
using GraphicTree;
using System;
using System.Collections.Generic;

namespace BehaviorTree
{

    /// <summary>
    /// Leaf Node
    /// </summary>
    public abstract class NodeLeafInspector : NodeBaseInspector
    {
        private Color32[] colorArr = new Color32[] { new Color32(178, 226, 221, 255), new Color32(220, 226, 178, 255), new Color32(209, 178, 226, 255), new Color32(178, 185, 226, 255) };
        protected HashSet<string> _groupHash = new HashSet<string>();
        protected DrawNodeParameter _drawNodeParameter = new DrawNodeParameter();

        protected override void NodeName()
        {
            int index = EnumNames.GetEnumIndex<NODE_TYPE>((NODE_TYPE)nodeValue.NodeType);
            string name = EnumNames.GetEnumName<NODE_TYPE>(index);
            name = string.Format("{0}_{1}", name, nodeValue.id);
            EditorGUILayout.LabelField(name);
            base.NodeName();
        }

        private Vector2 scrollPos = Vector2.zero;
        protected override void Common()
        {
            DrawNodeParameter();
        }

        protected virtual HashSet<string> GroupHash()
        {
            return _groupHash;
        }

        protected virtual void DrawNodeParameter()
        {
            GUIEnableTool.Enable = true;

            for (int i = 0; i < nodeValue.parameterList.Count; ++i)
            {
                nodeValue.parameterList[i].index = i;
            }

            GUIEnableTool.Enable = !DataController.Instance.CurrentOpenConfigSubTree();
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                EditorGUILayout.LabelField("参数");

                int height = (nodeValue.parameterList.Count * 58);
                height = height <= 400 ? height : 400;
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(height));
                {
                    GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);
                    DrawAllParameter();
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            DrawAddParameter(nodeValue);
            GUIEnableTool.Enable = true;
        }

        private void DrawAllParameter()
        {
            HashSet<string> groupHash = GroupHash();
            for (int i = 0; i < nodeValue.parameterList.Count; ++i)
            {
                NodeParameter behaviorParameter = nodeValue.parameterList[i];

                Action DelCallBack = () =>
                {
                    DataNodeHandler dataNodeHandler = new DataNodeHandler();
                    dataNodeHandler.NodeDelParameter(nodeValue.id, behaviorParameter);
                };

                Color color = Color.white;
                if (groupHash.Contains(behaviorParameter.parameterName))
                {
                    color = colorArr[0];
                }

                GUI.backgroundColor = color;
                EditorGUILayout.BeginVertical("box");
                {
                    string parameterName = behaviorParameter.parameterName;
                    NodeParameter tempParameter = behaviorParameter.Clone();
                    _drawNodeParameter.SetDelCallBack(DelCallBack);
                    //tempParameter = DrawParameter.Draw(behaviorParameter, DrawParameterType.NODE_PARAMETER, DelCallBack);
                     _drawNodeParameter.Draw(behaviorParameter);
                    tempParameter = behaviorParameter;

                    if (parameterName.CompareTo(behaviorParameter.parameterName) != 0)
                    {
                        DataNodeHandler dataNodeHandler = new DataNodeHandler();
                        dataNodeHandler.NodeParameterChange(nodeValue.id, parameterName, behaviorParameter.parameterName);
                    }
                    else
                    {
                        behaviorParameter.CloneFrom(tempParameter);
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }

        private void DrawAddParameter(NodeValue nodeValue)
        {
            EditorGUILayout.BeginVertical("box");
            {
                GUIEnableTool.Enable = !DataController.Instance.CurrentOpenConfigSubTree();
                if (GUILayout.Button("添加条件"))
                {
                    AddParameter(nodeValue);
                }
                GUIEnableTool.Enable = true;
            }
            EditorGUILayout.EndVertical();
        }

        private void AddParameter(NodeValue nodeValue)
        {
            if (DataController.Instance.BehaviorTreeData.parameterList.Count <= 0)
            {
                if (TreeNodeWindow.window != null)
                {
                    TreeNodeWindow.window.ShowNotification("没有参数可添加，请先添加参数");
                }
                return;
            }

            NodeParameter behaviorParameter = GetEnableAddParameter(nodeValue);
            if (null != behaviorParameter)
            {
                DataNodeHandler dataNodeHandler = new DataNodeHandler();
                dataNodeHandler.NodeAddParameter(nodeValue, behaviorParameter);
            }
            else
            {
                TreeNodeWindow.window.ShowNotification("没有参数可添加，请先添加参数");
            }
        }

        private NodeParameter GetEnableAddParameter(NodeValue nodeValue)
        {
            List<NodeParameter> parameterList = DataController.Instance.BehaviorTreeData.parameterList;
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter nodeParameter = parameterList[i];
                NodeParameter behaviorParameter = nodeValue.parameterList.Find((a) => { return a.parameterName.CompareTo(nodeParameter.parameterName) == 0; });
                if (null == behaviorParameter)
                {
                    return nodeParameter;
                }
            }
            return null;
        }

    }

}
