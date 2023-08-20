using GraphicTree;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace BehaviorTree
{
    public class ConfigFileLoad
    {
        public void LoadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            BehaviorDataController.Instance.ConfigDataDic.Clear();

            BehaviorReadWrite readWrite = new BehaviorReadWrite();
            BehaviorTreeData behaviorTreeData = ReadFile(fileName, true);
            if (null == behaviorTreeData)
            {
                UnityEngine.Debug.LogError("file is null:" + fileName);
                return;
            }

            BehaviorDataController.Instance.PlayState = BehaviorPlayType.STOP;
            NodeNotify.SetPlayState((int)BehaviorPlayType.STOP);

            BehaviorDataController.Instance.SetBehaviorData(behaviorTreeData);

            BehaviorDataController.Instance.CurrentSelectId = -1;
            BehaviorDataController.Instance.CurrentOpenSubTree = -1;

            BehaviorRunTime.Instance.Reset(behaviorTreeData);
        }

        public BehaviorTreeData ReadFile(string fileName, bool warningWhenExist)
        {
            BehaviorTreeData behaviorTreeData = null;
            if (BehaviorDataController.Instance.ConfigDataDic.TryGetValue(fileName, out behaviorTreeData))
            {
                return behaviorTreeData;
            }

            Queue<string> queue = new Queue<string>();
            queue.Enqueue(fileName);
            while (queue.Count > 0)
            {
                string name = queue.Dequeue();
                string filePath = BehaviorDataController.Instance.GetFilePath(name);
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
                behaviorTreeData = readWrite.ReadJson(filePath);
                BehaviorDataController.Instance.ConfigDataDic[name] = behaviorTreeData;

                foreach (var nodeValue in behaviorTreeData.nodeList)
                {
                    if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE
                        && nodeValue.subTreeType == (int)SUB_TREE_TYPE.CONFIG
                        && !BehaviorDataController.Instance.ConfigDataDic.TryGetValue(nodeValue.subTreeConfig, out behaviorTreeData))
                    {
                        queue.Enqueue(nodeValue.subTreeConfig);
                    }
                }
            }

            BehaviorDataController.Instance.ConfigDataDic.TryGetValue(fileName, out behaviorTreeData);
            return behaviorTreeData;
        }
    }
}