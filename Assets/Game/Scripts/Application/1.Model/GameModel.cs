
public class GameModel : Model
{

    #region 字段
    //当前游戏进度关卡
    private int m_PlayProgress = -1;

    //最大通关的游戏关卡
    private int m_MaxPassProgress = -1;

    //游戏分数(金币)
    private int m_Gold = 0;

    //是否正在游戏中
    private bool m_IsPlaying = false;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.M_GameModel; }
    }


    public int PlayProgress
    {
        get { return m_PlayProgress; }
    }

    public int MaxPassProgress
    {
        get { return m_MaxPassProgress; }
    }

    public int Gold
    {
        get { return m_Gold; }
        set
        {
            m_Gold = value;
        }
    }

    public bool IsPlaying
    {
        get { return m_IsPlaying; }
        set { m_IsPlaying = value; }
    }

    #endregion

    #region 方法
    /// <summary>
    /// 初始化
    /// </summary>
    public bool InitGame()
    {

        //读取游戏通关进度存档
        m_MaxPassProgress = Saver.GetGameProgress();
        //设置初始金币数
        Gold = 400;
        return true;
    }

    /// <summary>
    /// 开始关卡
    /// </summary>
    public void StartLevel(int levelIndex)
    {
        m_PlayProgress = levelIndex;

    }

    /// <summary>
    /// 关卡结束
    /// </summary>
    public void StopLevel(bool isWin)
    {
        //是否需要存储此时的游戏进度
        if (isWin == true && m_PlayProgress > m_MaxPassProgress)
        {
            //设置存档保存游戏进度
            Saver.SetGameProgress(m_PlayProgress);
            //设置此时的最大游戏通关进度
            m_MaxPassProgress = Saver.GetGameProgress();
        }
        m_IsPlaying = false;
    }

    /// <summary>
    /// 将参与该关卡的单词列表存入Json文件
    /// </summary>
    public void SaveWordListToJson()
    {
        Tools.SaveWordDicToJson(StaticData.m_WordDict);
    }

    /// <summary>
    /// 清空进度存档
    /// </summary>
    public void ClearProgress()
    {
        m_IsPlaying = false;
        m_MaxPassProgress = -1;
        m_PlayProgress = -1;
        //设置存档
        Saver.SetGameProgress(m_MaxPassProgress);
    }
    #endregion

}