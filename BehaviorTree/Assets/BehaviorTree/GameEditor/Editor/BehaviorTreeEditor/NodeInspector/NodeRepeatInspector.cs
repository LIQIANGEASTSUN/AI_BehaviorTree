using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// 组合：修饰重复节点
    /// </summary>
    public class NodeRepeatInspector : NodeCompositeInspector
    {

        protected override void Common()
        {
            Repeat();
        }

        private void Repeat()
        {
            string repetitionsTimes = Localization.GetInstance().Format("RepetitionsTimes");
            nodeValue.repeatTimes = EditorGUILayout.IntField(repetitionsTimes, nodeValue.repeatTimes);
        }

    }

}
