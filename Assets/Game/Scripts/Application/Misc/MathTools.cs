using System;
using System.Collections.Generic;

public class MathTools
{

    /// <summary>
    /// 普通随机抽取
    /// </summary>
    /// <param name="rand"></param>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static int[] RandomExtract(Random rand, List<int> datas, int returnCount)
    {
        List<int> result = new List<int>();
        if (rand != null)
        {
            for (int i = returnCount; i > 0;)
            {
                //获取datas集合中的一个随机整数
                int item = datas[rand.Next(25)];
                if (result.Contains(item))
                    continue;
                else
                {
                    result.Add(item);
                    i--;
                }
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// 受控随机抽取
    /// </summary>
    public static int[] ControllerRandomExtract(Random rand, List<int> datas, List<int> weights, int returnCount)
    {
        int nItemCount = datas.Count;

        List<int> result = new List<int>();

        if (rand != null)
        {
            //临时变量
            Dictionary<int, int> dict = new Dictionary<int, int>(nItemCount);
            List<KeyValuePair<int, int>> listDict1 = new List<KeyValuePair<int, int>>();
            //为每一项算一个随机数并乘相应的权值
            for (int i = 0; i < datas.Count; i++)
            {
                dict.Add(datas[i], rand.Next(10) * weights[i]);
            }
            listDict1.AddRange(dict);

            //根据权值对字典集合排序
            List<KeyValuePair<int, int>> listDict = SortByValue(dict);

            //拷贝抽取权值最大值的前Count项
            foreach (KeyValuePair<int, int> kvp in listDict.GetRange(0, returnCount))
            {
                result.Add(kvp.Key);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// 排序集合
    /// </summary>
    private static List<KeyValuePair<int, int>> SortByValue(Dictionary<int, int> dict)
    {
        List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();

        if (dict != null)
        {
            list.AddRange(dict);

            list.Sort(
                delegate (KeyValuePair<int, int> kvp1, KeyValuePair<int, int> kvp2)
                {
                    return kvp2.Value - kvp1.Value;
                });
        }

        return list;
    }


    /// <summary>
    /// 获取比例
    /// </summary>
    public static float GetRate(float part, float total)
    {
        if (total == 0)
        {
            return float.Parse(0.ToString("00.00"));
        }
        float result = (part / total) * 100;
        return float.Parse(result.ToString("00.00"));
    }

}
