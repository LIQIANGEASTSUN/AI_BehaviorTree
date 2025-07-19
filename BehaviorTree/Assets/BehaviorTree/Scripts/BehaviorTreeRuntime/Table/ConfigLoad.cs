using System.Collections;
using UnityEngine;
using System;

public class ConfigLoad
{
    public static Action loadEndCallBack;
    public void Init()
    {
        GameController.Instance.StartCoroutine(LoadConfig());
    }

    private byte[] byteData = new byte[] { };
    private string textContent = string.Empty;
    public IEnumerator LoadConfig()
    {
        // All configuration files are merged into the BehaviorTreeConfig.bytes file
        //yield return GameController.Instance.StartCoroutine(LoadData("Bina", "behavior_tree_config.bytes", 1));
        LoadResourceData("behavior_tree_config.bytes");

        yield return new WaitForEndOfFrame();
        DataCenter.behaviorData.LoadData(byteData);

        if (null != loadEndCallBack)
        {
            loadEndCallBack();
        }
    }

    private void LoadResourceData(string name)
    {
        name = System.IO.Path.GetFileNameWithoutExtension(name);
        TextAsset textAsset = Resources.Load<TextAsset>(name);
        byteData = textAsset.bytes;
    }

    IEnumerator LoadData(string directory, string name, int type)
    {
        string path = FileUtils.GetStreamingAssetsFilePath(name, directory);

        WWW www = new WWW(path);
        yield return www;
        if (type == 0)
        {
            textContent = www.text;
        }
        else
        {
            byteData = www.bytes;
        }
        yield return true;
    }
}
