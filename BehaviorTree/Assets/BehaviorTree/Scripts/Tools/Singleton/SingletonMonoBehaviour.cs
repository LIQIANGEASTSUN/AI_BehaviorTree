using UnityEngine;

/// <summary>
/// �̳��� MonoBehaviour �ĵ���
/// �����ȡ�����򴴽�һ�� GameObject ���ؽű� T
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static readonly object syslock = new object();

    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                CreateInstance();
            }
            return _instance;
        }
    }

    private static void CreateInstance()
    {
        lock (syslock)
        {
            if (null == _instance)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (null == _instance)
                {
                    GameObject singleton = new GameObject("(singleton)" + typeof(T).ToString());
                    _instance = singleton.AddComponent<T>();
                    DontDestroyOnLoad(singleton);
                }
            }
        }
    }

    public virtual void Destroy()
    {
        _instance = null;
        GameObject.Destroy(gameObject);
    }
}
