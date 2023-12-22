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

            List<PBConfigWriteFile> fileList = new List<PBConfigWriteFile>();
            for (int i = 0; i < fileInfoArr.Length; ++i)
            {
                string fullName = fileInfoArr[i].FullName;

                BehaviorReadWrite readWrite = new BehaviorReadWrite();
                BehaviorTreeData behaviorTreeData = readWrite.ReadJson(fullName);
                behaviorTreeData = RemoveInvalidParameter(behaviorTreeData);

                string content = LitJson.JsonMapper.ToJson(behaviorTreeData);
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(content);

                string fileName = System.IO.Path.GetFileNameWithoutExtension(fullName);
                if (byteData.Length <= 0)
                {
                    Debug.LogError("Invalid config file");
                    return;
                }

                PBConfigWriteFile skillConfigWriteFile = new PBConfigWriteFile();
                skillConfigWriteFile.filePath = filePath;
                skillConfigWriteFile.byteData = byteData;
                fileList.Add(skillConfigWriteFile);

                Debug.Log("end mergeFile:" + filePath);
            }

            ByteBufferWrite bbw = new ByteBufferWrite();
            bbw.WriteInt32(fileList.Count);

            int start = 4 + fileList.Count * (4 + 4);
            for (int i = 0; i < fileList.Count; ++i)
            {
                PBConfigWriteFile skillConfigWriteFile = fileList[i];
                bbw.WriteInt32(start);
                bbw.WriteInt32(skillConfigWriteFile.byteData.Length);
                start += skillConfigWriteFile.byteData.Length;
            }

            for (int i = 0; i < fileList.Count; ++i)
            {
                PBConfigWriteFile skillHsmWriteFile = fileList[i];
                bbw.WriteBytes(skillHsmWriteFile.byteData, skillHsmWriteFile.byteData.Length);
            }

            {
                //string mergeFilePath = string.Format("{0}/StreamingAssets/Bina/behavior_tree_config.bytes", Application.dataPath);

                //if (System.IO.File.Exists(mergeFilePath))
                //{
                //    System.IO.File.Delete(mergeFilePath);
                //    AssetDatabase.Refresh();
                //}
                //FileReadWrite.Write(mergeFilePath, byteData);
                byte[] byteData = bbw.GetBytes();
                SaveToStreamingAssets(byteData);

                byte[] byteData2 = bbw.GetBytes();
                SaveToResources(byteData2);
            }

            AssetDatabase.Refresh();
        }

        private void SaveToStreamingAssets(byte[] byteData)
        {
            string mergeFilePath = CommonUtils.FileUtils.CombinePath(new string[] { Application.dataPath, "StreamingAssets", "Bina", "behavior_tree_config.bytes" });
            if (System.IO.File.Exists(mergeFilePath))
            {
                System.IO.File.Delete(mergeFilePath);
                AssetDatabase.Refresh();
            }
            FileReadWrite.Write(mergeFilePath, byteData);
        }

        private void SaveToResources(byte[] byteData)
        {
            string mergeFilePath = CommonUtils.FileUtils.CombinePath(new string[] { Application.dataPath, "BehaviorTree", "Resources", "behavior_tree_config.bytes" });
            if (System.IO.File.Exists(mergeFilePath))
            {
                System.IO.File.Delete(mergeFilePath);
                AssetDatabase.Refresh();
            }
            FileReadWrite.Write(mergeFilePath, byteData);
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
