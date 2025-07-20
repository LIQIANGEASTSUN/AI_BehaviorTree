using System.IO;
using UnityEditor;

namespace BehaviorTree
{
    public class ConfigFileDelete
    {
        public void Delete(string fileName)
        {
            string filePath = FileHandleController.GetFileFolder();
            filePath = EditorUtility.OpenFilePanel("Select", filePath, "bytes");
            if (!File.Exists(filePath))
            {
                string tips = Localization.GetInstance().Format("Tips");
                string content = Localization.GetInstance().Format("FileDoesNotExist");
                string fileDoesNotExist = string.Format(content, filePath);
                string yes = Localization.GetInstance().Format("Yes");
                if (!EditorUtility.DisplayDialog(tips, fileDoesNotExist, yes))
                { }
                return;
            }

            File.Delete(filePath);
        }
    }
}