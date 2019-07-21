using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UISystem:View
{
    #region 常量
    #endregion

    #region 字段
    public Button btnContinue;
    public Button btnRestart;
    public Button btnSelect;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UISystem; }
    }
    #endregion


    #region 方法
    /// <summary>
    /// 隐藏该界面
    /// </summary>
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示该界面
    /// </summary>
    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void OnContinueClick()
    {
        Hide();
        //继续游戏事件发出，UIBoard脚本接收并处理
        SendEvent(Consts.E_ContinueGame);
    }


    public void OnRestartClick()
    {
        GameModel gm = GetModel<GameModel>();

        //发送退出当前关卡事件，ExitSceneCommand脚本处理
        SendEvent(Consts.E_ExitScene, new SceneArgs() { SceneIndex = 3 });
        //发送开始当前关卡事件，StartLevelCommand脚本处理
        SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gm.PlayProgress });   
    }

    /// <summary>
    /// 返回选择关卡界面按钮点击
    /// </summary>
    public void OnSelectClick()
    {
        SendEvent(Consts.E_ExitScene, new SceneArgs() { SceneIndex = 3 });
        Game.Instance.LoadScene(2);
    }
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {

    }
    #endregion
}

