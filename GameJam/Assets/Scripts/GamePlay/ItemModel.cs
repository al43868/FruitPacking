using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ItemModel", menuName = "SO/ItemModel")]
public class ItemModel : SerializedScriptableObject
{
    /// <summary>
    /// 价值
    /// </summary>
    public int value;
    /// <summary>
    /// 占据空间
    /// </summary>
    public List<Vector2Int> round;
    /// <summary>
    /// id
    /// </summary>
    public int ID;
    public ItemModel()
    {
        ID = 1001;
    }
}
