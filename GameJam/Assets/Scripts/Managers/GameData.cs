using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    /// <summary>
    /// ���صȼ�
    /// </summary>
    public int homeLevel;
    /// <summary>
    /// ��ʼ��Ʒ����
    /// </summary>
    public int startItemLevel;
    /// <summary>
    /// ��С���ߵȼ�
    /// </summary>
    public int smallEffLevel;
    /// <summary>
    /// �Ŵ���ߵȼ�
    /// </summary>
    public int bigEffLevel;
    /// <summary>
    /// ��Ǯ
    /// </summary>
    public int coin;
    public GameData()
    {
        homeLevel= 0;
        startItemLevel= 0;
        smallEffLevel= 0;
        bigEffLevel= 0;
        coin= 0;
    }
}
