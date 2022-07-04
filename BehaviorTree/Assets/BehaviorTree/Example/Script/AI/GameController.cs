using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ConfigLoad configLoad = new ConfigLoad();
        // 加载行为树配置文件回调
        ConfigLoad.loadEndCallBack = ConfigLoadEnd;
        //加载配置文件
        configLoad.Init();
    }

    // Update is called once per frame
    void Update()
    {
        // 驱动 Sprite 管理器
        SpriteManager.GetInstance().Update();
        // 驱动 bullet 管理器
        BulletManager.GetInstance().Update();
    }

    // 配置文件加载结束回调
    private void ConfigLoadEnd()
    {
        // 创建一个 Sprite
        Player sprite = new Player();
        sprite.Init(Vector3.zero);
        SpriteManager.GetInstance().AddSprite(sprite);
        // 创建一个 Npc
        CheckNpc();

        NumberSprite numberSprite = new NumberSprite();
        numberSprite.Init(Vector3.zero);
        SpriteManager.GetInstance().AddSprite(numberSprite);

        NumberTest.Instance.SetNumberSprite(numberSprite);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "CreateNpc"))
        {
            CheckNpc();
        }
    }

    // 创建Npc
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