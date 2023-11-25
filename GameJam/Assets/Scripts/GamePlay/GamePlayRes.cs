using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayRes 
{
    /// <summary>
    /// ��������
    /// </summary>
    public List<PartonObj> partons;
    /// <summary>
    /// ����index
    /// </summary>
    public int partonIndex;
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
        partons = new ();
        //todo
        for (int i = 0; i < 3; i++)
        {
            partons.Add(new(GameManager.Instance.GetDataList().partons[0][0]));
        }
        partonIndex = -1;
        smallEffCount = 2;
        bigEffCount = 2;
    }
}
