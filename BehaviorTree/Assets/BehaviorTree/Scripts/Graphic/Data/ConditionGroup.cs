using System.Collections.Generic;


namespace GraphicTree
{
    public class ConditionGroup
    {
        public int index;
        public List<string> parameterList = new List<string>();

        public ConditionGroup Clone()
        {
            ConditionGroup group = new ConditionGroup();
            group.index = this.index;
            group.parameterList.AddRange(parameterList);
            return group;
        }
    }
}