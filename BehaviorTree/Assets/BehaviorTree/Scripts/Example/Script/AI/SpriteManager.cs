using System.Collections.Generic;

public class SpriteManager
{
    // singleton
    public static SpriteManager Instance;
    private static object obj = new object();
    private static int SpriteId = 0;

    // Save all Sprite
    private List<BaseSprite> _spriteList = new List<BaseSprite>();
    // AI Manager
    private SpriteBTUpdateManager _spriteBTUpdateManager;

    public static SpriteManager GetInstance()
    {
        if (null == Instance)
        {
            lock(obj)
            {
                Instance = new SpriteManager();
            }
        }
        return Instance;
    }

    public SpriteManager()
    {
        _spriteBTUpdateManager = new SpriteBTUpdateManager();
    }

    // add Sprite
    public void AddSprite(BaseSprite sprite)
    {
        sprite.SpriteID = SpriteId++;
        _spriteList.Add(sprite);
        // Add Sprite to the AI manager
        _spriteBTUpdateManager.AddSprite(sprite.SpriteID, sprite as IBTNeedUpdate);
    }

    public void RemoveSprite(int spriteId)
    {
        BaseSprite sprite = _spriteList.Find((bs) =>
        {
            return bs.SpriteID == spriteId;
        });
        if (null == sprite)
        {
            return;
        }
        _spriteList.RemoveAll((bs)=> {
            return bs.SpriteID == spriteId;
        });
        _spriteBTUpdateManager.RemoveSprite(sprite.SpriteID);
    }

    public void Update()
    {
        for (int i = _spriteList.Count - 1; i >= 0; --i)
        {
            _spriteList[i].Update();
        }
        // Execute AI manager
        _spriteBTUpdateManager.Update();
    }

    public void Release()
    {
        for (int i = _spriteList.Count - 1; i >= 0; --i)
        {
            _spriteList[i].Release();
        }
        _spriteList.Clear();
    }
}
