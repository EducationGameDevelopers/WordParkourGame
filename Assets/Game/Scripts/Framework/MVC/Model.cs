using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 模型层
/// </summary>
public abstract class Model
{
    public abstract string Name { get; }

    /// <summary>
    /// 模型处理响应事件
    /// </summary>
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}

