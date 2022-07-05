using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class NodeIfInspector : NodeCompositeInspector
    {
        protected override void Common()
        {
            NodeChild();
        }

        private void NodeChild()
        {
            HashSet<int> childHash = new HashSet<int>();
            if (nodeValue.childNodeList.Count > 3)
            {
                if (null != TreeNodeWindow.window)
                {
                    string ifNodeMaxChild = Localization.GetInstance().Format("IfNodeMaxChild");
                    string msg = string.Format(ifNodeMaxChild, nodeValue.id);
                    TreeNodeWindow.window.ShowNotification(msg);
                }

                while (nodeValue.childNodeList.Count > 3)
                {
                    nodeValue.childNodeList.RemoveAt(nodeValue.childNodeList.Count - 1);
                }

                while (nodeValue.ifJudgeDataList.Count > 3)
                {
                    nodeValue.ifJudgeDataList.RemoveAt(nodeValue.ifJudgeDataList.Count - 1);
                }
            }

            EditorGUILayout.BeginVertical("box");
            {
                for (int i = 0; i < nodeValue.childNodeList.Count; ++i)
                {
                    int childId = nodeValue.childNodeList[i];
                    childHash.Add(childId);
                    IfJudgeData judgeData = nodeValue.ifJudgeDataList.Find((a) => { return a.nodeId == childId; });
                    if (null == judgeData)
                    {
                        judgeData = new IfJudgeData();
                        judgeData.nodeId = childId;
                        nodeValue.ifJudgeDataList.Add(judgeData);
                    }
                    judgeData.ifJudegType = ((i == 0) ? (int)NodeIfJudgeEnum.IF : (int)NodeIfJudgeEnum.ACTION);

                    EditorGUILayout.BeginVertical("box");
                    {
                        GUIEnableTool.Enable = false;
                        string nodeId = Localization.GetInstance().Format("NodeId");
                        EditorGUILayout.IntField(nodeId, judgeData.nodeId);
                        {
                            string[] nameArr = EnumNames.GetEnumNames<NodeIfJudgeEnum>();
                            int index = EnumNames.GetEnumIndex<NodeIfJudgeEnum>((NodeIfJudgeEnum)judgeData.ifJudegType);
                            string nodeType = Localization.GetInstance().Format("NodeType");
                            int result = EditorGUILayout.Popup(nodeType, index, nameArr);
                            judgeData.ifJudegType = (int)(EnumNames.GetEnum<NodeIfJudgeEnum>(result));
                        }
                        GUIEnableTool.Enable = true;

                        if (judgeData.ifJudegType == (int)NodeIfJudgeEnum.ACTION)
                        {
                            string[] nameArr = new string[] { "Fail", "Success" };
                            int oldValue = judgeData.ifResult;
                            string executeConditional = Localization.GetInstance().Format("ExecuteConditional");
                            int result = EditorGUILayout.Popup(executeConditional, judgeData.ifResult, nameArr);
                            if (oldValue != result)
                            {
                                JudgeNodeChangeChildCondition(nodeValue, judgeData.nodeId, (ResultType)result);
                            }
                        }
                    }
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.BeginVertical("box");
                {
                    string defaultResult = Localization.GetInstance().Format("DefaultResult");
                    EditorGUILayout.LabelField(defaultResult);
                    string[] nameArr = new string[] { "Fail", "Success", "Running" };
                    nodeValue.defaultResult = EditorGUILayout.Popup(defaultResult, nodeValue.defaultResult, nameArr);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();

            for (int i = nodeValue.ifJudgeDataList.Count - 1; i >= 0; --i)
            {
                IfJudgeData data = nodeValue.ifJudgeDataList[i];
                if (!childHash.Contains(data.nodeId))
                {
                    nodeValue.ifJudgeDataList.RemoveAt(i);
                }
            }

            nodeValue.ifJudgeDataList.Sort((a, b) =>
            {
                return a.ifJudegType - b.ifJudegType;
            });
        }

        private void JudgeNodeChangeChildCondition(NodeValue nodeValue, int childId, ResultType resultType)
        {
            for (int i = 0; i < nodeValue.ifJudgeDataList.Count; ++i)
            {
                IfJudgeData judgeData = nodeValue.ifJudgeDataList[i];
                if (judgeData.ifJudegType == (int)NodeIfJudgeEnum.IF)
                {
                    continue;
                }

                if (judgeData.nodeId == childId)
                {
                    judgeData.ifResult = (int)resultType;
                }
                else
                {
                    int result = (int)resultType - (int)ResultType.Success;
                    judgeData.ifResult = Mathf.Abs(result);
                }
            }
        }

    }
}

