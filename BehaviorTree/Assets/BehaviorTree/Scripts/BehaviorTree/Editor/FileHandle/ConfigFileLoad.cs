using GraphicTree;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace BehaviorTree
{
    public class ConfigFileLoad
    {
        public void LoadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            DataController.Instance.ConfigDataDic.Clear();

            BehaviorTreeData behaviorTreeData = ReadFile(filePath, true);
            if (null == behaviorTreeData)
            {
                UnityEngine.Debug.LogError("file is null:" + filePath);
                return;
            }

            DataController.Instance.PlayState = BehaviorPlayType.STOP;
            NodeNotify.SetPlayState((int)BehaviorPlayType.STOP);

            DataController.Instance.SetBehaviorData(behaviorTreeData);

            DataController.Instance.CurrentSelectId = -1;
            DataController.Instance.CurrentOpenSubTree = -1;

            BehaviorRunTime.Instance.Reset(behaviorTreeData);
        }

        public BehaviorTreeData ReadFile(string filePath, bool warningWhenExist)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(fileName);
            while (queue.Count > 0)
            {
                string name = queue.Dequeue();
                filePath = Path.Combine(directoryPath, name + ".bytes");
                if (warningWhenExist && !File.Exists(filePath))
                {
                    string tips = Localization.GetInstance().Format("Tips");
                    string content = Localization.GetInstance().Format("FileDoesNotExist");
                    string fileDoesNotExist = string.Format(content, filePath);
                    string yes = Localization.GetInstance().Format("Yes");
                    if (!EditorUtility.DisplayDialog(tips, fileDoesNotExist, yes))
                    {
                        continue;
                    }
                }

                BehaviorReadWrite readWrite = new BehaviorReadWrite();
                BehaviorTreeData behaviorTreeData = readWrite.ReadJson(filePath);
                DataController.Instance.ConfigDataDic[name] = behaviorTreeData;

                foreach (var nodeValue in behaviorTreeData.nodeList)
                {
                    if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE
                        && nodeValue.subTreeType == (int)SUB_TREE_TYPE.CONFIG
                        && !DataController.Instance.ConfigDataDic.TryGetValue(nodeValue.subTreeConfig, out behaviorTreeData))
                    {
                        queue.Enqueue(nodeValue.subTreeConfig);
                    }
                }
            }

            DataController.Instance.ConfigDataDic.TryGetValue(fileName, out var data);
            return data;
        }
    }
}