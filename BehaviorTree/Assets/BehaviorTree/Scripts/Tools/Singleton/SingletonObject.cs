using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// 泛型类单例
/// 继承自 SingletonObject 的单例类，声明一个 private 构造函数，放置在外部通过 new 方法再次实力化单利类
/// </summary>
/// <typeparam name="T">C#类</typeparam>
public abstract class SingletonObject<T> : System.Object where T : SingletonObject<T>{

    private static T _instance;

    private static readonly object syslock = new object();

    protected SingletonObject()
    {

    }

    public static T Instance
    {
        get {
            if (null == _instance)
            {
                CreateInstance();
            }
            return _instance;
        }
    }

    private static void CreateInstance() {
        lock (syslock) {
            if (null == _instance) {
                // 使用反射创建实例
                _instance = Activator.CreateInstance(typeof(T), nonPublic: true) as T;
            }
        }
    }

    public virtual void Destroy() {
        _instance = null;
    }

}