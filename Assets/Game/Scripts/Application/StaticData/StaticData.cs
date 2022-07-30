using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 静态数据类
/// </summary>
public class StaticData : Singleton<StaticData>
{
#if UNITY_EDITOR
    [MenuItem("DevelopText/InitPlayerInfo")]
    private static void DebugInit()
    {
        DebugInitPlayerInfo();
    }
#endif

    #region 字段
    //单词字典
    public static Dictionary<string, Word> m_WordDict = new Dictionary<string, Word>();
   
    private float playerRunSpeed;     //默认的玩家奔跑速度为2

    private int maxLabourValue = 100;       //玩家体力值上限

    private WordLexicon wordLexicon = WordLexicon.None;    //单词词库

    private WordLexicon wordLexiconOption = WordLexicon.None;  //单词词库选项

    private PlayerStatus m_PlayerStatus = PlayerStatus.None;   //玩家地位

    private MilitaryRank m_MilitaryRank = MilitaryRank.None;    //玩家军衔

    private int currentStatusEXP;    //当前玩家地位的经验值

    private int currentMiliaryRankEXP;   //当前玩家军衔经验值

    private Dictionary<PlayerStatus, PlayerStatusInfo> m_PlayerStatusEXPDict;   //各地位对应的经验值

    private Dictionary<MilitaryRank, MiliaryRankInfo> m_MilitaryRankEXPDict;   //各军衔对应的经验值
    #endregion

    #region 属性
    public WordLexicon WordLexiconOption
    {
        get { return wordLexiconOption; }
    }

    public float PlayerRunSpeed
    {
        get { return playerRunSpeed; }
        set
        {
            if (value >= 6)
            {
                value = 6;
            }
            playerRunSpeed = value;
        }
    }

    public int MaxLabourValue
    {
        get { return maxLabourValue; }
        set { maxLabourValue = value; }
    }

    public PlayerStatus CurPlayerStatus
    {
        get { return (PlayerStatus)PlayerPrefs.GetInt("PlayerStatus"); }
        set { PlayerPrefs.SetInt("PlayerStatus", (int)value); }
    }

    public MilitaryRank CurMilitaryRank
    {
        get { return (MilitaryRank)PlayerPrefs.GetInt("MilitaryRank"); }
        set { PlayerPrefs.SetInt("MilitaryRank", (int)value); }
    }

    public int CurrentStatusEXP
    {
        get { return PlayerPrefs.GetInt("CurrentStatusEXP"); }
        set { PlayerPrefs.SetInt("CurrentStatusEXP", value); }
    }

    public int CurrentRankEXP
    {
        get { return PlayerPrefs.GetInt("CurrentRankEXP"); }
        set { PlayerPrefs.SetInt("CurrentRankEXP", value); }
    }

    public PlayerStatusInfo CurrentPlayerStatusInfo
    {
        get { return GetPlayerStatusInfo(CurPlayerStatus); }
    }

    public MiliaryRankInfo CurrentMiliaryRankInfo
    {
        get { return GetMiliaryRankInfo(CurMilitaryRank); }
    }

    public PlayerStatusInfo NextPlayerStatusInfo
    {
        get
        {
            int nextStatusCode = CurPlayerStatus.GetHashCode() + 1;
            if (nextStatusCode <= Enum.GetNames(CurPlayerStatus.GetType()).Length)
            {
                PlayerStatusInfo nextInfo = GetPlayerStatusInfo((PlayerStatus)nextStatusCode);
                return nextInfo;
            }
            else
            {
                return GetPlayerStatusInfo(CurPlayerStatus);
            }
        }
    }

    public MiliaryRankInfo NextMiliaryRankInfo
    {
        get
        {
            int nextRankCode = CurMilitaryRank.GetHashCode() + 1;
            if (nextRankCode <= Enum.GetNames(CurPlayerStatus.GetType()).Length)
            {
                MiliaryRankInfo nextInfo = GetMiliaryRankInfo((MilitaryRank)nextRankCode);
                return nextInfo;
            }
            else
            {
                return GetMiliaryRankInfo(CurMilitaryRank);
            }
            
        }
    }

    public PlayerStatusInfo GetPlayerStatusInfo(PlayerStatus playerStatus)
    {
        return m_PlayerStatusEXPDict[playerStatus];
    }


    public MiliaryRankInfo GetMiliaryRankInfo(MilitaryRank militaryRank)
    {
        return m_MilitaryRankEXPDict[militaryRank];
    }

    public void SetNextPlayerStatus()
    {
        int nextStatusCode = CurPlayerStatus.GetHashCode() + 1;
        if (nextStatusCode <= Enum.GetNames(CurPlayerStatus.GetType()).Length)
        {
            m_PlayerStatus = (PlayerStatus)nextStatusCode;
        }
    }
   
    public void SetNextMiliaryRank()
    {
        int nextRankCode = CurMilitaryRank.GetHashCode() + 1;
        if (nextRankCode <= Enum.GetNames(CurPlayerStatus.GetType()).Length)
        {
            m_MilitaryRank = (MilitaryRank)nextRankCode;
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        InitPersistentWordFiles();
        InitAllPlayerInfo();
        InitPlayerSpeed();
    }

    #region 数据初始化
    public void InitPlayerSpeed()
    {
        PlayerRunSpeed = 2;
    }

    /// <summary>
    /// 初始化单词字典
    /// </summary>
    void InitWordDict(string wordDir)
    {
        Tools.FillWords(ref m_WordDict, wordDir);
    }

    /// <summary>
    /// 初始化Persistent路径下的单词Json文件
    /// </summary>
    void InitPersistentWordFiles()
    {
        Tools.SaveWordInfoToPersistent(Consts.WordInitDir + Consts.WordJson_PrimaryDir, Consts.SaveDir + Consts.WordJson_PrimaryDir);
        Tools.SaveWordInfoToPersistent(Consts.WordInitDir + Consts.WordJson_CET4Dir, Consts.SaveDir + Consts.WordJson_CET4Dir);
        Tools.SaveWordInfoToPersistent(Consts.WordInitDir + Consts.WordJson_CET6Dir, Consts.SaveDir + Consts.WordJson_CET6Dir);
        Tools.SaveWordInfoToPersistent(Consts.WordInitDir + Consts.WordJson_PostgraduateDir, Consts.SaveDir + Consts.WordJson_PostgraduateDir);
    }

    void InitAllPlayerInfo()
    {
        if (m_PlayerStatus == PlayerStatus.None&& m_MilitaryRank == MilitaryRank.None)
        {
            PlayerPrefs.SetInt("PlayerStatus", (int)PlayerStatus.Civilian);
            PlayerPrefs.SetInt("MilitaryRank", (int)MilitaryRank.Private);
        }

        if (currentStatusEXP == -1 && currentMiliaryRankEXP == -1)
        {
            PlayerPrefs.SetInt("CurrentStatusEXP", 0);
            PlayerPrefs.SetInt("CurrentRankEXP", 0);
        }

        InitPlayerStatusEXPDict();
        InitMiliaryRankEXPDict();
    }

    void InitPlayerStatusEXPDict()
    {
        m_PlayerStatusEXPDict = new Dictionary<PlayerStatus, PlayerStatusInfo>();

        m_PlayerStatusEXPDict.Add(PlayerStatus.Civilian, new PlayerStatusInfo("平民", 2000));     //平民
        m_PlayerStatusEXPDict.Add(PlayerStatus.Knight, new PlayerStatusInfo("骑士", 5000));       //骑士
        m_PlayerStatusEXPDict.Add(PlayerStatus.Baron, new PlayerStatusInfo("男爵", 8000));       //男爵
        m_PlayerStatusEXPDict.Add(PlayerStatus.Viscount, new PlayerStatusInfo("子爵", 15000));    //子爵
        m_PlayerStatusEXPDict.Add(PlayerStatus.Earl, new PlayerStatusInfo("伯爵", 21000));        //伯爵
        m_PlayerStatusEXPDict.Add(PlayerStatus.Marquie, new PlayerStatusInfo("侯爵", 32000));    //侯爵
        m_PlayerStatusEXPDict.Add(PlayerStatus.Duke, new PlayerStatusInfo("公爵", 58000));       //公爵
        m_PlayerStatusEXPDict.Add(PlayerStatus.Prince, new PlayerStatusInfo("亲王", 90000));     //亲王
        m_PlayerStatusEXPDict.Add(PlayerStatus.King, new PlayerStatusInfo("国王", 200000));       //国王
        m_PlayerStatusEXPDict.Add(PlayerStatus.Emperor, new PlayerStatusInfo("皇帝", 500000));    //皇帝
    }

    void InitMiliaryRankEXPDict()
    {
        m_MilitaryRankEXPDict = new Dictionary<MilitaryRank, MiliaryRankInfo>();

        m_MilitaryRankEXPDict.Add(MilitaryRank.Private, new MiliaryRankInfo("列兵", 20));           //列兵
        m_MilitaryRankEXPDict.Add(MilitaryRank.Corporal, new MiliaryRankInfo("下士", 45));          //下士
        m_MilitaryRankEXPDict.Add(MilitaryRank.Sergeant, new MiliaryRankInfo("中士", 110));          //中士
        m_MilitaryRankEXPDict.Add(MilitaryRank.SeniorSergeant, new MiliaryRankInfo("上士", 200));    //上士
        m_MilitaryRankEXPDict.Add(MilitaryRank.SecondLieutenant, new MiliaryRankInfo("少尉", 320));  //少尉
        m_MilitaryRankEXPDict.Add(MilitaryRank.Lieutenant, new MiliaryRankInfo("中尉", 480));        //中尉
        m_MilitaryRankEXPDict.Add(MilitaryRank.Captain, new MiliaryRankInfo("上尉", 660));           //上尉
        m_MilitaryRankEXPDict.Add(MilitaryRank.Major, new MiliaryRankInfo("少校", 980));             //少校
        m_MilitaryRankEXPDict.Add(MilitaryRank.LieutenantColonel, new MiliaryRankInfo("中校", 1300)); //中校
        m_MilitaryRankEXPDict.Add(MilitaryRank.Colonel, new MiliaryRankInfo("上校", 1800));           //上校
        m_MilitaryRankEXPDict.Add(MilitaryRank.MajorGeneral, new MiliaryRankInfo("少将", 2900));      //少将
        m_MilitaryRankEXPDict.Add(MilitaryRank.LieutenantGeneral, new MiliaryRankInfo("中将", 4800)); //中将
        m_MilitaryRankEXPDict.Add(MilitaryRank.General, new MiliaryRankInfo("上将", 8500));           //上将
        m_MilitaryRankEXPDict.Add(MilitaryRank.Marshal, new MiliaryRankInfo("元帅", 15000));           //元帅
        m_MilitaryRankEXPDict.Add(MilitaryRank.ArmyOfGod, new MiliaryRankInfo("军神", 30000));         //军神
   
    }

    private static void DebugInitPlayerInfo()
    {
        PlayerPrefs.SetInt("PlayerStatus", (int)PlayerStatus.Civilian);
        PlayerPrefs.SetInt("MilitaryRank", (int)MilitaryRank.Private);
        PlayerPrefs.SetInt("CurrentStatusEXP", 0);
        PlayerPrefs.SetInt("CurrentRankEXP", 0);
        Debug.Log("初始化一次");
    }
    #endregion

    #region 词库操作
    /// <summary>
    /// 选择单词词库
    /// </summary>
    public void SelectWordLexcion(WordLexicon wordLexicon)
    {
        this.wordLexicon = wordLexicon;

        //选择词库前先情况单词字典
        ClearWordDict();
        //选择JSON词库
        switch (this.wordLexicon)
        {
            case WordLexicon.None:
                break;

            case WordLexicon.Primary:
                InitWordDict(Consts.SaveDir + Consts.WordJson_PrimaryDir);          
                break;

            case WordLexicon.CET4:
                InitWordDict(Consts.SaveDir + Consts.WordJson_CET4Dir);
                break;

            case WordLexicon.CET6:
                InitWordDict(Consts.SaveDir + Consts.WordJson_CET6Dir);
                break;

            case WordLexicon.Postgraduate:
                InitWordDict(Consts.SaveDir + Consts.WordJson_PostgraduateDir);
                break;

            default:
                break;
        }

        //本地化存储词库选项
        PlayerPrefs.SetString("WordLexcionPotion", wordLexicon.ToString());
        //本地取出该选项记录
        wordLexiconOption = (WordLexicon)Enum.Parse(typeof(WordLexicon), PlayerPrefs.GetString("WordLexcionPotion"));
    }

    /// <summary>
    /// 获取当前所选词库的所有单词Id集合
    /// </summary>
    public List<string> GetWordIds()
    {
        List<string> wordIds = new List<string>();
        foreach (string wordId in m_WordDict.Keys)
        {
            wordIds.Add(wordId);
        }
        return wordIds;
    }

    /// <summary>
    /// 清空单词字典
    /// </summary>
    public void ClearWordDict()
    {
        m_WordDict.Clear();
    }

    /// <summary>
    /// 根据单词ID获取单词
    /// </summary>
    public Word GetWord(string wordId)
    {
        return m_WordDict[wordId];
    }

    /// <summary>
    /// 修改单词的正确或错误标记数
    /// </summary>
    public static void ModityWordMarkCount(string wordId, bool isRight)
    {
        if (isRight == true)
        {
            m_WordDict[wordId].RightMarkCount += 1;
        }
        else
        {
            m_WordDict[wordId].WrongMarkCount += 1;
        }
    }
    #endregion
}

