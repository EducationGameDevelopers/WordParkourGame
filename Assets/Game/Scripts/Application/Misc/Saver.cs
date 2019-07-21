using UnityEngine;

/// <summary>
/// 游戏存读类
/// </summary>
public static class Saver
{
    /// <summary>
    /// 读取此时的游戏关卡进度
    /// </summary>
    public static int GetGameProgress()
    {
        return PlayerPrefs.GetInt(Consts.S_GameProgress, -1);
    }

    /// <summary>
    /// 保存(设置)此时的游戏关卡进度
    /// </summary>
    /// <param name="levelIndex"></param>
    public static void SetGameProgress(int levelIndex)
    {
        PlayerPrefs.SetInt(Consts.S_GameProgress, levelIndex);
    }
}

