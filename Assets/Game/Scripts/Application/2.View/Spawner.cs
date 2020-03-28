using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Spawner : View
{
    //各种背景物品生成的位置
    private Vector2 landOriginPos = new Vector2(0, 0);    //地面物体初始生成位置
    private Vector2 skyOriginPos = new Vector2(0, 3);    //天空物体初始生成位置
    private Vector2 eventOriginPos = new Vector2(0, 1f);   //事件问题初始生成位置

    public override string Name
    {
        get { return Consts.V_Spawner; }
    }

    #region StartSpwan
    /// <summary>
    /// 开启地面物体生成协程
    /// </summary>
    private void StartSpawnLandCoroutine()
    {
        StartCoroutine(DelaySpawnObject(Random.Range(1, 3), SpawnTreeObject));
        StartCoroutine(DelaySpawnObject(Random.Range(2, 5), SpawnHillObject));
    }

    private void StartSpawnSkyCoroutine()
    {
        StartCoroutine(DelaySpawnObject(Random.Range(2, 5), SpawnCloudObjects));
        StartCoroutine(DelaySpawnObject(Random.Range(4, 8), SpawnFlyObjects));
    }

     private void StartSpawnEventObjCoroutine()
    {
        StartCoroutine(DelaySpawnObject(Random.Range(2, 5),SpawnGold));
        StartCoroutine(DelaySpawnObject(Random.Range(2, 6), SpawnEnemyObjects));
    }
    #endregion

    /// <summary>
    /// 延迟生成环境物体
    /// </summary>
    private IEnumerator DelaySpawnObject(float intervalTime, Action onSpawnObject)
    {
        while (Game.Instance.IsOnPlay)
        {
            yield return new WaitForSeconds(intervalTime);
            onSpawnObject();
        }
    }

    #region 生成物体
    /// <summary>
    /// 生成树木背景物体
    /// </summary>
    private void SpawnTreeObject()
    {
        //生成对象的路径来源
        Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs/EnvObjects/Trees";
        //具体该生成该物品的种类和生成对象的位置
        Game.Instance.a_ObjectPool.Spawn("tree_" + Random.Range(1, 6).ToString(), landOriginPos);
    }

    /// <summary>
    /// 生成山石背景物体
    /// </summary>
    private void SpawnHillObject()
    {
        Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs/EnvObjects/Stones";
        Game.Instance.a_ObjectPool.Spawn("Hill_" + Random.Range(1, 3).ToString(), landOriginPos);
    }


    /// <summary>
    /// 生成飞行天空物体
    /// </summary>
    private void SpawnFlyObjects()
    {
        Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs/EnvObjects/SkyObjetcs/Birds";
        Game.Instance.a_ObjectPool.Spawn("Bird_" + Random.Range(1, 2).ToString(),skyOriginPos);
    }

    /// <summary>
    /// 生成云朵
    /// </summary>
    private void SpawnCloudObjects()
    {
        Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs/EnvObjects/SkyObjetcs/Clouds";
        Game.Instance.a_ObjectPool.Spawn("Cloud_" + Random.Range(1, 3).ToString(), skyOriginPos);
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    private void SpawnEnemyObjects()
    {
        Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs/EnvObjects/Enemys";
        Game.Instance.a_ObjectPool.Spawn("Enemy_" + Random.Range(1, 4).ToString(), eventOriginPos);
    }

    /// <summary>
    /// 生成金币
    /// </summary>
    private void SpawnGold()
    {
        Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs/EnvObjects/Golds";
        Game.Instance.a_ObjectPool.Spawn("Gold_" + Random.Range(1, 3).ToString(), eventOriginPos);
    }
    #endregion

    #region override
    public override void RegisterAttentionEvent()
    {
        AttentionEventList.Add(Consts.E_SpawnEnvObjects);
        AttentionEventList.Add(Consts.E_StopSpawnObjects);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_SpawnEnvObjects:
                StartSpawnLandCoroutine();
                StartSpawnSkyCoroutine();
                StartSpawnEventObjCoroutine();
                break;

            case Consts.E_StopSpawnObjects:
                StopAllCoroutines();
                break;
        }
    }
    #endregion

}
