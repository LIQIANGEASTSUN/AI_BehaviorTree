using System.Collections.Generic;

namespace GraphicTree
{

    public interface IConditionCheck
    {
        void InitParmeter();

        void SetParameter(string parameterName, bool boolValue);

        void SetParameter(string parameterName, float floatValue);

        void SetParameter(string parameterName, int intValue);

        void AddParameter(NodeParameter parameter);

        bool Condition(NodeParameter parameter);

        bool ConditionAllAnd(List<NodeParameter> parameterList);

        bool Condition(ConditionParameter conditionParameter);

        List<NodeParameter> GetAllParameter();
    }

}

