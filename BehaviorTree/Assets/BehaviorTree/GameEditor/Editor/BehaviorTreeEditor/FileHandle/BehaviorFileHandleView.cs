using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class BehaviorFileHandleView
    {
        public BehaviorFileHandleView()
        {

        }

        public void Draw()
        {
            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("选择文件"))
                    {
                        SelectFile();
                    }

                    if (GUILayout.Button("保存"))
                    {
                        CreateSaveFile(BehaviorDataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    if (GUILayout.Button("删除"))
                    {
                        DeleteFile(BehaviorDataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }
                    if (GUILayout.Button("批量更新"))
                    {
                        UpdateAllFile(BehaviorDataController.Instance.FilePath);
                        AssetDatabase.Refresh();
                    }
                    if (GUILayout.Button("合并"))
                    {
                        MergeFile(BehaviorDataController.Instance.FilePath);
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("文件名", GUILayout.Width(80));
                    BehaviorDataController.Instance.FileName = EditorGUILayout.TextField(BehaviorDataController.Instance.FileName);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void SelectFile()
        {
            ConfigFileSelect configFileSelect = new ConfigFileSelect();
            string filePath = configFileSelect.Select();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            ConfigFileLoad configFileLoad = new ConfigFileLoad();
            configFileLoad.LoadFile(fileName);
        }

        private static void CreateSaveFile(string fileName)
        {
            ConfigFileSave configFileSave = new ConfigFileSave();
            configFileSave.SaveFile(fileName, BehaviorDataController.Instance.BehaviorTreeData);
        }

        private static void DeleteFile(string fileName)
        {
            ConfigFileDelete configFileDelete = new ConfigFileDelete();
            configFileDelete.Delete(fileName);
        }

        private static void UpdateAllFile(string filePath)
        {
            ConfigFileUpdate configFileUpdate = new ConfigFileUpdate();
            configFileUpdate.Update(filePath);
        }

        private static void MergeFile(string filePath)
        {
            ConfigFileMerge configFileMerge = new ConfigFileMerge();
            configFileMerge.MergeFile(filePath);
        }
    }
}
