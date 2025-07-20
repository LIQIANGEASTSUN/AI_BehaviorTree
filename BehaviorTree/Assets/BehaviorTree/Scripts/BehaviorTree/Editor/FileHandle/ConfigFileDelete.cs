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
                string fileDoesNotExist = $"文件不存在:{filePath}";
                if (!EditorUtility.DisplayDialog("提示", fileDoesNotExist, "Yes")) { }
                return;
            }

            File.Delete(filePath);
        }
    }
}