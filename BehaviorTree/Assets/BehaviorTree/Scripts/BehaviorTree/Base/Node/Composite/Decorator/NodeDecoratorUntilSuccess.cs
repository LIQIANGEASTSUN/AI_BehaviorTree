using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// execute child node until the child node returns success
    /// </summary>
    public class NodeDecoratorUntilSuccess : NodeDecoratorUntil
    {
        public static string descript = "DecoratorUntilSuccessNodeFunctionDescript";

        public NodeDecoratorUntilSuccess() : base(NODE_TYPE.DECORATOR_UNTIL_SUCCESS)
        {
            SetDesiredResult(ResultType.Success);
        }

        public override ResultType Execute()
        {
            return base.Execute();
        }

    }
}