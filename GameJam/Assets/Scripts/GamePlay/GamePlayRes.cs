using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayRes 
{
    /// <summary>
    /// 所有箱子
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
        partons = new ();
        //todo need
        for (int i = 0; i < 3; i++)
        {
            partons.Add(new(GameManager.Instance.GetDataList().partons[0][0]));
        }
        partonIndex = -1;
        smallEffCount = 2;
        bigEffCount = 2;
        randomEffCount = 2;
    }
}
