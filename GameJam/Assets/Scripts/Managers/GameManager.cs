using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : SerializedSingleTion<GameManager>
{
    public List<NewBox> boxes;
    public Transform panel;
    public ItemObj currentItem;
    public DataList dataList;
    protected override void Awake()
    {
        base.Awake();
    }
    public void Log(int textID)
    {
        //GameRoot.Instance.Log(GetDescriptionByID(textID));
    }

    //public string GetDescriptionByID(int id)
    //{
        
    //    foreach (var item in desText.des)
    //    {
    //        if (item.ID == id)
    //        {
    //            var locale = LocalizationSettings.Instance.GetSelectedLocale();
    //            string text = ""; // 定义一个文本变量
    //            print(locale.LocaleName);
    //            if (locale.LocaleName == "Chinese (Simplified) (zh)")
    //            {
    //                text = item.Description; // 获取描述文本
    //            }
    //            else if (locale.LocaleName == "English (en)")
    //            {
    //                text = item.Des_en; // 获取描述文本
    //            }
    //            else if (locale.LocaleName == "Spanish (es)")
    //            {
    //                text = item.Des_es; // 获取描述文本
    //            }
    //            else if (locale.LocaleName == "Russian (ru)")
    //            {
    //                text = item.Des_ru; // 获取描述文本
    //            }
    //            if (text == "") return "description no language";
    //            //Regex regex = new Regex(@"\bID\d{4}\b");
    //            //MatchCollection matches = regex.Matches(text);
    //            //// 遍历集合中的每个匹配项
    //            //foreach (Match match in matches)
    //            //{
    //            //    // 获取当前匹配项的值，即id
    //            //    string idStr = match.Value;

    //            //    // 将id转换为整数
    //            //    int idNum = int.Parse(idStr[2..]);
    //            //    // 获取当前id对应的名字
    //            //    string name = GetNameByID(idNum);

    //            //    // 将颜色转换为十六进制格式
    //            //    string hexColor = ColorUtility.ToHtmlStringRGB(Color.blue);

    //            //    // 用富文本格式替换文本中的id为名字，并加上颜色标签
    //            //    text = text.Replace(idStr, "<color=#" + hexColor + ">" + name + "</color>");
    //            //    //text = text.Replace(idStr, $"<color=#{hexColor}>{name}</color>");
    //            //}
    //            string pattern = @"_(\d+)_";
    //            text = Regex.Replace(text, pattern, match =>
    //            {
    //                // 获取匹配到的下划线内的文本
    //                string matchText = match.Groups[1].Value;

    //                // 将下划线内的文本转换为整数
    //                int matchId = int.Parse(matchText);

    //                // 获取当前匹配到的ID对应的名字
    //                string matchName = GetNameByID(matchId);

    //                // 用富文本格式替换匹配到的下划线内的文本为名字，并加上颜色标签
    //                return "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" + matchName + "</color>";
    //            });
    //            return text;
    //        }
    //    }
    //    return "description no id";
    //}
}
