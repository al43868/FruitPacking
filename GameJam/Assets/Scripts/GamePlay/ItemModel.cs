using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(fileName = "ItemModel", menuName = "SO/ItemModel")]
[Serializable]
public class ItemModel : SerializedScriptableObject
{
    /// <summary>
    /// id
    /// </summary>
    public int ID;
    /// <summary>
    /// ��ֵ
    /// </summary>
    public int value;
    /// <summary>
    /// ռ�ݿռ�
    /// </summary>
    public int roundID;
    /// <summary>
    /// ͬ����ƷID
    /// </summary>
    public int bindID;
    /// <summary>
    /// �����
    /// </summary>
    //public int round;
    /// <summary>
    /// ����ͼƬ
    /// </summary>
    public List<Sprite> sprites;
    /// <summary>
    /// ����tag
    /// </summary>
    public List<ItemType> tags;
    /// <summary>
    /// ��С
    /// </summary>
    public List<ItemModel> smallItems;
    /// <summary>
    /// �Ŵ�
    /// </summary>
    public List<ItemModel> bigItems;
    public List<Vector2Int> GetRound()
    {
        return GameManager.Instance.GetDataList().rounds[roundID];
    }
    public int GetHigh()
    {
        return roundID / 100 ;
    }
    public ItemModel()
    {
        ID = 1001;
    }
}
public enum ItemType
{
    roundLow=6001,
    roundNormal,
    roundHigh,
    green,
    yellow,
    purple,
    red,
    blue,
    orange
}