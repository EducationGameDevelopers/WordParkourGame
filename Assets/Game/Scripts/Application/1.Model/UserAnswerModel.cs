using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 用户答题相关数据模型
/// </summary>
public class UserAnswerModel : Model
{
    #region 字段
    private static int total_ClickCount;     //总的点击数

    private static int total_RightClickCount;    //总的错误点击数

    private static float total_AnswerTime;    //答题总时间

    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.M_UserAnswerModel; }
    }

    public int Total_ClickCount
    {
        get { return total_ClickCount; }
        set { total_ClickCount = value; }
    }

    public int Total_RightClickCount
    {
        get { return total_RightClickCount ; }
        set { total_RightClickCount = value; }
    }

    public float Total_AnswerTime
    {
        get { return float.Parse(total_AnswerTime.ToString("0.00")); }
        set { total_AnswerTime = value; }
    }

    public float Rate_RightAnswer
    {
        get { return MathTools.GetRate(Total_RightClickCount, Total_ClickCount); }
    }
    #endregion

    #region 方法

    public void InitData()
    {
        total_ClickCount = 0;
        total_RightClickCount = 0;
        total_AnswerTime = 0;
    }
    #endregion
}

