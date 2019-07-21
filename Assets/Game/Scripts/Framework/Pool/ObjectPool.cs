using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    //资源对象的路径
    public string ResourcesDir = "";

    //子对象池的映射
    private Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    /// <summary>
    /// 取出对象
    /// </summary>
    public GameObject Spawn(string name, Vector3 pos)
    {
        SubPool pool = null;
        //当映射中没有该名称的映射时
        if (m_pools.ContainsKey(name) == false)
        {
            //根据该名称创建新子对象池
            CreateNewPool(name);
        }
        pool = m_pools[name];
        return pool.Spawn(pos);
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="go"></param>
    public void Unspawn(GameObject go)
    {
        SubPool pool = null;
        //遍历所有子对象池，找到含有该对象的子对象池
        foreach (SubPool p in m_pools.Values)
        {
            if (p.IsContains(go))
            {
                pool = p;
                break;
            }
        }
        pool.Unspawn(go);
    }

    /// <summary>
    /// 回收所有对象
    /// </summary>
    public void UnspawnAll()
    {
        foreach (SubPool p in m_pools.Values)
        {
            p.UnspawnAll();
        }
    }

    /// <summary>
    /// 创建新的子对象池
    /// </summary>
    private void CreateNewPool(string name)
    {
        string path = "";
        //当资源路径为空时
        if (string.IsNullOrEmpty(ResourcesDir.Trim()))
        {
            path = name;
        }
        else
        {
            path = ResourcesDir + "/" + name;
        }

        //根据路径创建游戏对象物体
        GameObject go = Resources.Load<GameObject>(path);

        //创建子对象池并添加入映射
        SubPool pool = new SubPool(Game.Instance.gameObject.transform, go);
        m_pools.Add(pool.Name, pool);
    }

}
