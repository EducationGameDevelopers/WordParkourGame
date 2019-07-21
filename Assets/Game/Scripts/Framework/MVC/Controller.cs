using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    /// <summary>
    /// 获取模型
    /// </summary>
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>();
    }

    /// <summary>
    /// 获取视图
    /// </summary>
    protected T GetView<T>()
        where T : View
    {
        return MVC.GetView<T>();
    }

    /// <summary>
    /// 注册模型
    /// </summary>
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }

    /// <summary>
    /// 注册控制器
    /// </summary>
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    /// <summary>
    /// 控制器响应处理事件
    /// </summary>
    public abstract void Execute(object data);
}

