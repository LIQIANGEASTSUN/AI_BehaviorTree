using System.Collections.Generic;

/// <summary>
/// Sprite 管理器
/// </summary>
public class SpriteManager
{
    // 单例
    public static SpriteManager Instance;
    private static object obj = new object();
    private static int SpriteId = 0;

    // 保存所有的 Sprite
    private List<BaseSprite> _spriteList = new List<BaseSprite>();
    // AI 管理器
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

    // 添加 Sprite
    public void AddSprite(BaseSprite sprite)
    {
        sprite.SpriteID = SpriteId++;
        _spriteList.Add(sprite);
        // 将 Sprite 添加到 AI管理器
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
        // 驱动AI管理器
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
