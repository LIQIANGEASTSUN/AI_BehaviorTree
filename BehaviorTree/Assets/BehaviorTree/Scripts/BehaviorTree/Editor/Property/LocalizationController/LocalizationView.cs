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

            int index = GUILayout.Toolbar((int)DataController.Instance.LanguageType, optionArr, EditorStyles.toolbarButton);
            if (index != (int)DataController.Instance.LanguageType)
            {
                DataController.Instance.LanguageType = (LanguageType)index;
                if (null != DataController.languageChange)
                {
                    DataController.languageChange();
                }
            }
        }

    }

}
