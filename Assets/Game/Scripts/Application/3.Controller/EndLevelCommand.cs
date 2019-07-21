using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 结束关卡命令
/// </summary>
public class EndLevelCommand : Controller
{
    public override void Execute(object data)
    {
        
        //对象池回收所有对象
        Game.Instance.a_ObjectPool.UnspawnAll();

        //显示对应UI界面
        GetView<UIWin>().Show();
    }
}

