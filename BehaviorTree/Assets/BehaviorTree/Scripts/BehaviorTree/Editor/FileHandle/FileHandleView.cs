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
                    if (GUILayout.Button("选择文件"))
                    {
                        SelectFile();
                    }

                    if (GUILayout.Button("保存"))
                    {
                        CreateSaveFile(DataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    if (GUILayout.Button("删除"))
                    {
                        DeleteFile(DataController.Instance.FileName);
                        AssetDatabase.Refresh();
                    }

                    if (GUILayout.Button("更新"))
                    {
                        UpdateAllFile();
                        AssetDatabase.Refresh();
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("文件名", GUILayout.Width(80));
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