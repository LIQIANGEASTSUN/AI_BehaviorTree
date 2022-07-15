using System;
using System.Collections.Generic;
using BehaviorTree;

/*
 * 对AI实现方法的抽象
 * 逻辑决策需要数据的抽象：环境变量
 */
public interface IAIPerformer
{
    void UpdateParameter(string name, bool para);
    void UpdateParameter(string name, int para);
    void UpdateParameter(string name, float para);

    bool GetParameterValue(string parameterName, ref int value);

    bool GetParameterValue(string parameterName, ref float value);

    bool GetParameterValue(string parameterName, ref bool value);

}
