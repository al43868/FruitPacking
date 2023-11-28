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
    /// 价值
    /// </summary>
    public int value;
    /// <summary>
    /// 占据空间
    /// </summary>
    public int roundID;
    /// <summary>
    /// 同类物品ID
    /// </summary>
    public int bindID;
    /// <summary>
    /// 宽与高
    /// </summary>
    //public int round;
    /// <summary>
    /// 所有图片
    /// </summary>
    public List<Sprite> sprites;
    /// <summary>
    /// 所有tag
    /// </summary>
    public List<ItemType> tags;
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
    roundLow,
    roundNormal,
    roundHigh,
    green,
    yellow,
    purple,
    red,
    blue,
    orange
}