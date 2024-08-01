using UnityEngine;
using UnityEditor;


namespace BehaviorTree
{
    public delegate void BehaviorChangeSelectId(int nodeId);

    public class BehaviorManager
    {
        private BehaviorDrawPropertyController _behaviorDrawPropertyController;
        private BehaviorDrawController _behaviorDrawController;

        public BehaviorManager()
        {
            Init();
        }

        private void Init()
        {
            BehaviorDataController.Instance = new BehaviorDataController();

            _behaviorDrawPropertyController = new BehaviorDrawPropertyController();
            _behaviorDrawPropertyController.Init();

            _behaviorDrawController = new BehaviorDrawController();
            _behaviorDrawController.Init();

            string csvPath = BehaviorDataController.Instance.GetCsvPath();
            TableRead.Instance.ReadCustomPath(csvPath);

            BehaviorRunTime.Instance.Init();
            BehaviorNodeDrawInfoController.GetInstance();
        }

        public void OnDestroy()
        {
            BehaviorDataController.Instance.OnDestroy();
            _behaviorDrawPropertyController.OnDestroy();
            _behaviorDrawController.OnDestroy();
            BehaviorRunTime.Instance.OnDestroy();

            AssetDatabase.Refresh();
            //UnityEngine.Caching.ClearCache();
        }

        public void Update()
        {
            CheckNodeTool.CheckNode(BehaviorDataController.Instance.BehaviorTreeData.nodeList);
            CheckSubTreeTool.CheckSubTreeEditor(BehaviorDataController.Instance.BehaviorTreeData.nodeList);
            BehaviorRunTime.Instance.Update();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical("box", GUILayout.Width(300), GUILayout.ExpandHeight(true));
                {
                    _behaviorDrawPropertyController.OnGUI();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                {
                    _behaviorDrawController.OnGUI();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

    }
}
