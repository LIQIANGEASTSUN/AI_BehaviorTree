using System.Collections.Generic;

namespace GraphicTree
{
    public class ConditionGroupParameter
    {
        public List<NodeParameter> parameterList = new List<NodeParameter>();

        public void AddParameter(NodeParameter parameter)
        {
            parameterList.Add(parameter);
        }
    }

    public class ConditionParameter
    {
        private bool init = false;
        private List<ConditionGroupParameter> groupList = new List<ConditionGroupParameter>();

        public ConditionParameter() {  }

        public void Init(List<ConditionGroup> conditionGroupList, List<NodeParameter> parameterList)
        {
            if (init)
            {
                return;
            }
            init = true;

            for (int i = 0; i < conditionGroupList.Count; ++i)
            {
                ConditionGroup conditionGroup = conditionGroupList[i];
                ConditionGroupParameter group = GetParameter(conditionGroup, parameterList);
                groupList.Add(group);
            }
        }

        private ConditionGroupParameter GetParameter(ConditionGroup conditionGroup, List<NodeParameter> parameterList)
        {
            ConditionGroupParameter group = new ConditionGroupParameter();
            for (int i = 0; i < conditionGroup.parameterList.Count; ++i)
            {
                string parameter = conditionGroup.parameterList[i];
                for (int j = 0; j < parameterList.Count; ++j)
                {
                    if (parameter.CompareTo(parameterList[j].parameterName) == 0)
                    {
                        group.parameterList.Add(parameterList[j]);
                    }
                }
            }

            return group;
        }

        public List<ConditionGroupParameter> GetGroupList()
        {
            return groupList;
        }
    }
}
