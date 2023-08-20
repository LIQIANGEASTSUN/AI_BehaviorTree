using System.Collections.Generic;

namespace BehaviorTree
{
    public struct SubTreeEntryNodeCP
    {
        public int nodeId;
        public int parentNodeId;
        public int parentSubTreeNodeId;
    }

    public struct SubTreeCheckData
    {
        public int subTreeNodeId;
        public List<SubTreeEntryNodeCP> entryList;
    }

    public class CheckSubTreeTool
    {
        public static ISubTreeException GetException(SubTreeCheckType type)
        {
            if (type == SubTreeCheckType.Editor)
            {
                return new SubTreeExceptionEditor();
            }
            if (type == SubTreeCheckType.Update)
            {
                return new SubTreeExceptionUpdate();
            }
            return null;
        }

        public static void CheckSubTreeEditor(List<NodeValue> nodeValueList)
        {
            ISubTreeException exception = GetException(SubTreeCheckType.Editor);
            CheckSubTree(exception, nodeValueList);
        }

        public static void CheckSubTreeUpdate(List<NodeValue> nodeValueList)
        {
            ISubTreeException exception = GetException(SubTreeCheckType.Update);
            CheckSubTree(exception, nodeValueList);
        }

        private static void CheckSubTree(ISubTreeException exception, List<NodeValue> nodeValueList)
        {
            Dictionary<int, SubTreeCheckData> dic = GetCheckData(nodeValueList);
            string meg = string.Empty;
            foreach (var kv in dic)
            {
                List<SubTreeEntryNodeCP> entryList = kv.Value.entryList;
                if (entryList.Count <= 0)
                {
                    exception.NotEntryNode(kv.Value.subTreeNodeId);
                    break;
                }
                else if (entryList.Count > 1)
                {
                    exception.MultipleEntryNode(kv.Value.subTreeNodeId);
                    break;
                }

                foreach (var node in entryList)
                {
                    if (node.parentNodeId != node.parentSubTreeNodeId)
                    {
                        exception.ParentNodeId(node, nodeValueList);
                        break;
                    }
                }
            }
        }

        private static Dictionary<int, SubTreeCheckData> GetCheckData(List<NodeValue> nodeValueList)
        {
            Dictionary<int, SubTreeCheckData> dic = new Dictionary<int, SubTreeCheckData>();
            for (int i = 0; i < nodeValueList.Count; i++)
            {
                NodeValue nodeValue = nodeValueList[i];
                if (nodeValue.parentSubTreeNodeId < 0)
                {
                    continue;
                }

                if (!dic.ContainsKey(nodeValue.parentSubTreeNodeId))
                {
                    SubTreeCheckData subTreeCheckData = new SubTreeCheckData();
                    subTreeCheckData.entryList = new List<SubTreeEntryNodeCP>();
                    dic[nodeValue.parentSubTreeNodeId] = subTreeCheckData;
                }
                if (nodeValue.subTreeEntry)
                {
                    SubTreeEntryNodeCP subTreeEntry = new SubTreeEntryNodeCP();
                    subTreeEntry.nodeId = nodeValue.id;
                    subTreeEntry.parentNodeId = nodeValue.parentNodeID;
                    subTreeEntry.parentSubTreeNodeId = nodeValue.parentSubTreeNodeId;
                    dic[nodeValue.parentSubTreeNodeId].entryList.Add(subTreeEntry);
                }
            }

            return dic;
        }
    }

    public enum SubTreeCheckType
    {
        Editor,
        Update,
    }

    public interface ISubTreeException
    {
        void NotEntryNode(int subTreeNodeId);

        void MultipleEntryNode(int subTreeNodeId);

        void ParentNodeId(SubTreeEntryNodeCP entryNode, List<NodeValue> nodeValueList);
    }

    public class SubTreeExceptionEditor : ISubTreeException
    {
        public void MultipleEntryNode(int subTreeNodeId)
        {
            string meg = string.Format("The subtree _{0} has multiple entry nodes", subTreeNodeId);
            TreeNodeWindow.window.ShowNotification(meg);
        }

        public void NotEntryNode(int subTreeNodeId)
        {
            string meg = string.Format("Subtree _{0} has no entry node", subTreeNodeId);
            TreeNodeWindow.window.ShowNotification(meg);
        }

        public void ParentNodeId(SubTreeEntryNodeCP entryNode, List<NodeValue> nodeValueList)
        {
            string meg = string.Format("The parent node {1} of subtree entry node {0} is different from that of subtree node {2}", entryNode.parentNodeId, entryNode.nodeId, entryNode.parentSubTreeNodeId);
            TreeNodeWindow.window.ShowNotification(meg);
        }
    }

    public class SubTreeExceptionUpdate : ISubTreeException
    {
        public void MultipleEntryNode(int subTreeNodeId)
        {
            string meg = string.Format("The subtree _{0} has multiple entry nodes", subTreeNodeId);
            UnityEngine.Debug.LogError(meg);
        }

        public void NotEntryNode(int subTreeNodeId)
        {
            string meg = string.Format("Subtree _{0} has no entry node", subTreeNodeId);
            UnityEngine.Debug.LogError(meg);
        }

        public void ParentNodeId(SubTreeEntryNodeCP entryNode, List<NodeValue> nodeValueList)
        {
            string meg = string.Format("The parent node {1} of subtree entry node {0} is different from that of subtree node {2}", entryNode.parentNodeId, entryNode.nodeId, entryNode.parentSubTreeNodeId);
            UnityEngine.Debug.LogError(meg);
            if (entryNode.parentNodeId < 0)
            {
                NodeValue node = nodeValueList.Find(a => a.id == entryNode.nodeId);
                node.parentNodeID = entryNode.parentSubTreeNodeId;
            }
            else
            {
                UnityEngine.Debug.LogError("nodeParentId:" + entryNode.parentNodeId);
            }
        }
    }

}

