using UnityEngine;

public interface IBTOwner
{
    int SpriteID
    {
        get;
    }

    GameObject SpriteGameObject
    {
        get;
    }

    void Update();
}
