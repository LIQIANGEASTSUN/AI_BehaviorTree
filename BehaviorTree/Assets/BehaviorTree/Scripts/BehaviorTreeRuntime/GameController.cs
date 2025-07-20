using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using BehaviorTree;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private List<string> fileList = new List<string>() {
        "EneryEnougthSubTree",
        "Player",
    };

    void Start()
    {
        Instance = this;
        BehaviorRegisterNode.Instance.RegisterNode();
        Init();
    }

    void Update()
    {
        SpriteManager.GetInstance().Update();
        BulletManager.GetInstance().Update();
    }

    private void Init()
    {
        LoadData();
        // create Sprite
        Player sprite = new Player();
        sprite.Init(Vector3.zero);
        SpriteManager.GetInstance().AddSprite(sprite);
        // create Npc
        CheckNpc();
    }

    private void LoadData()
    {
        foreach(var fileName in fileList)
        {
            string filePath = $"Assets/SubAssets/GameData/BehaviorTree/{fileName}.bytes";
            TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            BehaviorTreeData data = LitJson.JsonMapper.ToObject<BehaviorTreeData>(asset.text);
            BehaviorData.AddData(data);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "CreateNpc"))
        {
            CheckNpc();
        }
    }

    // Create Npc
    private void CheckNpc()
    {
        GameObject npcGo = GameObject.Find("Npc");
        if (!npcGo)
        {
            GameObject npc = Resources.Load<GameObject>("Npc");
            npcGo = GameObject.Instantiate<GameObject>(npc);
            npcGo.name = "Npc";

            Vector3 randomPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            npcGo.transform.position = new Vector3(-10, 0, 2) + randomPos;
        }
    }
}