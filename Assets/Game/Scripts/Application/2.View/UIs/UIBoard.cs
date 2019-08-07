using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class UIBoard : View
{
    private GameObject go_PausePanel;    //暂停面板

    private Image labourFill;
    private Text text_LabourValue;      //体力值数据显示
    private Text text_RunDistance;      //奔跑距离显示
    private Text text_KillEnemyCount;    //击杀敌人数显示
    private Text text_TempReward;        //征战奖励显示

    private float maxLabourValue;
    private float currentLabour;

    private float runDistance;
    private float timeSpeed;

    public override string Name
    {
        get { return Consts.V_UIBoard; }
    }


    void Awake()
    {
        runDistance = 0;
        go_PausePanel = transform.Find("Pauseinfo").gameObject;

        labourFill = transform.Find("PlayerHPBar/Fill").GetComponent<Image>();
        text_LabourValue = transform.Find("PlayerHPBar/ShowText/CurrentLabour").GetComponent<Text>();
        text_RunDistance = transform.Find("RunDistance/Text").GetComponent<Text>();
        text_KillEnemyCount = transform.Find("KillEnemyCount/Text").GetComponent<Text>();
        text_TempReward = transform.Find("TempReward/Text").GetComponent<Text>();
      
        maxLabourValue = Game.Instance.a_StaticData.MaxLabourValue;
        currentLabour = maxLabourValue;
        labourFill.fillAmount = 1;
        UpdateLabourProcessShow(maxLabourValue);

        HidePauseInfo();
    }

    IEnumerator DOUpdateLabour(float intervalTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalTime);
            //奔跑距离显示
            runDistance += intervalTime;           
            Game.Instance.a_GameData.WarDistance = runDistance;
            text_RunDistance.text = runDistance.ToString("0") + "m";
            //体力值显示
            currentLabour = currentLabour - 1;
            if (currentLabour <= 0)
            {
                SendEvent(Consts.E_EndLevel);
                yield return new WaitForSeconds(0);
            }
            UpdateLabourProcessShow(currentLabour);
        }        
    }
 
    private void AddCurrentLabourValue()
    {
        currentLabour += 1;
        text_LabourValue.text = currentLabour.ToString("0") + "/" + maxLabourValue.ToString("00");
        labourFill.fillAmount = currentLabour / maxLabourValue;
    }

    private void UpdateLabourProcessShow(float currentLabour)
    {
        text_LabourValue.text = currentLabour.ToString("0") + "/" + maxLabourValue.ToString("00");
        labourFill.fillAmount = currentLabour/ maxLabourValue;
    }

    private void UpdateKillEnemyCount(int count)
    {
        text_KillEnemyCount.text = count.ToString();
    }

    private void UpdateTempReward(int reward)
    {
        text_TempReward.text = reward.ToString();
    }

    #region override
    public override void RegisterAttentionEvent()
    {
        AttentionEventList.Add(Consts.E_ContinueGame);
        AttentionEventList.Add(Consts.E_PauseGame);
        AttentionEventList.Add(Consts.E_UpdateKillEnemyCount);
        AttentionEventList.Add(Consts.E_UpdateTempTreasure);
        AttentionEventList.Add(Consts.E_AddLabourValue);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_ContinueGame:
                StartCoroutine(DOUpdateLabour(1));
                ContinuePlay();          
                break;

            case Consts.E_PauseGame:
                PausePlay();
                break;

            case Consts.E_UpdateTempTreasure:
                UpdateTempReward(int.Parse(data.ToString()));
                break;

            case Consts.E_UpdateKillEnemyCount:
                UpdateKillEnemyCount(int.Parse(data.ToString()));
                break;

            case Consts.E_AddLabourValue:
                AddCurrentLabourValue();
                break;
        }
    }
    #endregion

    #region 按钮点击
    public void OnGamePauseClick()
    {
        PausePlay();       
    }

    public void OnConutinueGameClick()
    {
        ContinuePlay();
    }

    public void OnBackUserScene()
    {
        SendEvent(Consts.E_EndLevel);
    }
    #endregion

    #region 帮助方法
    private void ShowPauseInfo()
    {
        go_PausePanel.SetActive(true);
    }

    private void HidePauseInfo()
    {
        go_PausePanel.SetActive(false);
    }

    private void PausePlay()
    {
        Game.Instance.IsOnPlay = false;
        Time.timeScale = 0;
        ShowPauseInfo();
    }

    private void ContinuePlay()
    {
        Game.Instance.IsOnPlay = true;
        Time.timeScale = 1;
        HidePauseInfo();
    }
    #endregion
}

