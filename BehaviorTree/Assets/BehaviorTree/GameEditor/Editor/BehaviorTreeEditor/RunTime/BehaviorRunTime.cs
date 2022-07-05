using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{

    public class BehaviorRunTime
    {
        public static readonly BehaviorRunTime Instance = new BehaviorRunTime();

        private BehaviorTreeEntity _behaviorTreeEntity = null;

        private RunTimeRotateGo _runtimeRotateGo;
        private BehaviorRunTime()
        {
        }

        public void Init()
        {
            _runtimeRotateGo = new RunTimeRotateGo();

            BehaviorTreeDebug.AddTreeDebugEvent(Reset);
        }

        public void OnDestroy()
        {
            _runtimeRotateGo.OnDestroy();
            BehaviorTreeDebug.RemoveTreeDebugEvent(Reset);
        }

        public void Reset(BehaviorTreeData behaviorTreeData)
        {
            behaviorTreeData.nodeDic.Clear();
            for (int i = 0; i < behaviorTreeData.nodeList.Count; ++i)
            {
                NodeValue nodeValue = behaviorTreeData.nodeList[i];
                int index = EnumNames.GetEnumIndex<NODE_TYPE>((NODE_TYPE)nodeValue.NodeType);
                string name = EnumNames.GetEnumName<NODE_TYPE>(index);
                nodeValue.nodeName = Localization.GetInstance().Format(name);

                behaviorTreeData.nodeDic.Add(nodeValue.id, nodeValue);
            }

            BehaviorAnalysis.GetInstance().SetLoadConfigEvent(LoadConfig);
            _behaviorTreeEntity = new BehaviorTreeEntity(long.MaxValue, behaviorTreeData);
            BehaviorTreeEntity.CurrentDebugEntityId = _behaviorTreeEntity.EntityId;
            SetRunTimeDrawNode(_behaviorTreeEntity);
            NodeNotify.Clear();
        }

        public void Reset(BehaviorTreeData behaviorTreeData, BehaviorTreeEntity behaviorTreeEntity)
        {
            BehaviorDataController.Instance.BehaviorTreeData = behaviorTreeData;
            _behaviorTreeEntity = behaviorTreeEntity;
            BehaviorTreeEntity.CurrentDebugEntityId = _behaviorTreeEntity.EntityId;
            SetRunTimeDrawNode( behaviorTreeEntity);
            BehaviorDataController.Instance.PlayState = BehaviorPlayType.PLAY;
            NodeNotify.Clear();
        }

        private void SetRunTimeDrawNode(BehaviorTreeEntity behaviorTreeEntity)
        {
            if (!Application.isPlaying || !Application.isEditor)
            {
                return;
            }
            BehaviorDataController.Instance.RunTimeInvalidSubTreeHash.Clear();
            foreach (var nodeId in behaviorTreeEntity.InvalidSubTreeList)
            {
                BehaviorDataController.Instance.RunTimeInvalidSubTreeHash.Add(nodeId);
            }
        }

        private BehaviorTreeData LoadConfig(string fileName)
        {
            ConfigFileLoad configFileLoad = new ConfigFileLoad();
            BehaviorTreeData behaviorTreeData = configFileLoad.ReadFile(fileName, false);
            behaviorTreeData.nodeDic.Clear();
            for (int i = 0; i < behaviorTreeData.nodeList.Count; ++i)
            {
                NodeValue nodeValue = behaviorTreeData.nodeList[i];
                behaviorTreeData.nodeDic.Add(nodeValue.id, nodeValue);
            }

            return behaviorTreeData;
        }

        public void Update()
        {
            _runtimeRotateGo.Update();

            Execute();
        }

        public BehaviorTreeEntity BehaviorTreeEntity
        {
            get
            {
                return _behaviorTreeEntity;
            }
        }

        public void Execute()
        {
            if (BehaviorDataController.Instance.PlayState == BehaviorPlayType.STOP
                || (BehaviorDataController.Instance.PlayState == BehaviorPlayType.PAUSE))
            {
                return;
            }

            if (Application.isPlaying)
            {
                return;
            }

            if (null != _behaviorTreeEntity)
            {
                _behaviorTreeEntity.Execute();
            }
        }
    }

}


public class RunTimeRotateGo
{
    private GameObject go;

    public RunTimeRotateGo()
    {

    }

    public void OnDestroy()
    {
        GameObject.DestroyImmediate(go);
    }

    public void Update()
    {
        if (!go)
        {
            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = Vector3.zero;
            go.transform.localScale = Vector3.one * 0.001f;
        }
        go.transform.Rotate(0, 5, 0);
    }

}

