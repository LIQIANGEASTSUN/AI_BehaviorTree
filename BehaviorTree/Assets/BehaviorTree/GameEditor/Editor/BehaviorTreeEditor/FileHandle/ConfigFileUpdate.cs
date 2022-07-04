using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BehaviorTree
{
    public class ConfigFileUpdate
    {
        public void Update(string filePath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(filePath);
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
                string fileName = System.IO.Path.GetFileNameWithoutExtension(fullName);

                fileName = string.Format("{0}.bytes", fileName);
                string jsonFilePath = CommonUtils.FileUtils.CombinePath(filePath, "Json", fileName);
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

        // 检测子节点是否包含圈
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
