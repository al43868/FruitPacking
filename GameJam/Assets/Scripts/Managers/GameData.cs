using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    /// <summary>
    /// 初始物品数量
    /// </summary>
    public int startItemLevel;
    public Dictionary<int, int> levelUPs;
    /// <summary>
    /// 金钱
    /// </summary>
    public int coin;
    public GameData()
    {
        startItemLevel= 0;
        levelUPs = new();
        //clickeffpanel
        //9种ClickEff升级
        for (int i = 0; i < 9; i++)
        {
            levelUPs.Add(100 + i, 0);
        }
        //basePanel
        //200 基地等级 
        //201 客源等级
        //202-4 货源热亚温
        for (int i = 0; i < 5; i++)
        {
            levelUPs.Add(200 + i, 0);
        }
        coin = 0;
    }
}
