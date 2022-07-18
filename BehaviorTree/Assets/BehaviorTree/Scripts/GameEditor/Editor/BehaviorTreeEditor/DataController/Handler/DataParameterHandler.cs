using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    public class DataParameterHandler
    {
        public bool AddParameter(List<NodeParameter> parameterList, NodeParameter parameter)
        {
            bool result = true;
            if (string.IsNullOrEmpty(parameter.parameterName))
            {
                string meg = string.Format("Conditional parameters cannot be empty", parameter.parameterName);
                TreeNodeWindow.window.ShowNotification(meg);
                result = false;
            }

            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter tempParameter = parameterList[i];
                if (tempParameter.parameterName.CompareTo(parameter.parameterName) == 0)
                {
                    string meg = string.Format("Conditional parameter :{0} already exists", parameter.parameterName);
                    TreeNodeWindow.window.ShowNotification(meg);
                    result = false;
                    break;
                }
            }

            if (result)
            {
                NodeParameter newParameter = parameter.Clone();
                parameterList.Add(newParameter);
                for (int i = 0; i < parameterList.Count; ++i)
                {
                    parameterList[i].index = i;
                }
            }

            return result;
        }

        public void DelParameter(List<NodeParameter> parameterList, NodeParameter parameter)
        {
            bool result = false;
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter tempParameter = parameterList[i];
                if (tempParameter.parameterName.CompareTo(parameter.parameterName) == 0)
                {
                    parameterList.RemoveAt(i);
                    result = true;
                    break;
                }
            }

            if (result)
            {
                for (int i = 0; i < parameterList.Count; ++i)
                {
                    parameterList[i].index = i;
                }
            }
        }
    }

}
