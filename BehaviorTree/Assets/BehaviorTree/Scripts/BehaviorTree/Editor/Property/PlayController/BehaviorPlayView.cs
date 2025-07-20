using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class BehaviorPlayView
    {
        private readonly string[] optionArr = new string[] { "Play", "Pause", "Stop" };
        public BehaviorPlayView()
        {

        }

        public void Draw()
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                int index = (int)DataController.Instance.PlayState;
                int option = GUILayout.Toolbar(index, optionArr, EditorStyles.toolbarButton);
                if (index != option)
                {
                    BehaviorPlayType state = (BehaviorPlayType)option;
                    DataController.Instance.PlayState = state;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

}
