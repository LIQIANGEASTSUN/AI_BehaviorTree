using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{

    public class LocalizationView
    {

        private readonly string[] optionArrKey = new string[] { "English" , "Chinese",};
        private string[] optionArr = new string[2] { "", ""};
        public void Draw()
        {
            for (int i = 0; i < optionArr.Length; ++i)
            {
                string msg = Localization.GetInstance().Format(optionArrKey[i]);
                optionArr[i] = msg;
            }

            int index = GUILayout.Toolbar((int)BehaviorDataController.Instance.LanguageType, optionArr, EditorStyles.toolbarButton);
            if (index != (int)BehaviorDataController.Instance.LanguageType)
            {
                BehaviorDataController.Instance.LanguageType = (LanguageType)index;
            }
        }

    }

}
