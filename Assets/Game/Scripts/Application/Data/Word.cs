using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 单词类
/// </summary>
public class Word
{
    private string str_Id;
    private string str_English;   //单词的英文
    private string str_Chinese;   //单词的中文释义

    private int wrongMarkCount;   //该单词的错误标记数
    private int rightMarkCount;   //该单词的正确标记数

    public Word(string str_Id, string str_English, string str_Chinese, int wrongMarkCount, int rightMarkCount)
    {
        this.str_Id = str_Id;
        this.str_English = str_English;
        this.str_Chinese = str_Chinese;
        this.wrongMarkCount = wrongMarkCount;
        this.rightMarkCount = rightMarkCount;
    }

    public string Str_Id
    {
        get { return str_Id; }
    }

    public string Str_English
    {
        get { return str_English; }
    }

    public string Str_Chinese
    {
        get { return str_Chinese; }
    }

    public int WrongMarkCount
    {
        get { return wrongMarkCount; }
        set
        {
            if (value <= 1)
            {
                wrongMarkCount = 1;
            }
            else
            {
                wrongMarkCount = value;
            }           
        }
    }

    public int RightMarkCount
    {
        get { return rightMarkCount; }
        set { rightMarkCount = value; }
    }
}

