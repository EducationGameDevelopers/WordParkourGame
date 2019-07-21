using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }

    //视图所需响应的事件列表
    [HideInInspector]
    public List<string> AttentionEventList = new List<string>();

    /// <summary>
    /// 视图响应处理事件
    /// </summary>
    public abstract void HandleEvent(string eventName, object data);

    /// <summary>
    /// 注册关心事件
    /// </summary>
    public virtual void RegisterAttentionEvent() { }

    /// <summary>
    /// 获取模型
    /// </summary>
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>();
    }

    /// <summary>
    /// 视图发送消息
    /// </summary>
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}

