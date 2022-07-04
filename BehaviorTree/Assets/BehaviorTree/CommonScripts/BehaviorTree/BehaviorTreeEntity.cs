﻿using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace BehaviorTree
{
    public class BehaviorTreeEntity
    {
        private NodeBase _rootNode;
        private IConditionCheck _iconditionCheck = null;
        private List<int> _invalidSubTreeList = new List<int>();
        private int _entityId;
        private static int _currentDebugEntityId;

        public BehaviorTreeEntity(long aiFunction, BehaviorTreeData data)
        {
            _iconditionCheck = new ConditionCheck();
            //UnityEngine.Profiling.Profiler.BeginSample("Analysis");
            _rootNode = BehaviorAnalysis.GetInstance().Analysis(aiFunction, data, _iconditionCheck, AddInvalidSubTree);
            //UnityEngine.Profiling.Profiler.EndSample();
            if (null != _rootNode)
            {
                _entityId = _rootNode.EntityId;
            }
        }

        public ConditionCheck ConditionCheck
        {
            get { return (ConditionCheck)_iconditionCheck; }
        }

        public List<int> InvalidSubTreeList
        {
            get
            {
                return _invalidSubTreeList;
            }
        }

        public NodeBase RootNode
        {
            get { return _rootNode; }
        }

        public int EntityId
        {
            get { return _entityId; }
        }

        private void AddInvalidSubTree(int nodeId)
        {
            if (!Application.isEditor)
            {
                return;
            }
            if (!_invalidSubTreeList.Contains(nodeId))
            {
                _invalidSubTreeList.Add(nodeId);
            }
        }

        public void Execute()
        {
            if (null != _rootNode)
            {
                _rootNode.Preposition();
                ResultType resultType = _rootNode.Execute();
                _rootNode.Postposition(resultType);
            }
        }

        public void Clear()
        {
            if (null != _rootNode)
            {
                _rootNode.Postposition(ResultType.Fail);
            }
            //ConditionCheck.InitParmeter();
        }

        public void Exit()
        {
            if (null != _rootNode)
            {
                _rootNode.Postposition(ResultType.Fail);
            }
        }

        public void Release()
        {
            Exit();

            _rootNode = null;
            _iconditionCheck = null;
            //ConditionCheck.InitParmeter();
        }

        public static int CurrentDebugEntityId
        {
            get { return _currentDebugEntityId; }
            set { _currentDebugEntityId = value;
                NodeNotify.SetCurrentEndityId(_currentDebugEntityId);
            }
        }

    }

}
