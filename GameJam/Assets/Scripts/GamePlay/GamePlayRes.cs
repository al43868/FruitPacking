using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayRes 
{
    /// <summary>
    /// 所有箱子
    /// </summary>
    public List<NewBox> NewBoxes;
    /// <summary>
    /// 箱子index
    /// </summary>
    public int index;
    /// <summary>
    /// 缩小使用次数
    /// </summary>
    public int smallEffCount;
    /// <summary>
    /// 放大使用次数
    /// </summary>
    public int bigEffCount;
}
