using UnityEngine;
using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 行为节点(叶节点)
    /// </summary>
    public class NodeAction : NodeLeaf, IAction
    {
        protected List<NodeParameter> _parameterList = new List<NodeParameter>();

        public NodeAction() : base()
        {
            SetNodeType(NODE_TYPE.ACTION);
        }

        public override ResultType Execute()
        {
            ResultType resultType = ResultType.Fail;
            if (!Application.isPlaying)
            {
                // 编辑器下预览用
                resultType = ResultType.Running;
            }
            else
            {
                resultType = DoAction();
            }

            NodeNotify.NotifyExecute(EntityId, NodeId, (int)resultType, Time.realtimeSinceStartup);
            return resultType;
        }

        public void SetParameters(List<NodeParameter> parameterList)
        {
            _parameterList.AddRange(parameterList);
            //if (parameterList.Count > 0)
            //{
            //    _parameterList.AddRange(parameterList);
            //}
        }

        public virtual ResultType DoAction()
        {
            return ResultType.Success;
        }

    }

}