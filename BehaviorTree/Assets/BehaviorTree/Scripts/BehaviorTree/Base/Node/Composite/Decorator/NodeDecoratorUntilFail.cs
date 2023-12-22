using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    /// <summary>
    /// execute child node until the child node returns fail
    /// </summary>
    public class NodeDecoratorUntilFail : NodeDecoratorUntil
    {
        public static string descript = "DecoratorUntilFailNodeFunctionDescript";

        public NodeDecoratorUntilFail() : base(NODE_TYPE.DECORATOR_UNTIL_FAIL)
        {
            SetDesiredResult(ResultType.Fail);
        }

        public override ResultType Execute()
        {
            return base.Execute();
        }

    }
}


