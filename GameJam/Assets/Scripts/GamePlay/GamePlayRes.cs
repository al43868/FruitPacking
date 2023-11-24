using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayRes 
{
    /// <summary>
    /// ��������
    /// </summary>
    public List<NewBox> NewBoxes;
    /// <summary>
    /// ����index
    /// </summary>
    public int boxIndex;
    /// <summary>
    /// ��Сʹ�ô���
    /// </summary>
    public int smallEffCount;
    /// <summary>
    /// �Ŵ�ʹ�ô���
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
