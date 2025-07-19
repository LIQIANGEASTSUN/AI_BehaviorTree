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
            string repetitionsTimes = Localization.GetInstance().Format("RepetitionsTimes");
            nodeValue.repeatTimes = EditorGUILayout.IntField(repetitionsTimes, nodeValue.repeatTimes);
        }

    }

}
