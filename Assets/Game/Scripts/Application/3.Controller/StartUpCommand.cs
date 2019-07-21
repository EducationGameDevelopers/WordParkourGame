using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型
        RegisterModel(new GameModel());
        RegisterModel(new UserAnswerModel());

        //注册命令
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));

        //初始化模型
        GameModel gameModel = GetModel<GameModel>();
        bool isInit = gameModel.InitGame();

        if (isInit == true)
            //跳转到登录场景
            Game.Instance.LoadScene(1);
    }
}

