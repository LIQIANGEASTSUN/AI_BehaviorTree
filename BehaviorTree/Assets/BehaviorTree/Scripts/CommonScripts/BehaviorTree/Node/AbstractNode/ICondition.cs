using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// condition node interface
    /// </summary>
    public interface ICondition
    {
        ResultType Condition();
    }
}

