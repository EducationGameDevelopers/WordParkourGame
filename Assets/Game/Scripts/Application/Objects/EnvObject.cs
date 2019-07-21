using UnityEngine;
using System;
using DG.Tweening;

public class EnvObject: ResuableObject
{
    private float moveLength = -14;      //物体移动的距离，单位 m

    public float moveSpeed = 2;      //物体移动的速度，单位 m/s


    protected float MoveLength
    {
        get { return moveLength; }
        set { moveLength = value; }
    }

    protected float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public override void OnSpawn()
    {
        transform.DOLocalMoveX(transform.localPosition.x + moveLength, Math.Abs(moveLength) / moveSpeed).SetEase(Ease.Linear).
            OnComplete(() => { Game.Instance.a_ObjectPool.Unspawn(this.gameObject); });
    }


    public override void OnUnspawn()
    {

    }
}
