using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayRes 
{
    /// <summary>
    /// 所有客人
    /// </summary>
    public List<PartonObj> partons;
    /// <summary>
    /// 箱子index
    /// </summary>
    public int partonIndex;
    /// <summary>
    /// 随机使用次数
    /// </summary>
    public int randomEffCount;
    /// <summary>
    /// 缩小使用次数
    /// </summary>
    public int smallEffCount;
    /// <summary>
    /// 放大使用次数
    /// </summary>
    public int bigEffCount;
    public GamePlayRes()
    {
        
    }
}
