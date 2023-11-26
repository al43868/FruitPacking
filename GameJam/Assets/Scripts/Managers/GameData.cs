using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
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
        startItemLevel= 0;
        levelUPs = new();
        //clickeffpanel
        //9��ClickEff����
        for (int i = 0; i < 9; i++)
        {
            levelUPs.Add(100 + i, 0);
        }
        //basePanel
        //200 ���صȼ� 
        //201 ��ʼˮ������
        //202 ��Դ�ȼ�
        for (int i = 0; i < 3; i++)
        {
            levelUPs.Add(200 + i, 0);
        }
        coin = 0;
    }
}
