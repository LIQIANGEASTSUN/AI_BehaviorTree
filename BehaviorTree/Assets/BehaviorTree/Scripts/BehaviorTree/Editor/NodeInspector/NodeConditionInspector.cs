using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// Condition Node
    /// </summary>
    public class NodeConditionInspector : NodeLeafInspector
    {
        protected override void Common()
        {
            DrawNodeConditionGroup();
            base.Common();
        }

        protected override HashSet<string> GroupHash()
        {
            return _groupHash;
        }

        protected void DrawNodeConditionGroup()
        {
            EditorGUILayout.BeginVertical("box");
            {
                ConditionGroup conditionGroup = BehaviorConditionGroup.DrawTransitionGroup(nodeValue);
                SetGroupHash(conditionGroup);

                GUIEnableTool.Enable = !DataController.Instance.CurrentOpenConfigSubTree();
                string addGroup = Localization.GetInstance().Format("AddGroup");
                if (GUILayout.Button(addGroup))
                {
                    DataNodeHandler dataNodeHandler = new DataNodeHandler();
                    dataNodeHandler.NodeAddConditionGroup(nodeValue.id);
                }

                GUIEnableTool.Enable = true;
            }
            EditorGUILayout.EndVertical();
        }

        private void SetGroupHash(ConditionGroup conditionGroup)
        {
            _groupHash.Clear();
            if (null == conditionGroup)
            {
                return;
            }
            foreach(var parameter in conditionGroup.parameterList)
            {
                _groupHash.Add(parameter);
            }
        }

    }

}
