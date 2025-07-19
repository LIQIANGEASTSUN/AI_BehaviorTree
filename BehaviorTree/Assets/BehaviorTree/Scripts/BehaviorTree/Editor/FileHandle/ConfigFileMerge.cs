using GraphicTree;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class ConfigFileMerge
    {

        public void MergeFile(string filePath)
        {
            AssetDatabase.Refresh();
            DirectoryInfo direcotryInfo = Directory.CreateDirectory(filePath);
            FileInfo[] fileInfoArr = direcotryInfo.GetFiles("*.bytes", SearchOption.TopDirectoryOnly);

            ConfigDataGroup configDataGroup = new ConfigDataGroup();
            for (int i = 0; i < fileInfoArr.Length; ++i)
            {
                string fullName = fileInfoArr[i].FullName;

                BehaviorReadWrite readWrite = new BehaviorReadWrite();
                BehaviorTreeData behaviorTreeData = readWrite.ReadJson(fullName);
                behaviorTreeData = RemoveInvalidParameter(behaviorTreeData);

                string content = LitJson.JsonMapper.ToJson(behaviorTreeData);
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(content);

                if (byteData.Length <= 0)
                {
                    Debug.LogError("Invalid config file");
                    return;
                }

                ConfigData configData = new ConfigData();
                configData.fileName = Path.GetFileNameWithoutExtension(filePath);
                configData.byteDatas = byteData;
                configDataGroup.dataList.Add(configData);
                Debug.Log("end mergeFile:" + filePath);
            }

            SaveToStreamingAssets(configDataGroup);
        }

        private void SaveToStreamingAssets(ConfigDataGroup dataGroup)
        {
            string json = LitJson.JsonMapper.ToJson(dataGroup);
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(json);
            string mergeFilePath = FileUtils.CombinePath(new string[] { Application.dataPath, "SubAssets", "ConfigBin", "behavior_tree_config.bytes" });
            if (File.Exists(mergeFilePath))
            {
                File.Delete(mergeFilePath);
                AssetDatabase.Refresh();
            }
            FileReadWrite.Write(mergeFilePath, byteData);
            AssetDatabase.Refresh();
        }

        private static BehaviorTreeData RemoveInvalidParameter(BehaviorTreeData behaviorData)
        {
            HashSet<string> _usedParameterHash = new HashSet<string>();

            for (int i = 0; i < behaviorData.nodeList.Count; ++i)
            {
                NodeValue nodeValue = behaviorData.nodeList[i];

                for (int j = 0; j < nodeValue.parameterList.Count; ++j)
                {
                    NodeParameter parameter = nodeValue.parameterList[j];
                    if (!_usedParameterHash.Contains(parameter.parameterName))
                    {
                        _usedParameterHash.Add(parameter.parameterName);
                    }
                }

                for (int j = 0; j < nodeValue.conditionGroupList.Count; ++j)
                {
                    ConditionGroup group = nodeValue.conditionGroupList[j];
                    for (int k = 0; k < group.parameterList.Count; ++k)
                    {
                        string name = group.parameterList[k];
                        if (!_usedParameterHash.Contains(name))
                        {
                            _usedParameterHash.Add(name);
                        }
                    }
                }
            }

            for (int i = behaviorData.parameterList.Count - 1; i >= 0; --i)
            {
                NodeParameter parameter = behaviorData.parameterList[i];
                if (!_usedParameterHash.Contains(parameter.parameterName))
                {
                    behaviorData.parameterList.RemoveAt(i);
                }
            }

            return behaviorData;
        }

    }

}
