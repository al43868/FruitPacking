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
    public int boxIndex;
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
        NewBoxes = new List<NewBox>();
        for (int i = 0; i < 3; i++)
        {
            NewBoxes.Add(new()
            {
                box = GameManager.Instance.GetDataList().boxs[0],
                rewards = new(),
                items = new ()
            });
        }
        boxIndex = -1;
        smallEffCount = 2;
        bigEffCount = 2;
    }
}
