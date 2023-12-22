using GraphicTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BehaviorTree
{

    public class BehaviorDataController
    {

        public static BehaviorDataController Instance;

        public static BehaviorChangeSelectId behaviorChangeSelectId;
        public static Action languageChange;

        public string FileName
        {
            get { return BehaviorTreeData.fileName; }
            set { BehaviorTreeData.fileName = value; }
        }

        private string _filePath = string.Empty;
        public string FilePath
        {
            get { return _filePath; }
        }

        public string GetFilePath(string fileName)
        {
            fileName = string.Format("{0}.bytes", fileName);
            string path = CommonUtils.FileUtils.CombinePath(FilePath, fileName);
            return path;
        }

        public string GetCsvPath()
        {
            string csvPath = CommonUtils.FileUtils.CombinePath(new string[] { Application.streamingAssetsPath, "CSVAssets" });
            return csvPath;
        }

        private BehaviorTreeData _behaviorTreeData;
        public BehaviorTreeData BehaviorTreeData
        {
            get { return _behaviorTreeData; }
            set { _behaviorTreeData = value; }
        }

        public List<NodeValue> NodeList
        {
            get
            {
                return BehaviorTreeData.nodeList;
            }
        }

        private Dictionary<string, BehaviorTreeData> _configDataDic = new Dictionary<string, BehaviorTreeData>();
        public Dictionary<string, BehaviorTreeData> ConfigDataDic
        {
            get { return _configDataDic; }
        }

        // The currently selected node
        private int _currentSelectId = 0;
        public int CurrentSelectId
        {
            get { return _currentSelectId; }
            set { _currentSelectId = value; }
        }

        // The currently selected subtree node
        private int _currentOpenSubTreeId = -1;
        public int CurrentOpenSubTree
        {
            get { return _currentOpenSubTreeId; }
            set { _currentOpenSubTreeId = value; }
        }

        private BehaviorPlayType _playState = BehaviorPlayType.STOP;
        public BehaviorPlayType PlayState
        {
            get { return _playState; }
            set {
                _playState = value;
                NodeNotify.SetPlayState((int)value);
            }
        }

        private HashSet<int> _runTimeInvalidSubTreeHash = new HashSet<int>();
        public HashSet<int> RunTimeInvalidSubTreeHash
        {
            get { return _runTimeInvalidSubTreeHash; }
        }

        private string languageTypeKey = "LanguageTypeKey";
        private LanguageType _languageType = LanguageType.EN;
        public LanguageType LanguageType
        {
            get {  return _languageType; }
            set {  _languageType = value;
                EditorPrefs.SetInt(languageTypeKey, (int)value);
            }
        }

        public BehaviorDataController()
        {
            Init();
        }

        public void Init()
        {
            _behaviorTreeData = new BehaviorTreeData();
            _runTimeInvalidSubTreeHash.Clear();
            ConfigDataDic.Clear();
            _playState = BehaviorPlayType.STOP;

            if(EditorPrefs.HasKey(languageTypeKey))
            {
                LanguageType = (LanguageType)EditorPrefs.GetInt(languageTypeKey);
            }

            _filePath = CommonUtils.FileUtils.CombinePath(new string[] { "Assets", "BehaviorTree", "GameData", "BehaviorTree" });

            behaviorChangeSelectId += ChangeSelectId;
        }

        public void OnDestroy()
        {
            behaviorChangeSelectId -= ChangeSelectId;
        }

        public void SetBehaviorData(BehaviorTreeData behaviorTreeData)
        {
            BehaviorTreeData = behaviorTreeData;
        }

        private void ChangeSelectId(int nodeId)
        {
            CurrentSelectId = nodeId;
        }

        public NodeValue CurrentNode
        {
            get
            {
                return GetNode(CurrentSelectId);
            }
        }

        public NodeValue GetNode(int nodeId)
        {
            return DataSearchTool.GetNode(nodeId);
        }

        public NodeValue GetNode(BehaviorTreeData treeData, int nodeId)
        {
            return DataSearchTool.GetNode(treeData, nodeId);
        }

        public List<NodeValue> FindChild(BehaviorTreeData treeData, int nodeId)
        {
            return DataSearchTool.FindChild(treeData, nodeId);
        }

        public bool CurrentOpenConfigSubTree()
        {
            if (BehaviorDataController.Instance.CurrentOpenSubTree <= 0)
            {
                return false;
            }

            NodeValue subTreeNode = BehaviorDataController.Instance.GetNode(BehaviorDataController.Instance.CurrentOpenSubTree);
            if (null == subTreeNode)
            {
                return false;
            }

            return subTreeNode.subTreeType == (int)SUB_TREE_TYPE.CONFIG;
        }

    }

}
