using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System;

namespace BehaviorTree
{
    public abstract class DrawParameterBase
    {
        protected Action _delCallBack;
        public void SetDelCallBack(Action callBack)
        {
            _delCallBack = callBack;
        }

        protected void DelCallBack()
        {
            if (null != _delCallBack)
            {
                _delCallBack();
            }
        }

        public abstract void Draw(NodeParameter behaviorParameter);

        protected void DrawParameterType(NodeParameter behaviorParameter)
        {
            string[] parameterNameArr = EnumNames.GetEnumNames<ParameterType>();
            int index = EnumNames.GetEnumIndex<ParameterType>((ParameterType)(behaviorParameter.parameterType));
            index = EditorGUILayout.Popup(index, parameterNameArr);
            behaviorParameter.parameterType = (int)EnumNames.GetEnum<ParameterType>(index);
        }

        protected virtual void DrawParameterName(NodeParameter behaviorParameter)
        {
            EditorGUILayout.BeginHorizontal();
            {
                behaviorParameter.parameterName = EditorGUILayout.TextField(behaviorParameter.parameterName);
            }
            EditorGUILayout.EndHorizontal();
        }

        protected virtual void DrawParameterCnName(NodeParameter behaviorParameter)
        {
            behaviorParameter.CNName = EditorGUILayout.TextField(behaviorParameter.CNName);
        }

        protected void DrawDelBtn(Action DelCallBack)
        {
            string deleteFile = Localization.GetInstance().Format("DeleteFile");
            if (GUILayout.Button(deleteFile))
            {
                if (null != DelCallBack)
                {
                    DelCallBack();
                }
            }
        }

        protected void DrawParameterValue(NodeParameter behaviorParameter)
        {
            if (behaviorParameter.parameterType == (int)ParameterType.Int)
            {
                behaviorParameter.intValue = EditorGUILayout.IntField(behaviorParameter.intValue, GUILayout.Width(60));
            }

            if (behaviorParameter.parameterType == (int)ParameterType.Long)
            {
                behaviorParameter.longValue = EditorGUILayout.LongField(behaviorParameter.longValue, GUILayout.Width(60));
            }

            if (behaviorParameter.parameterType == (int)ParameterType.Float)
            {
                behaviorParameter.floatValue = EditorGUILayout.FloatField(behaviorParameter.floatValue, GUILayout.Width(60));
            }

            if (behaviorParameter.parameterType == (int)ParameterType.Bool)
            {
                behaviorParameter.boolValue = EditorGUILayout.Toggle(behaviorParameter.boolValue, GUILayout.Width(60));
            }

            if (behaviorParameter.parameterType == (int)ParameterType.String)
            {
                behaviorParameter.stringValue = EditorGUILayout.TextField(behaviorParameter.stringValue, GUILayout.Width(60));
            }
        }

        protected virtual void DrawCompare(NodeParameter behaviorParameter)
        {
            ParameterCompare[] compareEnumArr = new ParameterCompare[] { };
            if (behaviorParameter.parameterType == (int)ParameterType.Int)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.GREATER, ParameterCompare.GREATER_EQUALS, ParameterCompare.LESS_EQUAL, ParameterCompare.LESS, ParameterCompare.EQUALS, ParameterCompare.NOT_EQUAL };
            }
            else if (behaviorParameter.parameterType == (int)ParameterType.Long)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.GREATER, ParameterCompare.GREATER_EQUALS, ParameterCompare.LESS_EQUAL, ParameterCompare.LESS, ParameterCompare.EQUALS, ParameterCompare.NOT_EQUAL };
            }
            else if (behaviorParameter.parameterType == (int)ParameterType.Float)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.GREATER, ParameterCompare.LESS };
            }
            else if (behaviorParameter.parameterType == (int)ParameterType.Bool)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.EQUALS, ParameterCompare.NOT_EQUAL };
                behaviorParameter.compare = (int)ParameterCompare.EQUALS;
            }
            else if (behaviorParameter.parameterType == (int)ParameterType.String)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.EQUALS, ParameterCompare.NOT_EQUAL };
            }

            string[] compareArr = new string[compareEnumArr.Length];
            int compareIndex = (int)compareEnumArr[0];
            for (int i = 0; i < compareEnumArr.Length; ++i)
            {
                int index = EnumNames.GetEnumIndex<ParameterCompare>(compareEnumArr[i]);
                compareArr[i] = EnumNames.GetEnumName<ParameterCompare>(index);
                //compareArr[i] = System.Enum.GetName(typeof(ParameterCompare), compareEnumArr[i]);
                if ((ParameterCompare)behaviorParameter.compare == compareEnumArr[i])
                {
                    compareIndex = i;
                }
            }

            GUIEnableTool.Enable = (behaviorParameter.parameterType != (int)ParameterType.Bool);
            compareIndex = EditorGUILayout.Popup(compareIndex, compareArr, GUILayout.Width(75));
            behaviorParameter.compare = (int)(compareEnumArr[compareIndex]);
            GUIEnableTool.Enable = true;
        }

    }
}

