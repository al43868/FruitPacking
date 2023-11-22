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
    //            string text = ""; // ����һ���ı�����
    //            print(locale.LocaleName);
    //            if (locale.LocaleName == "Chinese (Simplified) (zh)")
    //            {
    //                text = item.Description; // ��ȡ�����ı�
    //            }
    //            else if (locale.LocaleName == "English (en)")
    //            {
    //                text = item.Des_en; // ��ȡ�����ı�
    //            }
    //            else if (locale.LocaleName == "Spanish (es)")
    //            {
    //                text = item.Des_es; // ��ȡ�����ı�
    //            }
    //            else if (locale.LocaleName == "Russian (ru)")
    //            {
    //                text = item.Des_ru; // ��ȡ�����ı�
    //            }
    //            if (text == "") return "description no language";
    //            //Regex regex = new Regex(@"\bID\d{4}\b");
    //            //MatchCollection matches = regex.Matches(text);
    //            //// ���������е�ÿ��ƥ����
    //            //foreach (Match match in matches)
    //            //{
    //            //    // ��ȡ��ǰƥ�����ֵ����id
    //            //    string idStr = match.Value;

    //            //    // ��idת��Ϊ����
    //            //    int idNum = int.Parse(idStr[2..]);
    //            //    // ��ȡ��ǰid��Ӧ������
    //            //    string name = GetNameByID(idNum);

    //            //    // ����ɫת��Ϊʮ�����Ƹ�ʽ
    //            //    string hexColor = ColorUtility.ToHtmlStringRGB(Color.blue);

    //            //    // �ø��ı���ʽ�滻�ı��е�idΪ���֣���������ɫ��ǩ
    //            //    text = text.Replace(idStr, "<color=#" + hexColor + ">" + name + "</color>");
    //            //    //text = text.Replace(idStr, $"<color=#{hexColor}>{name}</color>");
    //            //}
    //            string pattern = @"_(\d+)_";
    //            text = Regex.Replace(text, pattern, match =>
    //            {
    //                // ��ȡƥ�䵽���»����ڵ��ı�
    //                string matchText = match.Groups[1].Value;

    //                // ���»����ڵ��ı�ת��Ϊ����
    //                int matchId = int.Parse(matchText);

    //                // ��ȡ��ǰƥ�䵽��ID��Ӧ������
    //                string matchName = GetNameByID(matchId);

    //                // �ø��ı���ʽ�滻ƥ�䵽���»����ڵ��ı�Ϊ���֣���������ɫ��ǩ
    //                return "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" + matchName + "</color>";
    //            });
    //            return text;
    //        }
    //    }
    //    return "description no id";
    //}
}
