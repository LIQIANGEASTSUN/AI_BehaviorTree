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
                    string selectFile = Localization.GetInstance().Format("SelectFile");
                    if (GUILayout.Button(selectFile))
                    {
                        SelectFile();
                    }

                    string save = Localization.GetInstance().Format("SaveFile");
                    if (GUILayout.Button(save))
                    {
                        CreateSaveFile(BehaviorDataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    string delete = Localization.GetInstance().Format("DeleteFile");
                    if (GUILayout.Button(delete))
                    {
                        DeleteFile(BehaviorDataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    string update = Localization.GetInstance().Format("UpdateFile");
                    if (GUILayout.Button(update))
                    {
                        UpdateAllFile(BehaviorDataController.Instance.FilePath);
                        AssetDatabase.Refresh();
                    }

                    string merge = Localization.GetInstance().Format("MergeFile");
                    if (GUILayout.Button(merge))
                    {
                        MergeFile(BehaviorDataController.Instance.FilePath);
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);

                EditorGUILayout.BeginHorizontal();
                {
                    string fileName = Localization.GetInstance().Format("FileName");
                    EditorGUILayout.LabelField(fileName, GUILayout.Width(80));
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
