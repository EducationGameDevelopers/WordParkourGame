using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 关卡信息类
/// </summary>
public class Level
{
    public string Name;    //关卡名称

    public string CardImage;  //选择关卡卡片图片

    public string Background;   //背景图片名称

    public string Road;        //路径图片名称

    public int InitScore;       //初始金币

    public List<string> WordIds = new List<string>();   //当前关卡的单词

}

