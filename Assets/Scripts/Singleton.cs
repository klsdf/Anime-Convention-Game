using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作者：闫辰祥
//创建时间: 2024年8月12日

/// <summary>
/// 单例模式基类
/// 提供通用的单例模式实现
/// </summary>
/// <typeparam name="T">继承MonoBehaviour的类型</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 单例实例
    /// </summary>
    private static T _instance;

    /// <summary>
    /// 线程锁对象
    /// </summary>
    private static readonly object _lock = new object();

    /// <summary>
    /// 应用程序是否正在退出的标志
    /// </summary>
    private static bool _applicationIsQuitting = false;

    /// <summary>
    /// 获取单例实例
    /// 如果实例不存在则创建新实例
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance of {typeof(T)} already destroyed. Returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError($"[Singleton] More than one instance of {typeof(T)} found!");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = $"{typeof(T).ToString()} (Singleton)";

                        DontDestroyOnLoad(singleton);

                        Debug.Log($"[Singleton] An instance of {typeof(T)} was created with DontDestroyOnLoad.");
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// 当对象被销毁时调用
    /// 设置退出标志以防止访问已销毁的实例
    /// </summary>
    protected virtual void OnDestroy()
    {
        _applicationIsQuitting = true;
    }
}