using UnityEngine;

partial class PlayerPatrol : IMove
{
    private Vector3 _position;

    public PlayerPatrol()
    {

    }

    public void ResetPos()
    {
        float x = UnityEngine.Random.Range(-10, 10);
        float z = UnityEngine.Random.Range(-10, 10);
        _position = new Vector3(x, 0, z);
    }

    public void Move(ref float speed, ref Vector3 position, ref float distance)
    {
        speed = 1;
        position = _position;
        distance = 0.5f;
    }
}

