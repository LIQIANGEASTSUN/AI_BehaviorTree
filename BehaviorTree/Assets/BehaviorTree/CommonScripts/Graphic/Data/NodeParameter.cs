using UnityEngine;
using System;

namespace GraphicTree
{
    public class NodeParameter
    {
        public int parameterType = 0;
        public string parameterName = string.Empty;
        public string CNName = string.Empty;
        public int index;
        public int intValue = 0;
        public long longValue = 0;
        public float floatValue = 0;
        public bool boolValue = false;
        public string stringValue = string.Empty;
        public int compare;

        public NodeParameter Clone()
        {
            NodeParameter newParameter = new NodeParameter();
            newParameter.CloneFrom(this);
            return newParameter;
        }

        public void CloneFrom(NodeParameter parameter)
        {
            parameterType = parameter.parameterType;
            parameterName = parameter.parameterName;
            CNName = parameter.CNName;
            index = parameter.index;
            intValue = parameter.intValue;
            longValue = parameter.longValue;
            floatValue = parameter.floatValue;
            boolValue = parameter.boolValue;
            stringValue = parameter.stringValue;
            compare = parameter.compare;
        }

    }
}



