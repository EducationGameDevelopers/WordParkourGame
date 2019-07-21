using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIStart : View
{
    private Text text_WordLexcionTitle;   //显示当前选择的词库

    private Text text_PlayerStatus;   //玩家地位显示
    private Text text_MiliaryRank;    //玩家军衔显示
    
    public override string Name 
    {
        get { return Consts.V_UIStart; }
    }

    private void Awake()
    {
        text_WordLexcionTitle = transform.Find("BtnSelectLexicon/Text").GetComponent<Text>();

        text_PlayerStatus = transform.Find("UIPlayerInfo/PlayerInfo/Status/Text").GetComponent<Text>();
        text_MiliaryRank = transform.Find("UIPlayerInfo/PlayerInfo/MiliaryRank/Text").GetComponent<Text>();
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("BgMusic") == 1)
        {
            Game.Instance.a_Sound.PlayBgMusic(Consts.MusicName);
        } 
        else
        {
            Game.Instance.a_Sound.StopBgMusic();
        }

        if(PlayerPrefs.GetInt("AudioEffect") == 1)
        {
            Game.Instance.a_Sound.PlayEffect();
        }
        else
        {
            Game.Instance.a_Sound.StopEffect();
        }
    }

    private void UpdatePlayerInfo()
    {
        text_PlayerStatus.text = Game.Instance.a_StaticData.CurrentPlayerStatusInfo.StrStatus;
        text_MiliaryRank.text = Game.Instance.a_StaticData.CurrentMiliaryRankInfo.StrRank;
    }


    /// <summary>
    /// 跳转到选择关卡界面
    /// </summary>
    public void GoToSelectScene()
    {
        Game.Instance.LoadScene(3);
    }

    public override void RegisterAttentionEvent()
    {
        AttentionEventList.Add(Consts.E_WordLexcionOption);
        AttentionEventList.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            //处理选择到的词库事件，UISelectLexcion脚本发出
            case Consts.E_WordLexcionOption:
                SetWordLexcion(StaticData.Instance.WordLexiconOption);
                break;

            case Consts.E_EnterScene:
                SceneArgs e = data as SceneArgs;
                if(e.SceneIndex==1)
                {
                    SetWordLexcion(StaticData.Instance.WordLexiconOption);
                    UpdatePlayerInfo();
                }
                break;
          
            default:
                break;
        }
    }

    /// <summary>
    /// 设置当前词库选择显示
    /// </summary>
    /// <param name="wordLexicon"></param>
    public void SetWordLexcion(WordLexicon wordLexicon)
    {
        switch (wordLexicon)
        {
            case WordLexicon.None:
                text_WordLexcionTitle.text = "请选择词库";
                break;

            case WordLexicon.Primary:
                text_WordLexcionTitle.text = "已选小学词库";
                break;

            case WordLexicon.CET4:
                text_WordLexcionTitle.text = "已选四级词库";
                break;

            case WordLexicon.CET6:
                text_WordLexcionTitle.text = "已选六级词库";
                break;

            case WordLexicon.Postgraduate:
                text_WordLexcionTitle.text = "已选考研词库";
                break;

            default:
                break;
        }
      
    }
}
