

public interface IAIPerformer
{
    void UpdateParameter(string name, bool para);
    void UpdateParameter(string name, int para);
    void UpdateParameter(string name, float para);

    bool GetParameterValue(string parameterName, ref int value);

    bool GetParameterValue(string parameterName, ref float value);

    bool GetParameterValue(string parameterName, ref bool value);

}
