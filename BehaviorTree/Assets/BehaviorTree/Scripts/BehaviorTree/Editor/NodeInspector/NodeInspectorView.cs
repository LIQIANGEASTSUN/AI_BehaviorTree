using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class NodeInspectorView
    {
        private NodeCompositeInspector nodeCompositeInspector = new NodeCompositeInspector();
        private Dictionary<NODE_TYPE, NodeBaseInspector> _inspectorDic = new Dictionary<NODE_TYPE, NodeBaseInspector>();
        public NodeInspectorView()
        {
            _inspectorDic[NODE_TYPE.CONDITION] = new NodeConditionInspector();
            _inspectorDic[NODE_TYPE.ACTION] = new NodeActionInspector();
            _inspectorDic[NODE_TYPE.SUB_TREE] = new NodeSubTreeInspector();
            _inspectorDic[NODE_TYPE.DECORATOR_REPEAT] = new NodeRepeatInspector();

            NodeIfInspector nodeIfInspector = new NodeIfInspector();
            _inspectorDic[NODE_TYPE.IF_JUDEG_PARALLEL] = nodeIfInspector;
            _inspectorDic[NODE_TYPE.IF_JUDEG_SEQUENCE] = nodeIfInspector;

            _inspectorDic[NODE_TYPE.RANDOM_PRIORITY] = new NodeRandomPriorityInspector();
        }

        private NodeBaseInspector GetBaseInspector(NodeValue nodeValue)
        {
            NodeBaseInspector inspector = null;
            if (!_inspectorDic.TryGetValue((NODE_TYPE)nodeValue.NodeType, out inspector))
            {
                inspector = nodeCompositeInspector;
            }
            inspector.SetNodeValue(nodeValue);
            return inspector;
        }

        public void Draw(NodeValue nodeValue)
        {
            if (null == nodeValue)
            {
                string msg = Localization.GetInstance().Format("NoSelectNode");
                EditorGUILayout.LabelField(msg);
                return;
            }

            NodeBaseInspector nodeBaseInspector = GetBaseInspector(nodeValue);

            GUIEnableTool.Enable = !DataController.Instance.CurrentOpenConfigSubTree();
            EditorGUILayout.BeginVertical("box");
            {
                nodeBaseInspector.Draw();
            }
            EditorGUILayout.EndVertical();
            GUIEnableTool.Enable = true;

            ParentInfo();
        }

        private int _nodeID = -1;
        private void ParentInfo()
        {
            GUILayout.Space(20);
            string nodeId = Localization.GetInstance().Format("NodeId");
            _nodeID = EditorGUILayout.IntField(nodeId, _nodeID);
            string printParentPaths = Localization.GetInstance().Format("PrintParentPaths");
            if (GUILayout.Button(printParentPaths))
            {
                DebugNodeParentInfoTool debugNodeParentInfoTool = new DebugNodeParentInfoTool();
                debugNodeParentInfoTool.DebugNodeParentInfo(_nodeID);
            }
        }
    }
}