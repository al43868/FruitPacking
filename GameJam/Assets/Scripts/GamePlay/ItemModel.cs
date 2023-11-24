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
    /// 价值
    /// </summary>
    public int value;
    /// <summary>
    /// 占据空间
    /// </summary>
    public int roundID;
    /// <summary>
    /// id
    /// </summary>
    public int ID;
    /// <summary>
    /// 缩小物品
    /// </summary>
    public List<ItemModel> lowItems;
    /// <summary>
    /// 放大物品
    /// </summary>
    public List<ItemModel> highItems;
    public float wigh, high;
    public Sprite sprite;
    public List<Vector2Int> GetRound()
    {
        return GameManager.Instance.GetDataList().rounds[roundID];
    }
    public ItemModel()
    {
        ID = 1001;
    }
}
