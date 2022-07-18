using System.Collections.Generic;

namespace GraphicTree
{

    public class ConditionCheck : IConditionCheck
    {
        /// <summary>
        /// Store all the parameters used, data from the configuration,
        /// Save it in _environmentParameterDic when Init
        /// </summary>
        private List<NodeParameter> _parameterList = new List<NodeParameter>();

        /// <summary>
        /// Saves the values of all parameters in the environment variable
        /// </summary>
        private Dictionary<string, NodeParameter> _environmentParameterDic = new Dictionary<string, NodeParameter>();

        public ConditionCheck() { }

        /// <summary>
        /// Set the value of the environment variable of type bool
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="boolValue"></param>
        public void SetParameter(string parameterName, bool boolValue)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null == parameter)
            {
                parameter = CreateParameter(parameterName, (int)ParameterType.Bool);
                _environmentParameterDic[parameterName] = parameter;
            }
            parameter.boolValue = boolValue;
        }

        /// <summary>
        /// Set the value of the environment variable of type float
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="floatValue"></param>
        public void SetParameter(string parameterName, float floatValue)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null == parameter)
            {
                parameter = CreateParameter(parameterName, (int)ParameterType.Float);
                _environmentParameterDic[parameterName] = parameter;
            }
            parameter.floatValue = floatValue;
        }

        /// <summary>
        /// Set the value of the environment variable of type int
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="intValue"></param>
        public void SetParameter(string parameterName, int intValue)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null == parameter)
            {
                parameter = CreateParameter(parameterName, (int)ParameterType.Int);
                _environmentParameterDic[parameterName] = parameter;
            }
            parameter.intValue = intValue;
        }

        /// <summary>
        /// Set the value of the environment variable of type long
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="longValue"></param>
        public void SetParameter(string parameterName, long longValue)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null == parameter)
            {
                parameter = CreateParameter(parameterName, (int)ParameterType.Long);
                _environmentParameterDic[parameterName] = parameter;
            }
            parameter.longValue = longValue;
        }

        /// <summary>
        /// Set the value of the environment variable of type string
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="stringValue"></param>
        public void SetParameter(string parameterName, string stringValue)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null == parameter)
            {
                parameter = CreateParameter(parameterName, (int)ParameterType.String);
                _environmentParameterDic[parameterName] = parameter;
            }
            parameter.stringValue = stringValue;
        }

        private NodeParameter CreateParameter(string parameterName, int type)
        {
            NodeParameter nodeParameter = new NodeParameter();
            nodeParameter.parameterName = parameterName;
            nodeParameter.parameterType = type;
            return nodeParameter;
        }

        /// <summary>
        /// Add environment variables
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameter(NodeParameter parameter)
        {
            NodeParameter cache = GetNodeParametere(parameter.parameterName);
            if (null == cache)
            {
                _environmentParameterDic[parameter.parameterName] = parameter;
            }
        }

        /// <summary>
        /// Get the environment variable based on the parameter name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        private NodeParameter GetNodeParametere(string parameterName)
        {
            NodeParameter parameter = null;
            _environmentParameterDic.TryGetValue(parameterName, out parameter);
            return parameter;
        }

        /// <summary>
        /// Get the value of the environment variable based on the parameter name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetParameterValue(string parameterName, ref int value)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null != parameter)
            {
                value = parameter.intValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the value of the environment variable based on the parameter name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetParameterValue(string parameterName, ref long value)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null != parameter)
            {
                value = parameter.longValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the value of the environment variable based on the parameter name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetParameterValue(string parameterName, ref float value)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null != parameter)
            {
                value = parameter.floatValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the value of the environment variable based on the parameter name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetParameterValue(string parameterName, ref bool value)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null != parameter)
            {
                value = parameter.boolValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the value of the environment variable based on the parameter name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetParameterValue(string parameterName, ref string value)
        {
            NodeParameter parameter = GetNodeParametere(parameterName);
            if (null != parameter)
            {
                value = parameter.stringValue;
                return true;
            }
            return false;
        }

        public void InitParmeter()
        {

        }

        public bool Condition(NodeParameter parameter)
        {
            NodeParameter environmentParameter = null;
            if (!_environmentParameterDic.TryGetValue(parameter.parameterName, out environmentParameter))
            {
                return false;
            }

            if (environmentParameter.parameterType != parameter.parameterType)
            {
                return false;
            }

            ParameterCompare compare = ParameterCompareTool.Compare(environmentParameter, parameter);
            int value = (parameter.compare) & (int)compare;
            return value > 0;
        }

        public bool ConditionAllAnd(List<NodeParameter> parameterList)
        {
            bool result = true;
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter temp = parameterList[i];
                bool value = Condition(temp);
                if (!value)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool Condition(ConditionParameter conditionParameter)
        {
            bool result = true;
            List<ConditionGroupParameter> groupList = conditionParameter.GetGroupList();
            for (int i = 0; i < groupList.Count; ++i)
            {
                ConditionGroupParameter groupParameter = groupList[i];
                result = true;

                for (int j = 0; j < groupParameter.parameterList.Count; ++j)
                {
                    NodeParameter parameter = groupParameter.parameterList[j];
                    bool value = Condition(parameter);
                    if (!value)
                    {
                        result = false;
                        break;
                    }
                }

                if (result)
                {
                    break;
                }
            }

            return result;
        }

        public List<NodeParameter> GetAllParameter()
        {
            List<NodeParameter> parameterList = new List<NodeParameter>();
            foreach (var kv in _environmentParameterDic)
            {
                parameterList.Add(kv.Value);
            }

            return parameterList;
        }

    }

}
