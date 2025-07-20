using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class FileHandleView
    {
        public FileHandleView() {   }

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
                        CreateSaveFile(DataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    string delete = Localization.GetInstance().Format("DeleteFile");
                    if (GUILayout.Button(delete))
                    {
                        DeleteFile(DataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    string update = Localization.GetInstance().Format("UpdateFile");
                    if (GUILayout.Button(update))
                    {
                        UpdateAllFile();
                        AssetDatabase.Refresh();
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);

                EditorGUILayout.BeginHorizontal();
                {
                    string fileName = Localization.GetInstance().Format("FileName");
                    EditorGUILayout.LabelField(fileName, GUILayout.Width(80));
                    DataController.Instance.FileName = EditorGUILayout.TextField(DataController.Instance.FileName);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void SelectFile()
        {
            ConfigFileSelect configFileSelect = new ConfigFileSelect();
            string filePath = configFileSelect.Select();
            ConfigFileLoad configFileLoad = new ConfigFileLoad();
            configFileLoad.LoadFile(filePath);
        }

        private static void CreateSaveFile(string fileName)
        {
            ConfigFileSave configFileSave = new ConfigFileSave();
            configFileSave.SaveFile(fileName, DataController.Instance.BehaviorTreeData);
        }

        private static void DeleteFile(string fileName)
        {
            ConfigFileDelete configFileDelete = new ConfigFileDelete();
            configFileDelete.Delete(fileName);
        }

        private static void UpdateAllFile()
        {
            ConfigFileUpdate configFileUpdate = new ConfigFileUpdate();
            configFileUpdate.Update();
        }
    }
}