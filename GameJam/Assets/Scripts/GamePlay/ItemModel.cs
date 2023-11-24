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
    /// ��ֵ
    /// </summary>
    public int value;
    /// <summary>
    /// ռ�ݿռ�
    /// </summary>
    public int roundID;
    /// <summary>
    /// id
    /// </summary>
    public int ID;
    /// <summary>
    /// ��С��Ʒ
    /// </summary>
    public List<ItemModel> lowItems;
    /// <summary>
    /// �Ŵ���Ʒ
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
