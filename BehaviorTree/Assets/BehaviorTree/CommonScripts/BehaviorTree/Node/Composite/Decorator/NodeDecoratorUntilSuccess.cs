using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 直到为Success修饰节点， 一直执行节点，直到返回结果为 Success。
    /// </summary>
    public class NodeDecoratorUntilSuccess : NodeDecoratorUntil
    {
        public static string descript = "修饰_直到Success：\n" +
                                        "执行节点 \n" +
                                        "如果节点返回结果不是 Success \n" +
                                        "向父节点返回 Running \n\n" +

                                        "直到节点返回 Success，向父节点返回 Success";

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