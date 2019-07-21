using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UICountDown : View
{
    #region 字段
    public Text timer;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UICountDown; }
    }
    #endregion

    #region 事件
    #endregion

    #region 方法
    /// <summary>
    /// 注册其关心的事件
    /// </summary>
    public override void RegisterAttentionEvent()
    {
        this.AttentionEventList.Add(Consts.E_EnterScene);

    }
    /// <summary>
    /// 倒计时动画
    /// </summary>
    private void ShowTime(int time)
    {
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;
        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
        //TODO 播放一次音乐
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
    /// 开始计时
    /// </summary>
    public void StartCountDown()
    {
        Show();
        StartCoroutine(DisPlayCountDown());
    }


    /// <summary>
    /// 显示计数
    /// </summary>
    IEnumerator DisPlayCountDown()
    {
        int count = 3;
        while (count > 0)
        {
            ShowTime(count);
            Game.Instance.a_Sound.PlayEffectMusic("Timer");
            count--;
            //等待1秒
            yield return new WaitForSeconds(1);

            if (count <= 0)
            {
                break;
            }
        }

        Hide();   //计数结束后隐藏该界面

        //发送继续游戏事件，由UIBoard接收处理
        SendEvent(Consts.E_ContinueGame);
        //开始生成地面物体
        SendEvent(Consts.E_SpawnEnvObjects);
    }
    #endregion

    #region Uinty回调
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            //进入场景事件，Game脚本发出
            case Consts.E_EnterScene:
                SceneArgs e = (SceneArgs)data;
                //确认该场景
                if (e.SceneIndex == 3)
                {
                    //开始倒计时
                    StartCountDown();
                }
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion

}

