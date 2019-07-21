using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIWin:View
{
    #region 字段    
    private int tempReward;
    private int killEnemyCount;
    private float warDistance;
    private float rateRight;

    private int score_TempReward;
    private int score_KillEnemyCount;
    private float score_WarDistance;
    private float score_RateRight;

    private float totalScore;

    private int currentStatusEXP;
    private int finialStatusEXP;

    private int currentRankEXP;
    private int finialRankEXP;

    private bool isUpStatus = false;   //玩家地位是否升级
    private bool isUpRank = false;     //玩家军衔是否升级

    private PlayerStatus m_PlayerStatus;
    private MilitaryRank m_MilitaryRank;

    private Text text_TempReward;      //此次征战获得财富
    private Text text_KillEnemyCount;   //杀敌数
    private Text text_WarDistance;      //征战距离
    private Text text_RateRight;       //答题正确率

    private Text add_TempReward;
    private Text add_KillEnemyCount;
    private Text add_WarDistance;
    private Text add_RateRight;

    private Text text_TotalScore;    //获得的总分
    private Text text_Grade;         //获得的评级

    private Text text_CurrentStatus;
    private Text text_NextStatus;
    private Text text_CurrentRank;
    private Text text_NextRank;

    private Text text_StatusRateValue;
    private Text text_RankRateValue;

    private Image img_StatusProcessBar;
    private Image img_RankProcessBar;

    private Button btn_BackSelect;

    private StaticData staticData;
    private GameData gameData; 
    private UserAnswerModel uam;

    private Sequence dataSequence;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_UIWin; }
    }
    #endregion

    #region Unity回调
    void Awake()
    {
        text_TempReward = transform.Find("AnswerMark/TempReward/txtTempReward").GetComponent<Text>();
        text_KillEnemyCount = transform.Find("AnswerMark/KillENemyCount/txtEnemyKill").GetComponent<Text>();
        text_WarDistance = transform.Find("AnswerMark/WarDistance/txtWarDistance").GetComponent<Text>();
        text_RateRight = transform.Find("AnswerMark/RateRight/txtRateRight").GetComponent<Text>();

        add_TempReward = transform.Find("AnswerMark/TempReward/AddScore").GetComponent<Text>();
        add_KillEnemyCount = transform.Find("AnswerMark/KillENemyCount/AddScore").GetComponent<Text>();
        add_WarDistance = transform.Find("AnswerMark/WarDistance/AddScore").GetComponent<Text>();
        add_RateRight = transform.Find("AnswerMark/RateRight/AddScore").GetComponent<Text>();

        text_TotalScore = transform.Find("AnswerMark/TotalScore/Text").GetComponent<Text>();

        text_Grade = transform.Find("AnswerMark/Grade/Text").GetComponent<Text>();

        text_CurrentStatus = transform.Find("AnswerMark/SelfStatusProcess/CurrentStatus").GetComponent<Text>();
        text_NextStatus= transform.Find("AnswerMark/SelfStatusProcess/NextStatus").GetComponent<Text>();
        text_CurrentRank = transform.Find("AnswerMark/SelfMilitaryRankProcess/CurrentRank").GetComponent<Text>();
        text_NextRank = transform.Find("AnswerMark/SelfMilitaryRankProcess/NextRank").GetComponent<Text>();

        text_StatusRateValue = transform.Find("AnswerMark/SelfStatusProcess/CurValue").GetComponent<Text>();
        text_RankRateValue = transform.Find("AnswerMark/SelfMilitaryRankProcess/CurValue").GetComponent<Text>();

        img_StatusProcessBar = transform.Find("AnswerMark/SelfStatusProcess/ProcessBar/Image").GetComponent<Image>();
        img_RankProcessBar = transform.Find("AnswerMark/SelfMilitaryRankProcess/ProcessBar/Image").GetComponent<Image>();

        btn_BackSelect = transform.Find("AnswerMark/BtnBack").GetComponent<Button>();
    }

    private void Start()
    {
        btn_BackSelect.onClick.AddListener(OnBackSelectClick);
        text_Grade.rectTransform.localScale = Vector3.zero;
        Hide();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 展示最终的收获
    /// </summary>
    private void ShowUltimatePrize()
    {
        dataSequence = DOTween.Sequence();

        //text_TempReward.text = tempReward.ToString() + "J";
        add_TempReward.text = "+" + score_TempReward.ToString();
        //显示动画设计       
        dataSequence.Append(text_TempReward.DOText(tempReward.ToString() + "J", 1, true, ScrambleMode.Numerals));
        dataSequence.Append(add_TempReward.transform.DOLocalMoveX(420, 1));

        //text_KillEnemyCount.text = killEnemyCount.ToString();
        add_KillEnemyCount.text = "+" + score_KillEnemyCount.ToString();
        dataSequence.Append(text_KillEnemyCount.DOText(killEnemyCount.ToString(), 1, true, ScrambleMode.Numerals));
        dataSequence.Append(add_KillEnemyCount.transform.DOLocalMoveX(420, 1));

        //text_WarDistance.text = warDistance.ToString("0")+"m";
        add_WarDistance.text = "+" + score_WarDistance.ToString("0");
        dataSequence.Append(text_WarDistance.DOText(warDistance.ToString("0") + "m", 1, true, ScrambleMode.Numerals));
        dataSequence.Append(add_WarDistance.transform.DOLocalMoveX(420, 1));

        //text_RateRight.text = rateRight.ToString() + "%";
        add_RateRight.text = "×" + score_RateRight.ToString();
        dataSequence.Append(text_RateRight.DOText(rateRight.ToString() + "%", 1, true, ScrambleMode.Numerals));
        dataSequence.Append(add_RateRight.transform.DOLocalMoveX(420, 1));

        text_TotalScore.text = "+" + totalScore.ToString("0");
        dataSequence.Append(text_TotalScore.transform.DOLocalMoveX(240, 1));

        //评级显示动画
        dataSequence.Append(text_Grade.transform.DOScale(1, 0.8f));

        text_CurrentStatus.text = staticData.CurrentPlayerStatusInfo.StrStatus;
        text_NextStatus.text = staticData.NextPlayerStatusInfo.StrStatus;
        text_CurrentRank.text = staticData.CurrentMiliaryRankInfo.StrRank;
        text_NextRank.text = staticData.NextMiliaryRankInfo.StrRank;
        
        if (isUpStatus)
        {
            staticData.CurPlayerStatus = (PlayerStatus)((int)staticData.CurPlayerStatus + 1);
                        
            //进度条变化显示
            dataSequence.Append(img_StatusProcessBar.DOFillAmount(1, 0.5f).OnComplete(()=>{ img_StatusProcessBar.fillAmount = 0; }));
            
            dataSequence.Append(img_StatusProcessBar.DOFillAmount((float)currentStatusEXP / (float)staticData.CurrentPlayerStatusInfo.EXP, 0.5f));

            dataSequence.AppendCallback(() =>
            {
                text_CurrentStatus.text = staticData.CurrentPlayerStatusInfo.StrStatus;
                text_NextStatus.text = staticData.NextPlayerStatusInfo.StrStatus;
            });
            
            isUpStatus = false;
        }
        else
        {
            //进度条变化显示
            dataSequence.Append(img_StatusProcessBar.DOFillAmount((float)currentStatusEXP / (float)staticData.CurrentPlayerStatusInfo.EXP, 1));
        }

        if (isUpRank)
        {
            staticData.CurMilitaryRank = (MilitaryRank)((int)staticData.CurMilitaryRank + 1);

            dataSequence.Append(img_RankProcessBar.DOFillAmount(1, 0.5f).OnComplete(()=> { img_RankProcessBar.fillAmount = 0; }));
            dataSequence.Append(img_RankProcessBar.DOFillAmount((float)currentRankEXP / (float)staticData.CurrentMiliaryRankInfo.EXP, 0.5f));

            dataSequence.AppendCallback(() =>
            {
                text_CurrentRank.text = staticData.CurrentMiliaryRankInfo.StrRank;
                text_NextRank.text = staticData.NextMiliaryRankInfo.StrRank;
            });

            isUpRank = false;
        }
        else
        {
            dataSequence.Append(img_RankProcessBar.DOFillAmount((float)currentRankEXP / (float)staticData.CurrentMiliaryRankInfo.EXP, 1));
        }

        dataSequence.AppendCallback(() =>
        {
            text_StatusRateValue.text = currentStatusEXP + "/" + staticData.CurrentPlayerStatusInfo.EXP;
            text_RankRateValue.text = currentRankEXP + "/" + staticData.CurrentMiliaryRankInfo.EXP;
            
        });

        dataSequence.Append(btn_BackSelect.transform.DOLocalMoveY(-550, 0.8f).OnComplete(()=> { Time.timeScale = 0; }));        
    }

    /// <summary>
    /// 计算最终结果
    /// </summary>
    private void CalculateUltimatePrize()
    {
        staticData = Game.Instance.a_StaticData;
        gameData = Game.Instance.a_GameData;
        uam = GetModel<UserAnswerModel>();

        tempReward = gameData.TempTreasure;
        killEnemyCount = gameData.KillEnemyCount;
        warDistance = gameData.WarDistance;
        rateRight = uam.Rate_RightAnswer;

        score_TempReward = tempReward * 50;
        score_KillEnemyCount = killEnemyCount * 60;
        score_WarDistance = warDistance * 10;
        score_RateRight = (rateRight / 100) + 1;

        totalScore = (score_TempReward + score_WarDistance + score_KillEnemyCount) * score_RateRight;

        JudgeGrade((int)totalScore);   
        CaluclateEXPS();
    }

    /// <summary>
    /// 计算经验值
    /// </summary>
    private void CaluclateEXPS()
    {
        text_StatusRateValue.text = staticData.CurrentStatusEXP + "/" + staticData.CurrentPlayerStatusInfo.EXP;
        text_RankRateValue.text = staticData.CurrentRankEXP + "/" + staticData.CurrentMiliaryRankInfo.EXP;

        img_StatusProcessBar.fillAmount = (float)staticData.CurrentStatusEXP / (float)staticData.CurrentPlayerStatusInfo.EXP;
        img_RankProcessBar.fillAmount = (float)staticData.CurrentRankEXP / (float)staticData.CurrentMiliaryRankInfo.EXP;

        currentStatusEXP = staticData.CurrentStatusEXP;
        currentRankEXP = staticData.CurrentRankEXP;

        if (currentStatusEXP + (int)totalScore >= staticData.CurrentPlayerStatusInfo.EXP)
        {
            int curEXP = currentStatusEXP + (int)totalScore - staticData.CurrentPlayerStatusInfo.EXP;
            staticData.CurrentStatusEXP = curEXP;           
            currentStatusEXP = curEXP;
            isUpStatus = true;
        }
        else
        {
            currentStatusEXP += (int)totalScore;
            staticData.CurrentStatusEXP = currentStatusEXP;
        }

        if(currentRankEXP + (int)killEnemyCount >= staticData.CurrentMiliaryRankInfo.EXP)
        {
            int curEXP = currentRankEXP + (int)killEnemyCount - staticData.CurrentMiliaryRankInfo.EXP;
            staticData.CurrentRankEXP = curEXP;
            currentRankEXP = curEXP;
            isUpRank = true;
        }
        else
        {
            currentRankEXP += (int)killEnemyCount;
            staticData.CurrentRankEXP = currentRankEXP;
        }
    }

    /// <summary>
    /// 根据总分判断评级
    /// </summary>
    /// <param name="total"></param>
    private void JudgeGrade(int total)
    {
        if(total < 500)
        {
            text_Grade.text = "D";
        }
        else if (total >= 500 && total < 1500)
        {
            text_Grade.text = "C";
        }
        else if (total >= 1500 && total < 4500)
        {
            text_Grade.text = "B";
        }
        else if (total >= 4500 && total < 7500)
        {
            text_Grade.text = "A";
        }
        else if (total >= 7500 && total < 11500)
        {
            text_Grade.text = "S";
        }
        else if (total >= 11500 && total < 18500)
        {
            text_Grade.text = "SS";
        }
        else if (total >= 18500 && total < 25000)
        {
            text_Grade.text = "SSS";
        }
    }

    /// <summary>
    /// 显示该界面
    /// </summary>
    public void Show()
    {
        Time.timeScale = 1;
        CalculateUltimatePrize();
        gameObject.SetActive(true);
        ShowUltimatePrize();
        
    }

    /// <summary>
    /// 隐藏该界面
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 返回选择关卡按钮点击事件
    /// </summary>
    public void OnBackSelectClick()
    {
        Game.Instance.LoadScene(1);
    }
    #endregion



    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {

    }
    #endregion  
}

