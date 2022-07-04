﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    /// <summary>
    /// 直到为Fail修饰节点， 一直执行节点，直到返回结果为 Fail。
    /// </summary>
    public class NodeDecoratorUntilFail : NodeDecoratorUntil
    {
        public static string descript = "修饰_直到Fail：\n" +
                                        "执行节点 \n" +
                                        "如果节点返回结果不是 Fail \n" +
                                        "向父节点返回 Running \n\n" +

                                        "直到节点返回 Fail，向父节点返回 Success";

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


