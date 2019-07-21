using UnityEngine;
using DG.Tweening;

public class CoronaDT : MonoBehaviour {

    private Vector2 originOptionScale;     //选项的原始尺寸
    private Vector3 originOptionPos;       //选项的原始位置

    private Sequence coronaSequence;    //动画序列

    private GameObject coronaLightGO;   //发光轮盘
    private GameObject DTMask;    //动画播放时的遮罩

    private Transform currentOption;   //当前操作的选项
    private Transform rightOptionLight = null;   //正确选项发光特效

    private Transform Bg_Corona;   //轮盘

    private float duration_CoronaRotate = 0.6f;   //轮盘旋转固定角度所用时间

    private void Awake()
    {
        Bg_Corona = transform.Find("Bg_Corona").transform;

        coronaLightGO = Bg_Corona.Find("CoronaLight").gameObject;
        DTMask = Bg_Corona.Find("DTMask").gameObject;
        

        coronaLightGO.SetActive(false);
        DTMask.SetActive(false);
    }

    /// <summary>
    /// 选项点击的动画效果
    /// </summary>
    public void OptionDT(Transform optionTran, bool isRight)
    {
        //序列动画的实例必须与Append等方法在同一一个函数下
        coronaSequence = DOTween.Sequence();

        currentOption = optionTran;

        //记录此次操作的选项的原始位置与尺寸
        originOptionPos = currentOption.localPosition;
        originOptionScale = currentOption.localScale;

        rightOptionLight = currentOption.Find("RightEffect").transform;
        //动画遮罩开启，此时无法对轮盘进行点击
        DTMask.SetActive(true);
        //回答正确
        if (isRight == true)
        {
            if (rightOptionLight == null)
                rightOptionLight = currentOption.Find("RightEffect").transform;

            //轮盘为发光模式
            coronaLightGO.SetActive(true);
            //轮盘旋转一周
            coronaSequence.Append(Bg_Corona.DOLocalRotate(new Vector3(0, 0, -360), duration_CoronaRotate, RotateMode.LocalAxisAdd));

            //该选项变大
            coronaSequence.Append(currentOption.DOScale(1.3f, 0.5f).OnComplete(OptionSelfDT));
            //正确选项反光特效
            coronaSequence.Insert(duration_CoronaRotate, rightOptionLight.DOScale(Vector2.one, 0.4f));                     
        }
        //回答错误
        else
        {
            OptionSelfDT();
        }
    }

    /// <summary>
    /// 选项自身动画
    /// </summary>
    private void OptionSelfDT()
    {
        coronaSequence = DOTween.Sequence();
        //选项移动到中间位置
        coronaSequence.Append(currentOption.DOLocalMove(Vector3.zero, 0.5f));

        //选项缩小，之后重置轮盘
        coronaSequence.Append(currentOption.DOScale(Vector3.zero, 0.5f).OnComplete(RemodelCorona));       
    }

    /// <summary>
    /// 重置轮盘状态
    /// </summary>
    public void RemodelCorona()
    {               
        //结束动画序列
        coronaSequence.Complete();

        //对象返回初始状态
        currentOption.localPosition = originOptionPos;
        currentOption.localScale = originOptionScale;
        
        Bg_Corona.localRotation = Quaternion.identity;

        if (rightOptionLight != null)
            rightOptionLight.localScale = Vector3.zero;

        DTMask.SetActive(false);
        coronaLightGO.SetActive(false);       
    }
}
