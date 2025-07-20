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
            DataController.Instance = new DataController();
            BehaviorConfigNode.Instance.Init();

            _behaviorDrawPropertyController = new BehaviorDrawPropertyController();
            _behaviorDrawPropertyController.Init();

            _behaviorDrawController = new BehaviorDrawController();
            _behaviorDrawController.Init();

            string path = FileUtils.CombinePath(Application.dataPath, "StreamingAssets", "CSVAssets");
            TableRead.Instance.ReadCustomPath(path);
            BehaviorRunTime.Instance.Init();
            BehaviorNodeDrawInfoController.GetInstance().InitInfoList();
        }

        public void OnDestroy()
        {
            DataController.Instance.OnDestroy();
            _behaviorDrawPropertyController.OnDestroy();
            _behaviorDrawController.OnDestroy();
            BehaviorRunTime.Instance.OnDestroy();

            AssetDatabase.Refresh();
            //UnityEngine.Caching.ClearCache();
        }

        public void Update()
        {
            CheckNodeTool.CheckNode(DataController.Instance.BehaviorTreeData.nodeList);
            CheckSubTreeTool.CheckSubTreeEditor(DataController.Instance.BehaviorTreeData.nodeList);
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
