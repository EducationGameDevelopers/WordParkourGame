using UnityEngine;

public static class Consts
{  
    public static string LevelDir = Application.dataPath + @"/Game/Resources/Levels";  //关卡配置文件路径

    public static readonly string MapDir = Application.dataPath + @"/Game/Resources/Maps";      //地图资源文件路径
    public static readonly string CardDir = Application.dataPath + @"/Game/Resources/Cards";    //选择关卡图片文件路径

    public static readonly string SaveDir = Application.persistentDataPath + "/";   //单词的Json信息存取路径
    public static string WordInitDir = "Words/";

    public static string WordJson_PrimaryDir = "word_primary.json";   //小学单词Json路径
    public static string WordJson_CET4Dir = "CET4.json";   //四级单词Json路径
    public static string WordJson_CET6Dir = "CET6.json";   //六级单词Json路径
    public static string WordJson_PostgraduateDir = "Postgraduate.json";   //考研单词Json路径

    public static string UserData = "user.json";

    //数值
    public const float DotClosedDistance = 0.1f;   //物体间相遇距离
    public const float RangeClosedDistance = 0.7f;

    //存档
    public const string S_GameProgress = "S_GameProgress";  //游戏进度存档

    //Model
    public const string M_GameModel = "M_GameModel";    //游戏数据模型
    public const string M_UserAnswerModel = "M_UserAnswerModel";  //用户数据信息模型

    //View
    public const string V_UIStart = "V_UIStart";
    public const string V_UISelect = "V_UISelect";
    public const string V_UIBoard = "V_UIBoard";
    public const string V_UIAnswer = "V_UIAnswer";
    public const string V_UICountDown = "V_UICountDown";
    public const string V_UIWin = "V_UIWin";
    public const string V_UILost = "V_UILost";
    public const string V_UISystem = "V_UISystem";
    public const string V_UIComplete = "V_UIComplete";
    public const string V_UISelectLexcion = "UISelectLexcion";

    public const string V_Spawner = "V_Spawner";  //生产者
    //事件
    public const string E_StartUp = "StartUp";
    public const string E_EnterScene = "EnterScene";  //进入场景事件 SceneArgs
    public const string E_ExitScene = "ExitScene";    //退出场景事件 SceneArgs

    public const string E_StartLevel = "E_StartLevel";  //开始关卡事件 StartArgs
    public const string E_EndLevel = "E_EndLevel";    //结束关卡事件 EndArgs
    public const string E_PauseGame = "E_PauseGame";    //暂停游戏
    public const string E_ContinueGame = "E_ContinueGame";    //继续游戏
    public const string E_EndCountDown = "E_EndCountDown";    //倒计时结束事件

    public const string E_CallQuestionPanel = "E_CallQuestionPanel";    //呼出答题界面

    public const string E_GoldEffect = "E_GoldEffect";    //金币特效显示事件

    public const string E_WordLexcionOption = "E_WordLexcionOption";  //词库选择事件

    public const string E_AnswerMark = "E_AnswerMark";   //用户答题评价事件

    public const string E_SceneLoadProcess = "E_SceneLoadProcess";    //场景加载进度事件

    public const string E_SpawnEnvObjects = "E_SpawnLandObjects";  //地面元素生成
    public const string E_StopSpawnObjects = "E_StopSpawnObjects";  //停止元素生成


    public const string E_UpdateKillEnemyCount = "E_UpdateKillEnemyCount";  //更新击杀敌人数显示
    public const string E_UpdateTempTreasure = "E_UpdateTempTreasure";  //更新征战财富奖励

    public const string E_AddLabourValue = "E_AddLabourValue";     //体力值增加事件

    public const string E_UpdateStatusAndRank = "E_UpdateStatusAndRank";     //玩家地位变更事件
    //音乐
    public const string MusicName = "crybaby";
}


/// <summary>
/// 单词词库
/// </summary>
public enum WordLexicon
{
    None,
    Primary,    //小学词库
    CET4,       //四级词库
    CET6,       //六级词库
    Postgraduate,     //考研词库
}

/// <summary>
/// 角色的爵位
/// </summary>
public enum PlayerStatus
{
    None,          
    Civilian,      //平民
    Knight,        //骑士
    Baron,         //男爵
    Viscount,      //子爵
    Earl,          //伯爵
    Marquie,       //侯爵
    Duke,          //公爵
    Prince,        //亲王 
    King,          //国王
    Emperor        //皇帝
}

/// <summary>
/// 玩家获得的军衔
/// </summary>
public enum MilitaryRank
{
    None,
    Private,          //列兵
    Corporal,         //下士
    Sergeant,        //中士
    SeniorSergeant,   //上士
    SecondLieutenant,          //少尉
    Lieutenant,      //中尉
    Captain,         //上尉
    Major,           //少校
    LieutenantColonel,   //中校
    Colonel,         //上校
    MajorGeneral,    //少将
    LieutenantGeneral,   //中将
    General,         //上将
    Marshal,         //元帅
    ArmyOfGod        //军神
}

public struct PlayerStatusInfo
{
    private string m_StrStatus;
    private int m_EXP;

    public PlayerStatusInfo(string strStatus,int exp)
    {
        m_StrStatus = strStatus;
        m_EXP = exp;
    }

    public string StrStatus
    {
        get { return m_StrStatus; }
    }

    public int EXP
    {
        get { return m_EXP; }
    }
}

public struct MiliaryRankInfo
{
    private string m_StrRank;
    private int m_EXP;

    public MiliaryRankInfo(string strRank, int exp)
    {
        m_StrRank = strRank;
        m_EXP = exp;
    }

    public string StrRank
    {
        get { return m_StrRank; }
    }

    public int EXP
    {
        get { return m_EXP; }
    }
}

