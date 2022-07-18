using UnityEngine;
using System.Collections.Generic;
using System;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// dynamic subtree
    /// </summary>
    public abstract class NodeSubTreeDynamicAB : NodeSubTree
    {
        protected IConditionCheck _iconditionCheck = null;

        protected bool _needLoadConfig = false;
        protected string _subTreeConfig = string.Empty;

        protected Dictionary<string, DynamicData> _dataDic = new Dictionary<string, DynamicData>();
        private const int _cacheCount = 30;
        public NodeSubTreeDynamicAB() : base()
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();

            CalculateNewSubTree();

            if (NeedReloadConfig())
            {
                ReloadConfig();
                _needLoadConfig = false;
            }
        }

        protected abstract void CalculateNewSubTree();

        protected void SetSubTreeConfig(string config)
        {
            if (string.IsNullOrEmpty(config))
            {
                return;
            }

            if (string.IsNullOrEmpty(_subTreeConfig)
                || _subTreeConfig.CompareTo(config) != 0
                || (GetChilds().Count <= 0))
            {
                _needLoadConfig = true;
            }
            _subTreeConfig = config;
        }

        protected string GetSubTreeConfig()
        {
            return _subTreeConfig;
        }

        public void SetConditionCheck(IConditionCheck iConditionCheck)
        {
            _iconditionCheck = iConditionCheck;
        }

        protected virtual void ReloadConfig()
        {
            ClearChild();

            string aiConfig = GetSubTreeConfig();
            //Debug.LogError("TreeConfig ReloadConfig:" + aiConfig + "   Time:" + Time.realtimeSinceStartup);

            NodeBase subTreeNode = GetDynamicTree(aiConfig);
            AddNode(subTreeNode);
        }

        protected bool NeedReloadConfig()
        {
            return _needLoadConfig;
        }

        private NodeBase GetDynamicTree(string aiConfig)
        {
            DynamicData dynamicData = null;
            if (!_dataDic.TryGetValue(aiConfig, out dynamicData))
            {
                dynamicData = AddDynamicTree(aiConfig);
            }

            NodeBase nodeBase = dynamicData.TreeNode;
            return nodeBase;
        }

        private DynamicData AddDynamicTree(string aiConfig)
        {
            BehaviorTreeData data = DataCenter.behaviorData.GetBehaviorInfo(aiConfig);
            long aiFunction = AIFunction();
            NodeBase subTreeNode = BehaviorAnalysis.GetInstance().Analysis(EntityId, aiFunction, data, _iconditionCheck, null);
            TreeSetOwner(subTreeNode);

            DynamicData dynamicData = new DynamicData(Time.realtimeSinceStartup, subTreeNode);
            _dataDic[aiConfig] = dynamicData;

            CheckRemove();
            return dynamicData;
        }

        protected abstract void TreeSetOwner(NodeBase subTreeNode);

        private void CheckRemove()
        {
            if (_dataDic.Count > _cacheCount)
            {
                string removeKey = string.Empty;
                float min = float.MaxValue;
                foreach (var kv in _dataDic)
                {
                    if (kv.Value.Time < min)
                    {
                        min = kv.Value.Time;
                        removeKey = kv.Key;
                    }
                }
                _dataDic.Remove(removeKey);
            }
        }

        protected abstract Int64 AIFunction();

    }


    public class DynamicData
    {
        private float _time;
        private NodeBase _treeNode;

        public DynamicData(float time, NodeBase nodeBase)
        {
            _time = time;
            _treeNode = nodeBase;
        }

        public float Time
        {
            get
            {
                return _time;
            }
        }

        public NodeBase TreeNode
        {
            get
            {
                return _treeNode;
            }
        }
    }

}

