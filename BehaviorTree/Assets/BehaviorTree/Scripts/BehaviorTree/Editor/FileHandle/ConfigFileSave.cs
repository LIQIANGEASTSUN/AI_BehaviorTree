using UnityEditor;
using System.IO;


namespace BehaviorTree
{
    public class ConfigFileSave
    {

        public void SaveFile(string fileName, BehaviorTreeData data)
        {
            if (data == null)
            {
                UnityEngine.Debug.LogError("rootNode is null");
                return;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                if (EditorUtility.DisplayDialog("Warning", "文件名不能为空", "OK"))
                {
                    return;
                }
            }

            if (fileName.Length > 30)
            {
                if (EditorUtility.DisplayDialog("Warning", "FileNameTooLong", "OK"))
                {
                    return;
                }
            }

            string filePath = FileHandleController.GetFileFolder();
            filePath = EditorUtility.SaveFilePanel("Save", filePath, fileName, "bytes");
            if (File.Exists(filePath))
            {
                if (!EditorUtility.DisplayDialog("文件已存在, 是否替换新文件", "替换", "Yes"))
                {
                    return;
                }
            }
            FileHandleController.SaveFilePath(filePath);

            // 如果项目总不包含该路径，创建一个
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            data.nodeDic.Clear();

            data = DataNodeIdStandardTool.StandardID(data);
            data.fileName = fileName;

            BehaviorReadWrite readWrite = new BehaviorReadWrite();
            readWrite.WriteJson(data, filePath);
        }
    }
}
