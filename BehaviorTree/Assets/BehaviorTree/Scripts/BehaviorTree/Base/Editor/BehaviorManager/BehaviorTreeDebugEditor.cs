using UnityEditor;

namespace BehaviorTree
{
    [CustomEditor(typeof(BehaviorTreeDebug))]
    public class BehaviorTreeDebugEditor : Editor
    {
        private BehaviorTreeDebug _treeDebug;

        private void OnEnable()
        {
            _treeDebug = target as BehaviorTreeDebug;
            _treeDebug.OnSelect(true);
        }

    }
}