using System.IO;
using UnityEditor;

namespace BehaviorTree
{
    public class ConfigFileDelete
    {
        public void Delete(string fileName)
        {
            string filePath = BehaviorDataController.Instance.GetFilePath(fileName);
            if (!File.Exists(filePath))
            {
                if (!EditorUtility.DisplayDialog("提示", "文件不存在", "yes"))
                { }
                return;
            }

            File.Delete(filePath);
        }
    }
}