using System.Collections.Generic;

public class ConfigData
{
    public string fileName;
    public byte[] byteDatas;
}


public class ConfigDataGroup
{
    public List<ConfigData> dataList = new List<ConfigData>();
}
