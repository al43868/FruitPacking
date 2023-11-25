using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    /// <summary>
    /// 基地等级
    /// </summary>
    public int homeLevel;
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
        homeLevel= 0;
        startItemLevel= 0;
        levelUPs = new();
        for (int i = 0; i < 9; i++)
        {
            levelUPs.Add(100 + i, 0);
        }
        coin = 0;
    }
}
