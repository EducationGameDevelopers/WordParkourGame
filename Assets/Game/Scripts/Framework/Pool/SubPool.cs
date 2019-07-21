using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool : MonoBehaviour {

    private GameObject m_Prefab;     //预制体，预设
    private Transform m_Parent;      //父物体

    private List<GameObject> m_Objects = new List<GameObject>();   //该子对象池中对象集合

    //对象名称
    public string Name
    {
        get { return m_Prefab.name; }
    }

    //构造函数
    public SubPool(Transform parent, GameObject prefab)
    {
        this.m_Prefab = prefab;
        this.m_Parent = parent;

    }

    /// <summary>
    /// 取出对象
    /// </summary>
    public GameObject Spawn(Vector3 pos)
    {
        GameObject go = null;

        foreach (GameObject item in m_Objects)
        {
            //找到一个此时集合中没有使用的对象
            if (item.activeSelf == false)
            {
                go = item;
                break;
            }
        }

        //对象池中没有该物体
        if (go == null)
        {
            //创建该对象物体并放入集合中
            go = GameObject.Instantiate<GameObject>(m_Prefab);
            //设置该对象的父物体
            go.transform.SetParent(m_Parent);
            m_Objects.Add(go);
        }
        go.transform.localPosition = pos;
        go.SetActive(true);
        //赋予该对象初始状态
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return go;
    }

    /// <summary>
    /// 回收一个该对象池中的对象
    /// </summary>
    public void Unspawn(GameObject go)
    {
        if (IsContains(go))
        {
            //隐藏该对象并设置回收状态            
            go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    /// <summary>
    /// 回收所有该对象中的对象
    /// </summary>
    public void UnspawnAll()
    {
        foreach (GameObject item in m_Objects)
        {
            if (item.activeSelf == true)
            {
                Unspawn(item);
            }
        }
    }

    /// <summary>
    /// 判断对象池中是否包含该对象
    /// </summary>
    public bool IsContains(GameObject go)
    {
        return m_Objects.Contains(go);
    }
}
