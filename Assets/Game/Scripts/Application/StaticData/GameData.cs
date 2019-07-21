using UnityEngine;

/// <summary>
/// 出征过程中产生的数据
/// </summary>
public class GameData : Singleton<GameData>
{
    private int tempTreasure;  //此次出征的财富

    private int killEnemyCount;   //此次出征的杀敌数

    private float warDistance;     //此次出征的距离

    public int TempTreasure
    {
        get { return tempTreasure; }
    }

    public int KillEnemyCount
    {
        get { return killEnemyCount; }
    }

    public void AddKillEnemyCount()
    {
        killEnemyCount++;
        Game.Instance.GameSendEvent(Consts.E_UpdateKillEnemyCount, killEnemyCount);
    }

    public void AddTempTreasure()
    {
        tempTreasure++;
        Game.Instance.GameSendEvent(Consts.E_UpdateTempTreasure, tempTreasure);
    }

    public float WarDistance
    {
        get { return warDistance; }
        set { warDistance = value; }
    }

    public void InitAllData()
    {
        tempTreasure = 0;
        killEnemyCount = 0;
        warDistance = 0;
    }
}

