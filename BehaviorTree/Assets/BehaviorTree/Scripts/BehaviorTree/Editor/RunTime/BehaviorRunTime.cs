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
                behaviorTreeData.nodeDic.Add(nodeValue.id, nodeValue);
            }

            //BehaviorAnalysis.Instance.SetLoadConfigEvent(LoadConfig);
            _behaviorTreeEntity = new BehaviorTreeEntity(long.MaxValue, behaviorTreeData);
            BehaviorTreeEntity.CurrentDebugEntityId = _behaviorTreeEntity.EntityId;
            SetRunTimeDrawNode(_behaviorTreeEntity);
            NodeNotify.Clear();
        }

        public void Reset(BehaviorTreeData behaviorTreeData, BehaviorTreeEntity behaviorTreeEntity)
        {
            DataController.Instance.BehaviorTreeData = behaviorTreeData;
            _behaviorTreeEntity = behaviorTreeEntity;
            BehaviorTreeEntity.CurrentDebugEntityId = _behaviorTreeEntity.EntityId;
            SetRunTimeDrawNode( behaviorTreeEntity);
            DataController.Instance.PlayState = BehaviorPlayType.PLAY;
            NodeNotify.Clear();
        }

        private void SetRunTimeDrawNode(BehaviorTreeEntity behaviorTreeEntity)
        {
            if (!Application.isPlaying || !Application.isEditor)
            {
                return;
            }
            DataController.Instance.RunTimeInvalidSubTreeHash.Clear();
            foreach (var nodeId in behaviorTreeEntity.InvalidSubTreeList)
            {
                DataController.Instance.RunTimeInvalidSubTreeHash.Add(nodeId);
            }
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
            if (DataController.Instance.PlayState == BehaviorPlayType.STOP
                || (DataController.Instance.PlayState == BehaviorPlayType.PAUSE))
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

