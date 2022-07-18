using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ConfigLoad configLoad = new ConfigLoad();
        // Load the behavior tree configuration file callback
        ConfigLoad.loadEndCallBack = ConfigLoadEnd;
        // Loading a Configuration File
        configLoad.Init();
    }

    // Update is called once per frame
    void Update()
    {

        SpriteManager.GetInstance().Update();

        BulletManager.GetInstance().Update();
    }

    // Callback at the end of configuration file loading
    private void ConfigLoadEnd()
    {
        // create Sprite
        Player sprite = new Player();
        sprite.Init(Vector3.zero);
        SpriteManager.GetInstance().AddSprite(sprite);
        // create Npc
        CheckNpc();

        NumberSprite numberSprite = new NumberSprite();
        numberSprite.Init(Vector3.zero);
        SpriteManager.GetInstance().AddSprite(numberSprite);
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