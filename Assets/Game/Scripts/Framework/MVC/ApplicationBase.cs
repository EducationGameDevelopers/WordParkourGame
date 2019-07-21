using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 应用层基类，完善MVC框架
/// </summary>
public abstract class ApplicationBase<T> : Singleton<T>
    where T : MonoBehaviour
{
    /// <summary>
    /// 注册控制器
    /// </summary>
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

}

