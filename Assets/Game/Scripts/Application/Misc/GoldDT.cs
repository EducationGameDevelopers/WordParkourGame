using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GoldDT : ResuableObject, IReusable
{

    public override void OnSpawn()
    {
        transform.DOLocalMove(new Vector2(-430, 340), 1f).OnComplete(MyUpspawn);
        transform.DOScale(0, 1f);
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }


    void MyUpspawn()
    {
        Game.Instance.a_ObjectPool.Unspawn(this.gameObject);
    }
    public override void OnUnspawn()
    {
        
    }
}
