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
    public Dictionary<int, int> levelUPs;
    /// <summary>
    /// ��Ǯ
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
