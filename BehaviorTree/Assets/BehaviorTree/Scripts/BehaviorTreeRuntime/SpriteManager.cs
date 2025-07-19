using System.Collections.Generic;

public class SpriteManager
{
    // singleton
    public static SpriteManager Instance;
    private static object obj = new object();
    private static int SpriteId = 0;

    // Save all Sprite
    private List<BaseSprite> _spriteList = new List<BaseSprite>();

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
    }

    // add Sprite
    public void AddSprite(BaseSprite sprite)
    {
        sprite.SpriteId = SpriteId++;
        _spriteList.Add(sprite);
    }

    public void RemoveSprite(int spriteId)
    {
        BaseSprite sprite = _spriteList.Find((bs) =>
        {
            return bs.SpriteId == spriteId;
        });
        if (null == sprite)
        {
            return;
        }
        _spriteList.RemoveAll((bs)=> {
            return bs.SpriteId == spriteId;
        });
    }

    public void Update()
    {
        for (int i = _spriteList.Count - 1; i >= 0; --i)
        {
            _spriteList[i].Update();
        }
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
