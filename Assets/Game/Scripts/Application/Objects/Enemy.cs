using DG.Tweening;
using System;
public class Enemy : EnvObject
{
    private Sequence sequence;
    private void Awake()
    {
        MoveLength = -7;
    }

    public override void OnSpawn()
    {
        sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMoveX(transform.localPosition.x + MoveLength, Math.Abs(MoveLength) / moveSpeed).SetEase(Ease.Linear).
            OnComplete(() => GameData.Instance.AddKillEnemyCount()));

        sequence.AppendCallback(() => Game.Instance.a_ObjectPool.Unspawn(gameObject));
    }

}
