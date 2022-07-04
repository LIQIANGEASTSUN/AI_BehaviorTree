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
                if (EditorUtility.DisplayDialog("警告", "文件名不能为空", "确定"))
                {
                    return;
                }
            }

            if (fileName.Length > 30)
            {
                if (EditorUtility.DisplayDialog("警告", "文件名过长，不能大于20个字符", "确定"))
                {
                    return;
                }
            }

            string path = BehaviorDataController.Instance.GetFilePath(fileName);
            if (File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("已存在文件", "是否替换新文件", "替换", "取消"))
                {
                    return;
                }
            }

            // 如果项目总不包含该路径，创建一个
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            data.nodeDic.Clear();

            data = DataNodeIdStandardTool.StandardID(data);
            data.fileName = fileName;

            BehaviorReadWrite readWrite = new BehaviorReadWrite();
            readWrite.WriteJson(data, path);
        }

    }

}
