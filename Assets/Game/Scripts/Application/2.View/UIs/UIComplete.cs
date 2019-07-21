using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIComplete:View
{


    #region 字段   
    public Button btnSelect;
    public Button btnClear;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UIComplete; }
    }
    #endregion


    #region 方法
    /// <summary>
    /// 选择关卡按钮点击
    /// </summary>
    public void OnSelectClick()
    {
        Game.Instance.LoadScene(1);
    }

    /// <summary>
    /// 清空存档按钮点击
    /// </summary>
    public void OnClearClick()
    {
        GameModel gm = GetModel<GameModel>();
        gm.ClearProgress();
    }
    #endregion


    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {
        
    }
    #endregion

    
}

