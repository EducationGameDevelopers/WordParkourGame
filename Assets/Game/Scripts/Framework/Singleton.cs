using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 单例模板
/// </summary>
public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T m_instance = null;

    public static T Instance
    {
        get { return m_instance; }
    }

    protected virtual void Awake()
    {
        //单例赋值
        m_instance = this as T;
    }
}

