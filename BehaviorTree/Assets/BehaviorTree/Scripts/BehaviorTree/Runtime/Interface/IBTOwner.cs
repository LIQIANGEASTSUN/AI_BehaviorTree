using UnityEngine;

public interface IBTOwner
{
    int SpriteId
    {
        get;
    }

    GameObject SpriteGameObject
    {
        get;
    }

    void Update();
}
