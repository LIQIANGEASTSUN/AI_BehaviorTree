using UnityEngine;

public interface ISprite
{
    int SpriteID
    {
        get;
    }

    //long SpriteType
    //{
    //    get;
    //}

    GameObject SpriteGameObject
    {
        get;
    }

    void Update();

    //void Destroy(bool isExitLevel);

    //void OnPause(bool isPause);
}
