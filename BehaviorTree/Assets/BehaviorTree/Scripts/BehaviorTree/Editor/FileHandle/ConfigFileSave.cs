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
                string warning = Localization.GetInstance().Format("Warning");
                string fileNameCannotEmpty = Localization.GetInstance().Format("FileNameCannotEmpty");
                string ok = Localization.GetInstance().Format("OK");
                if (EditorUtility.DisplayDialog(warning, fileNameCannotEmpty, ok))
                {
                    return;
                }
            }

            if (fileName.Length > 30)
            {
                string warning = Localization.GetInstance().Format("Warning");
                string ok = Localization.GetInstance().Format("OK");
                string fileNameTooLong = Localization.GetInstance().Format("FileNameTooLong");
                if (EditorUtility.DisplayDialog(warning, fileNameTooLong, ok))
                {
                    return;
                }
            }

            string filePath = FileHandleController.GetFileFolder();
            filePath = EditorUtility.SaveFilePanel("Save", filePath, fileName, "bytes");
            if (File.Exists(filePath))
            {
                string replace = Localization.GetInstance().Format("Replace");
                string yes = Localization.GetInstance().Format("Yes");
                string fileExistWantReplace = Localization.GetInstance().Format("FileExistWantReplace");
                if (!EditorUtility.DisplayDialog(fileExistWantReplace, replace, yes))
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
