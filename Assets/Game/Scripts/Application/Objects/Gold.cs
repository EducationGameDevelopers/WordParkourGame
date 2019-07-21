using System;
using DG.Tweening;
using UnityEngine;

public class Gold : EnvObject
{
    private Sequence sequence;

    private void Awake()
    {
        MoveLength = -7;
    }

    public override void OnSpawn()
    {
        sequence = DOTween.Sequence();
        sequence.Append( transform.DOLocalMoveX(transform.localPosition.x + MoveLength, Math.Abs(MoveLength) / moveSpeed).SetEase(Ease.Linear));
        sequence.Append(transform.DOLocalMove(new Vector3(-5.1f, 5.6f, 0), 2).
            OnComplete(() => GameData.Instance.AddTempTreasure()));
        sequence.AppendCallback(() => Game.Instance.a_ObjectPool.Unspawn(gameObject));
    }

    public override void OnUnspawn()
    {
        
    }
}