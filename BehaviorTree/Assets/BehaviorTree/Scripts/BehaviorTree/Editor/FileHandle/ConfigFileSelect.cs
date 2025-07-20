using UnityEditor;

namespace BehaviorTree
{
    public class ConfigFileSelect
    {
        public string Select()
        {
            string filePath = FileHandleController.GetFileFolder();
            filePath = EditorUtility.OpenFilePanel("Select", filePath, "bytes");
            FileHandleController.SaveFilePath(filePath);
            return filePath;
        }
    }
}
