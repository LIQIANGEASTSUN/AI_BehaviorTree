using UnityEngine;
using UnityEditor;
using GraphicTree;

namespace BehaviorTree
{
    public static class BehaviorConditionGroup
    {
        private static int nodeId = -1;
        private static int selectIndex = -1;
        public static ConditionGroup DrawTransitionGroup(NodeValue nodeValue)
        {
            if (null == nodeValue)
            {
                return null;
            }

            ConditionGroup group = null;
            for (int i = 0; i < nodeValue.conditionGroupList.Count; ++i)
            {
                ConditionGroup tempGroup = nodeValue.conditionGroupList[i];
                ConditionGroup temp = DrawGroup(nodeValue, tempGroup);
                if (null != temp)
                {
                    group = temp;
                }
            }

            return group;
        }

        private static ConditionGroup DrawGroup(NodeValue nodeValue, ConditionGroup group)
        {
            Rect area = GUILayoutUtility.GetRect(0f, 1, GUILayout.ExpandWidth(true));
            bool select = (selectIndex == group.index);

            GUIEnableTool.Enable = true;
            EditorGUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));
            {
                if (selectIndex < 0 || nodeId < 0 || nodeId != nodeValue.id)
                {
                    nodeId = nodeValue.id;
                    selectIndex = group.index;
                }

                Rect rect = new Rect(area.x, area.y, area.width, 30);
                GUI.backgroundColor = select ? new Color(0, 1, 0, 1) : Color.white;// 
                GUI.Box(rect, string.Empty);
                GUI.backgroundColor = Color.white;

                string selectLocalization = Localization.GetInstance().Format("Select");
                if (GUILayout.Button(selectLocalization, GUILayout.Width(50)))
                {
                    selectIndex = group.index;
                }

                for (int i = group.parameterList.Count - 1; i >= 0; --i)
                {
                    string parameter = group.parameterList[i];
                    NodeParameter behaviorParameter = nodeValue.parameterList.Find(a => (a.parameterName.CompareTo(parameter) == 0));
                    if (null == behaviorParameter)
                    {
                        group.parameterList.RemoveAt(i);
                    }
                }

                GUIEnableTool.Enable = select && !BehaviorDataController.Instance.CurrentOpenConfigSubTree();
          
                for (int i = 0; i < nodeValue.parameterList.Count; ++i)
                {
                    NodeParameter parameter = nodeValue.parameterList[i];
                    string name = group.parameterList.Find(a => (a.CompareTo(parameter.parameterName) == 0));

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(10));
                        bool value = !string.IsNullOrEmpty(name);
                        bool oldValue = value;
                        value = EditorGUILayout.Toggle(value, GUILayout.Width(10));
                        if (value)
                        {
                            if (!oldValue)
                            {
                                group.parameterList.Add(parameter.parameterName);
                                break;
                            }
                        }
                        else
                        {
                            if (oldValue)
                            {
                                group.parameterList.Remove(parameter.parameterName);
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);
                }
                GUIEnableTool.Enable = true;

                GUIEnableTool.Enable = !BehaviorDataController.Instance.CurrentOpenConfigSubTree();
                string deleteFile = Localization.GetInstance().Format("DeleteFile");
                if (GUILayout.Button(deleteFile))
                {
                    DataNodeHandler dataNodeHandler = new DataNodeHandler();
                    dataNodeHandler.NodeDelConditionGroup(nodeValue.id, group.index);
                }
                GUIEnableTool.Enable = true;
            }
            EditorGUILayout.EndHorizontal();

            if (select)
            {
                return group;
            }
            return null;
        }

    }

}
