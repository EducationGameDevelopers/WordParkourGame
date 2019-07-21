using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 开始关卡命令
/// </summary>
public class StartLevelCommand : Controller
{
    public override void Execute(object data)
    {
        StartLevelArgs e = data as StartLevelArgs;

        //第一步
        GameModel gModel = GetModel<GameModel>();
        gModel.StartLevel(e.LevelIndex);
        gModel.Gold = 400;

        //进入游戏
        Game.Instance.LoadScene(3);
        
    }
}

