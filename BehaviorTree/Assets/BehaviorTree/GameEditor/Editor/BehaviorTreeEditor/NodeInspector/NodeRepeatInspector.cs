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
            nodeValue.repeatTimes = EditorGUILayout.IntField("重复执行次数", nodeValue.repeatTimes);
        }

    }

}
