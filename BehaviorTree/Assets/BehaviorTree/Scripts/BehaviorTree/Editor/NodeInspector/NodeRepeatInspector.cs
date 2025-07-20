using UnityEditor;

namespace BehaviorTree
{

    public class NodeRepeatInspector : NodeCompositeInspector
    {

        protected override void Common()
        {
            Repeat();
            base.Common();
        }

        private void Repeat()
        {
            nodeValue.repeatTimes = EditorGUILayout.IntField("重复执行次数", nodeValue.repeatTimes);
        }
    }
}
