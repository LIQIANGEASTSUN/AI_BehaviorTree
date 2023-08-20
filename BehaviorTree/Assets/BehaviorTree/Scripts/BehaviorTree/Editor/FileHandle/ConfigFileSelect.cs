using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class ConfigFileSelect
    {

        public string Select()
        {
            string path = BehaviorDataController.Instance.FilePath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            GUILayout.Space(8);

            string selectConfigFile = Localization.GetInstance().Format("SelectConfigFile");
            string filePath = EditorUtility.OpenFilePanel(selectConfigFile, path, "bytes");
            return filePath;
        }

    }

}
