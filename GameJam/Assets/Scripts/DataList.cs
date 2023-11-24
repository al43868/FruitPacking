using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataList", menuName = "SO/DataList")]
public class DataList : SerializedScriptableObject
{
    public Dictionary<int, List<Vector2Int>> rounds;
    public List<Box> boxs;
    public List<ItemModel> items;
}
