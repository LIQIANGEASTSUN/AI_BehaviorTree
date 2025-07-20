using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class ConfigFileUpdate
    {
        public void Update()
        {
            string directory = FileHandleController.GetFileFolder();
            directory = EditorUtility.OpenFolderPanel("Select", directory, "bytes");
            FileHandleController.SaveFilePath(directory);

            DirectoryInfo dInfo = new DirectoryInfo(directory);
            FileInfo[] fileInfoArr = dInfo.GetFiles("*.bytes", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < fileInfoArr.Length; ++i)
            {
                string fullName = fileInfoArr[i].FullName;
                BehaviorReadWrite readWrite = new BehaviorReadWrite();
                BehaviorTreeData treeData = readWrite.ReadJson(fullName);

                treeData = DataNodeIdStandardTool.StandardID(treeData);
                CheckCircle(treeData, treeData.rootNodeId);

                CheckSubTreeTool.CheckSubTreeUpdate(treeData.nodeList);

                treeData = UpdateData(treeData);
                string fileName = Path.GetFileNameWithoutExtension(fullName);

                fileName = string.Format("{0}.bytes", fileName);
                string jsonFilePath = FileUtils.CombinePath(directory, "Json", fileName);
                bool value = readWrite.WriteJson(treeData, jsonFilePath);
                if (!value)
                {
                    Debug.LogError("WriteError:" + jsonFilePath);
                }
            }
        }

        private BehaviorTreeData UpdateData(BehaviorTreeData treeData)
        {
            return treeData;
        }

        // Checks whether the child node contains a circle
        private void CheckCircle(BehaviorTreeData treeData, int id)
        {
            HashSet<int> hash = new HashSet<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(id);
            while (queue.Count > 0)
            {
                id = queue.Dequeue();
                NodeValue nodeValue = treeData.nodeList.Find((a) => {
                    return a.id == id;
                });

                if (null == nodeValue || nodeValue.childNodeList.Count <= 0)
                {
                    continue;
                }

                foreach (var childId in nodeValue.childNodeList)
                {
                    if (hash.Contains(childId))
                    {
                        Debug.LogError(treeData.fileName + "   NodeValue:" + childId + "  Has multiple parent");
                        break;
                    }
                    else
                    {
                        hash.Add(childId);
                        queue.Enqueue(childId);
                    }
                }
            }
        }
    }
}
