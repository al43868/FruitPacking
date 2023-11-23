using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ItemModel", menuName = "SO/ItemModel")]
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
    public List<Vector2Int> GetRound()
    {
        return GameManager.Instance.dataList.rounds[roundID];
    }
    public ItemModel()
    {
        ID = 1001;
    }
}
