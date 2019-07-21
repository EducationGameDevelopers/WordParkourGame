using System;
using System.Collections.Generic;
using System.Text;

public static class MVC
{
    //存储映射
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();     //名字--模型
    public static Dictionary<string, View> Views = new Dictionary<string, View>();        //名字--视图
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();   //事件名称--控制器类型

    //注册各模块映射
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        //防止重复注册视图
        if (Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }
        //视图注册其需关心的事件
        view.RegisterAttentionEvent();

        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName, Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    /// <summary>
    /// 根据类型获取模型
    /// </summary>
    public static T GetModel<T>()
        where T : Model
    {
        foreach (Model m in Models.Values)
        {
            if (m is T)
            {
                return (T)m;
            }
        }

        return null;
    }

    /// <summary>
    /// 根据类型获取视图
    /// </summary>
    public static T GetView<T>()
        where T : View
    {
        foreach (View v in Views.Values)
        {
            if (v is T)
            {
                return (T)v;
            }
        }

        return null;
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    public static void SendEvent(string eventName, object data = null)
    {
        //当命令映射中拥有该事件时
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            //为该类型创建一个控制器
            Controller ctr = Activator.CreateInstance(t) as Controller;
            //该控制器响应事件
            ctr.Execute(data);
        }

        //视图响应事件
        foreach (View v in Views.Values)
        {
            //当视图响应事件列表中包含该事件时
            if (v.AttentionEventList.Contains(eventName))
            {
                //视图响应处理该事件
                v.HandleEvent(eventName, data);
            }
        }
    }
}

