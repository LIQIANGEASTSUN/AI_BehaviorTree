using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{

    public class BehaviorNodeInspectorModel
    {
        public NodeValue GetCurrentSelectNode()
        {
            return BehaviorDataController.Instance.CurrentNode;
        }
    }

}