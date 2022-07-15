using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphicTree
{
    public enum ParameterCompare
    {
        INVALID = 0,
        /// <summary>
        /// Greater
        /// </summary>
        [EnumAttirbute("Greater")]
        GREATER = 1 << 0,

        /// <summary>
        /// Less
        /// </summary>
        [EnumAttirbute("Less")]
        LESS = 1 << 1,

        /// <summary>
        /// Equal
        /// </summary>
        [EnumAttirbute("Equal")]
        EQUALS = 1 << 2,

        /// <summary>
        /// NotEqual
        /// </summary>
        [EnumAttirbute("NotEqual")]
        NOT_EQUAL = 1 << 3,

        /// <summary>
        /// GreaterOrEqual
        /// </summary>
        [EnumAttirbute("GreaterOrEqual")]
        GREATER_EQUALS = 1 << 4,

        /// <summary>
        /// LessOrEqual
        /// </summary>
        [EnumAttirbute("LessOrEqual")]
        LESS_EQUAL = 1 << 5,
    }

    public class ParameterCompareTool
    {

        public static ParameterCompare Compare(NodeParameter self, NodeParameter parameter)
        {
            ParameterCompare result = ParameterCompare.NOT_EQUAL;
            if (self.parameterType != parameter.parameterType)
            {
                Debug.LogError("parameter Type not Equal:" + parameter.parameterName + "    " + parameter.parameterType + "    " + self.parameterType);
                return result;
            }

            if (self.parameterType == (int)ParameterType.Float)
            {
                result = CompareFloat(self.floatValue, parameter.floatValue);
            }
            else if (self.parameterType == (int)ParameterType.Int)
            {
                result = CompareLong(self.intValue, parameter.intValue);
            }
            else if (self.parameterType == (int)ParameterType.Long)
            {
                result = CompareLong(self.longValue, parameter.longValue);
            }
            else if (self.parameterType == (int)ParameterType.Bool)
            {
                result = CompareBool(self.boolValue, parameter.boolValue);
            }
            else if (self.parameterType == (int)ParameterType.String)
            {
                result = CompareString(self.stringValue, parameter.stringValue);
            }

            return result;
        }

        public static ParameterCompare CompareFloat(float floatValue1, float floatValue2)
        {
            ParameterCompare BehaviorCompare = ParameterCompare.INVALID;
            if (floatValue1 > floatValue2)
            {
                BehaviorCompare |= ParameterCompare.GREATER;
            }
            else if (floatValue1 < floatValue2)
            {
                BehaviorCompare |= ParameterCompare.LESS;
            }

            return BehaviorCompare;
        }

        public static ParameterCompare CompareLong(long longValue1, long longValue2)
        {
            ParameterCompare behaviorCompare = ParameterCompare.INVALID;
            if (longValue1 > longValue2)
            {
                behaviorCompare |= ParameterCompare.GREATER;
                behaviorCompare |= ParameterCompare.NOT_EQUAL;
            }
            else if (longValue1 < longValue2)
            {
                behaviorCompare |= ParameterCompare.LESS;
                behaviorCompare |= ParameterCompare.NOT_EQUAL;
            }
            else
            {
                behaviorCompare |= ParameterCompare.EQUALS;
            }

            if (longValue1 >= longValue2)
            {
                behaviorCompare |= ParameterCompare.GREATER_EQUALS;
            }

            if (longValue1 <= longValue2)
            {
                behaviorCompare |= ParameterCompare.LESS_EQUAL;
            }

            return behaviorCompare;
        }

        public static ParameterCompare CompareBool(bool boolValue1, bool boolValue2)
        {
            ParameterCompare behaviorCompare = (boolValue1 == boolValue2) ? ParameterCompare.EQUALS : ParameterCompare.NOT_EQUAL;
            return behaviorCompare;
        }

        public static ParameterCompare CompareString(string stringValue1, string stringValue2)
        {
            ParameterCompare behaviorCompare = (stringValue1.CompareTo(stringValue2) == 0) ? ParameterCompare.EQUALS : ParameterCompare.NOT_EQUAL;
            return behaviorCompare;
        }
    }

}