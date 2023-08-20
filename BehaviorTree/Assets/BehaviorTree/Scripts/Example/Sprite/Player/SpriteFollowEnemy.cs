using UnityEngine;

public class SpriteFollowEnemy : IMove
{
    private Player _player;
    public SpriteFollowEnemy(BaseSprite sprite)
    {
        _player = sprite as Player;
    }

    public void Move(ref float speed, ref Vector3 position, ref float distance)
    {
        speed = 3;
        position = Vector3.zero;
        if (null != _player.Enemy)
        {
            position = _player.Enemy.Position();
        }
        distance = 5f;
    }
}