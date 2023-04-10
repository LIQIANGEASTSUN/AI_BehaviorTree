using System;
using GraphicTree;

namespace BehaviorTree
{
    public delegate BehaviorTreeData LoadConfigInfoEvent(string fileName);

    public class BehaviorAnalysis
    {
        private static BehaviorAnalysis _instance;
        private static object lockObj = new object();
        public static BehaviorAnalysis GetInstance()
        {
            if (null == _instance)
            {
                lock (lockObj)
                {
                    if (null == _instance)
                    {
                        _instance = new BehaviorAnalysis();
                    }
                }
            }

            return _instance;
        }

        public NodeBase Analysis(long aiFunction, BehaviorTreeData data, IConditionCheck iConditionCheck, Action<int> InvalidSubTreeCallBack)
        {
            int entityId = NewEntityId;
            return Analysis(entityId, aiFunction, data, iConditionCheck, InvalidSubTreeCallBack);
        }

        public NodeBase Analysis(int entityId, long aiFunction, BehaviorTreeData data, IConditionCheck iConditionCheck, Action<int> InvalidSubTreeCallBack)
        {
            NodeBase rootNode = AnalysisTree(entityId, aiFunction, data, iConditionCheck, InvalidSubTreeCallBack);
            return rootNode;
        }

        private NodeBase AnalysisTree(int entityId, long aiFunction, BehaviorTreeData data, IConditionCheck iConditionCheck, Action<int> InvalidSubTreeCallBack)
        {
            NodeBase rootNode = null;
            if (null == data || data.rootNodeId < 0)
            {
                //ProDebug.Logger.LogError("数据无效");
                return rootNode;
            }

            SetParameter(iConditionCheck, data);
            rootNode = AnalysisNode(entityId, aiFunction, data, data.rootNodeId, iConditionCheck, InvalidSubTreeCallBack);

            return rootNode;
        }

        private NodeBase AnalysisNode(int entityId, long aiFunction, BehaviorTreeData data, int nodeId, IConditionCheck iConditionCheck, Action<int> InvalidSubTreeCallBack)
        {
            NodeValue nodeValue = null;
            if (!data.nodeDic.TryGetValue(nodeId, out nodeValue))
            {
                return null;
            }

            if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE && nodeValue.subTreeValue > 0)
            {
                if ((aiFunction & nodeValue.subTreeValue) <= 0)
                {
                    if (null != InvalidSubTreeCallBack)
                    {
                        InvalidSubTreeCallBack(nodeValue.id);
                    }
                    return null;
                }
            }

            //UnityEngine.Profiling.Profiler.BeginSample("AnalysisNode CreateNode");
            NodeBase nodeBase = AnalysisNode(nodeValue, iConditionCheck) as NodeBase;
            nodeBase.NodeId = nodeValue.id;
            nodeBase.EntityId = entityId;
            nodeBase.Priority = nodeValue.priority;
            //UnityEngine.Profiling.Profiler.EndSample();

            if (nodeValue.NodeType == (int)NODE_TYPE.SUB_TREE && nodeValue.subTreeType == (int)SUB_TREE_TYPE.CONFIG)
            {
                //BehaviorTreeData subTreeData = _loadConfigInfoEvent(nodeValue.subTreeConfig);
                BehaviorTreeData subTreeData = DataCenter.behaviorData.GetBehaviorInfo(nodeValue.subTreeConfig);
                if (null != subTreeData)
                {
                    NodeBase subTreeNode = AnalysisTree(entityId, aiFunction, subTreeData, iConditionCheck, InvalidSubTreeCallBack);
                    NodeComposite composite = (NodeComposite)(nodeBase);
                    composite.AddNode(subTreeNode);
                }
            }

            //UnityEngine.Profiling.Profiler.BeginSample("AnalysisNode IsLeafNode");
            if (!IsLeafNode(nodeValue.NodeType))
            {
                NodeComposite composite = (NodeComposite)nodeBase;
                for (int i = 0; i < nodeValue.childNodeList.Count; ++i)
                {
                    int childNodeId = nodeValue.childNodeList[i];
                    NodeBase childNode = AnalysisNode(entityId, aiFunction, data, childNodeId, iConditionCheck, InvalidSubTreeCallBack);
                    if (null != childNode)
                    {
                        composite.AddNode(childNode);
                    }
                }
            }
            //UnityEngine.Profiling.Profiler.EndSample();

            return nodeBase;
        }

        private bool IsLeafNode(int type)
        {
            return (type == (int)NODE_TYPE.ACTION) || (type == (int)NODE_TYPE.CONDITION);
        }

        private AbstractNode AnalysisNode(NodeValue nodeValue, IConditionCheck iConditionCheck)
        {
            AbstractNode nodeBase = BehaviorConfigNode.Instance.GetNode(nodeValue.identificationName);
            if (nodeBase is NodeBase nb)
            {
                nb.SetData(nodeValue);
            }

            if (nodeBase is ISetConditionCheck iscc)
            {
                iscc.SetConditionCheck(iConditionCheck);
            }

            return nodeBase;
        }

        private void SetParameter(IConditionCheck iConditionCheck, BehaviorTreeData data)
        {
            foreach (var parameter in data.parameterList)
            {
                iConditionCheck.AddParameter(parameter.Clone());
            }
        }

        private static int _entityId = 0;
        private int NewEntityId
        {
            get { return ++_entityId; }
        }
    }
}