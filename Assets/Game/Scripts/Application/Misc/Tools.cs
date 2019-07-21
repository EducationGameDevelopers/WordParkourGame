using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using LitJson;
using System.Text.RegularExpressions;    //正则表达式处理

public class Tools
{
    public static JsonData CurrentWordJsonData;    //当前单词Json类
    public static string CurrentSaveJsonDir;      //当前存储Json文件的路径

    /// <summary>
    /// 获取关卡文件集合
    /// </summary>
    public static List<FileInfo> GetLevelFiles()
    {
        TextAsset[] xmlTexts = Resources.LoadAll<TextAsset>("Levels");
      
        List<FileInfo> list = new List<FileInfo>();
        //将文件名称包装成文件加入文件集合中
        for (int i = 0; i < xmlTexts.Length; i++)
        {
            FileInfo file = new FileInfo(xmlTexts[i].name);
            list.Add(file);         
        }

        return list;
    }

    /// <summary>
    /// 填充Level类中的数据(从Xml文件中获取数据并填充)
    /// </summary>
    public static void FillLevel(string fileName, ref Level level)
    {
        XmlDocument doc = new XmlDocument();

        TextAsset myText = Resources.Load("Levels" + "/" + fileName) as TextAsset;
        XmlReader reader = XmlReader.Create(new StringReader(myText.text));

        doc.Load(reader);

        //单个结点写入XML文件
        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.CardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
        level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);

        XmlNodeList nodes;

        //当前关卡的单词解析与填充
        nodes = doc.SelectNodes("/Level/WordIds/WordId");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            string wordId = node.Attributes["Id"].Value;
            level.WordIds.Add(wordId);
        }
        
    }

    

    /// <summary>
    /// 将Resources文件下的单词Json文件信息拷贝到persistentDataPath路径下
    /// </summary>
    /// <param name="wordResDir">原Resources文件夹下单词Json</param>
    /// <param name="wordPerDir">存入的PersistentPath路径</param>
    public static void SaveWordInfoToPersistent(string wordResDir, string wordPerDir)
    {
        //加载Json文件
        TextAsset wordJsonText = Resources.Load<TextAsset>(wordResDir);

        //将Resource文件下的Json转存到persistentDataPath路径下
        JsonData jD = JsonMapper.ToObject(wordJsonText.ToString());
        if (File.Exists(wordPerDir) == false)
        {
            SaveWordToJson(jD, wordPerDir);
        }       
    }

    /// <summary>
    /// 填充Word类中中数据
    /// </summary>
    public static void FillWords(ref Dictionary<string, Word> wordDict, string wordDir)
    {
        //创建文件流
        FileStream fs = new FileStream(wordDir, FileMode.Open);
       
        StreamReader sr = new StreamReader(fs);
      
        JsonData jd = JsonMapper.ToObject(sr.ReadToEnd());
        string str = jd.ToJson();
       
        //json存入后会被转义成编码，使用正则显示中文
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        string wordJson = reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        CurrentWordJsonData = jd;      //选定当前JsonData
        CurrentSaveJsonDir = wordDir;   //选定当前存储路径

        JSONObject jo = new JSONObject(wordJson);
        //遍历解析Json中的信息
        foreach (var temp in jo.list)
        {
            Word word = null;
            string str_Id = temp["Id"].str.ToString().PadLeft(4, '0');    //单词id
            string str_English = temp["English"].str;
            string str_Chinese = temp["Chinese"].str;
            int wrongMarkCount = (int)(temp["WrongMarkCount"].n);
            int rightMarkCount = (int)(temp["RightMarkCount"].n);

            word = new Word(str_Id, str_English, str_Chinese, wrongMarkCount, rightMarkCount);

            wordDict.Add(word.Str_Id, word);
        }

        sr.Close();
        fs.Close();      
    }

    /// <summary>
    /// 将单词信息存入Json文件
    /// </summary>
    public static void SaveWordToJson(JsonData jsonData, string wordInfoDir)
    {
        string str = jsonData.ToJson();
        //json存入后会被转义成编码，使用正则显示中文
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        string modifyString = reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        string pathDir = wordInfoDir;
        FileStream file = null;
        //当该文件路径不存在时
        if (!File.Exists(pathDir))
        {
            //创建该路径文件
             file = File.Create(pathDir);
        }
        else
        {
            //文件流写入UTF8格式内容
            file = new FileStream(pathDir, FileMode.Open);
        }
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(modifyString);

        file.Write(bts, 0, bts.Length);

        //关闭文件流
        file.Close();
        
    }


    /// <summary>
    /// 将单词字典存入相应路径下的Json文件
    /// </summary>
    public static void SaveWordDicToJson(Dictionary<string, Word> wordDic)
    {
        JsonData jd = new JsonData();
        foreach (Word word in wordDic.Values)
        {
            JsonWriter jw = new JsonWriter();
            jw.WriteObjectStart();
            jw.WritePropertyName("Id");
            jw.Write(word.Str_Id);

            jw.WritePropertyName("English");
            jw.Write(word.Str_English);

            jw.WritePropertyName("Chinese");
            jw.Write(word.Str_Chinese);

            jw.WritePropertyName("WrongMarkCount");
            jw.Write(word.WrongMarkCount);

            jw.WritePropertyName("RightMarkCount");
            jw.Write(word.RightMarkCount);
            jw.WriteObjectEnd();

            jd.Add(JsonMapper.ToObject(jw.ToString()));
        }
        Debug.Log(CurrentSaveJsonDir);
        SaveWordToJson(jd, CurrentSaveJsonDir);
    }


    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="url">图片的绝对地址</param>
    /// <param name="render">图片渲染组件</param>
    public static IEnumerator LoadImage(string url, SpriteRenderer render)
    {
        //使用Resources异步加载方式
        ResourceRequest request = Resources.LoadAsync(url, typeof(Texture2D));
        while (!request.isDone)
        {
            yield return request;
        }

        Texture2D texture = request.asset as Texture2D;

        //渲染图片，确定大小、位置
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        render.sprite = sp;
    }


    public static IEnumerator LoadImage(string url, Image image)
    {
        ResourceRequest request = Resources.LoadAsync(url, typeof(Texture2D));
        while (!request.isDone)
        {
            yield return request;
        }

        Texture2D texture = request.asset as Texture2D;
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        image.sprite = sp;
    }

}

