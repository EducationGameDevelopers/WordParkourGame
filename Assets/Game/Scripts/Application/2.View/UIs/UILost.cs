using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UILost:View
{

    #region 字段
    public Text txtCurrent;
    public Text txtTotal;
    public Text txtLevelIndex;

    public Button btnRestart;
    public Button btnBackMenu;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UILost; }
    }
    #endregion


    #region 方法
    /// <summary>
    /// 最终结果，显示该关卡怪物波数信息
    /// </summary>
    public void UpdateRoundInfo(int currentCount, int totalCount, int levelIndex)
    {
        txtCurrent.text = currentCount.ToString().PadLeft(2, '0');    //当前怪物的波数
        txtTotal.text = totalCount.ToString().PadLeft(2, '0');        //该关卡的怪物总波数
        txtLevelIndex.text = levelIndex.ToString().PadLeft(2, '0');   //当前关卡索引数
    }

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

    /// <summary>
    /// 重新开始该关卡按钮点击事件
    /// </summary>
    public void OnRestartClick()
    {
        GameModel gm = GetModel<GameModel>();
        //发送开始当前关卡事件
        SendEvent(Consts.E_StartLevel, new StartLevelArgs() { LevelIndex = gm.PlayProgress });
    }

    /// <summary>
    /// 返回主菜单按钮点击事件
    /// </summary>
    public void OnBackMenu()
    {
        //发出退出该场景事件，ExitSceneCommand脚本处理
        SendEvent(Consts.E_ExitScene, new SceneArgs() { SceneIndex = 3 });
        Game.Instance.LoadScene(1);
    }
    #endregion

    #region Unity回调
    void Awake()
    {
        UpdateRoundInfo(0, 0, 0);
    }
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {

    }
    #endregion


}

