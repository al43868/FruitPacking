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
    /// ��С��Ʒ
    /// </summary>
    public List<ItemModel> lowItems;
    /// <summary>
    /// �Ŵ���Ʒ
    /// </summary>
    public List<ItemModel> highItems;
    /// <summary>
    /// �����
    /// </summary>
    public int round;
    /// <summary>
    /// ����ͼƬ
    /// </summary>
    public List<Sprite> sprites;
    /// <summary>
    /// ����tag
    /// </summary>
    public List<ItemType> tags;
    public List<Vector2Int> GetRound()
    {
        return GameManager.Instance.GetDataList().rounds[roundID];
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
    blue
}