using System.Collections;
using System.Collections.Generic;

public interface IReusable
{
    /// <summary>
    /// 当使用时调用
    /// </summary>
    void OnSpawn();

    /// <summary>
    /// 当回收时调用
    /// </summary>
    void OnUnspawn();

}
