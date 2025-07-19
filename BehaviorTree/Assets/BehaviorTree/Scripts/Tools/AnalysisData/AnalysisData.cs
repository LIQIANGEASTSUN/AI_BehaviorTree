using LitJson;

public class AnalysisData
{

    public static ConfigDataGroup Analysis(byte[] byteDatas)
    {
        string json = System.Text.Encoding.UTF8.GetString(byteDatas);
        return Analysis(json);
    }

    public static ConfigDataGroup Analysis(string json)
    {
        ConfigDataGroup group = JsonMapper.ToObject<ConfigDataGroup>(json);
        return group;
    }

}
