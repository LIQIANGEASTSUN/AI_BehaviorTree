using UnityEngine;
using UnityEditor;
using GraphicTree;

namespace BehaviorTree
{
    public class NodeDrawEditor
    {

        private static int height = 45;
        public static void Draw(NodeValue nodeValue, int selectNodeId, float value = 0f)
        {
            nodeValue.position.height = GetHight(nodeValue);
            height = (int)nodeValue.position.height - 15;

            EditorGUILayout.BeginVertical(/*"box",*/ GUILayout.Height(height));
            {
                Rect backRect = new Rect(5, 20, nodeValue.position.width - 10, height - 8);
                GUI.backgroundColor = GetColor(nodeValue, selectNodeId);
                GUI.Box(backRect, string.Empty);
                GUI.backgroundColor = Color.white;

                string idMsg = string.Empty;
                if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
                {
                    idMsg = $"ID:{nodeValue.id}";
                }
                else
                {
                    if (nodeValue.NodeType == (int)NODE_TYPE.CONDITION)
                    {
                        idMsg = $"ID:{nodeValue.id}";
                    }
                    else if (nodeValue.NodeType == (int)NODE_TYPE.ACTION)
                    {
                        idMsg = $"ID:{nodeValue.id}";
                    }
                    else
                    {
                        idMsg = $"ID:{nodeValue.id}";
                    }
                }
                EditorGUILayout.LabelField(idMsg);

                nodeValue.descript = EditorGUILayout.TextField(nodeValue.descript);

                int resultIntType = (int)ResultType.Fail;
                float slider = NodeNotify.NodeDraw(nodeValue.id, ref resultIntType);
                ResultType resultType = (ResultType)resultIntType;

                Rect runRect = new Rect(8, height, nodeValue.position.width - 16, 8);
                GUI.Box(runRect, string.Empty);
                if (slider > 0)
                {
                    runRect.width = runRect.width * slider;
                    GUI.backgroundColor = GetSliderColor(resultType);
                    GUI.Box(runRect, string.Empty);
                    GUI.backgroundColor = Color.white;
                }
            }
            EditorGUILayout.EndVertical();
        }

        private static Color GetColor(NodeValue nodeValue, int selectNodeId)
        {
            Color color = Color.white;

            if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
            {
                if (nodeValue.id == selectNodeId)
                {
                    color = new Color(0, 1, 0, 0.35f);
                }
                else
                {
                    color = new Color(0, 0, 1, 0.35f);
                }
                return color;
            }

            if (nodeValue.isRootNode || nodeValue.subTreeEntry)
            {
                color = Color.green;
            }
            else if (nodeValue.id == selectNodeId)
            {
                color = new Color(0, 1, 0, 0.35f);
            }
            else
            {
                color = new Color(1f, 0.92f, 0.016f, 1f);
            }

            return color;
        }

        private static Color GetSliderColor(ResultType resultType)
        {
            Color color = Color.white;
            if (resultType == ResultType.Fail)
            {
                color = Color.red;
            }
            else if (resultType == ResultType.Running)
            {
                color = Color.green;
            }
            else if (resultType == ResultType.Success)
            {
                color = Color.blue;
            }
            return color;
        }

        private static int GetHight(NodeValue nodeValue)
        {
            //int height = 60;
            int height = 80;
            if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE)
            {
                height = 80;
            }

            return height;
        }

    }
}
