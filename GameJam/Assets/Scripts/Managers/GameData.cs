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
    /// <summary>
    /// 缩小光线等级
    /// </summary>
    public int smallEffLevel;
    /// <summary>
    /// 放大光线等级
    /// </summary>
    public int bigEffLevel;

    public GameData()
    {

    }
}
