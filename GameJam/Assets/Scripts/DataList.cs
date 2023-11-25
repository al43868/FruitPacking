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
    public List<List<PartonModel>> partons;
    public Dictionary<int, List<int>> levelUps;


    [Button]
    public void SetLevelUP()
    {
        levelUps = new Dictionary<int, List<int>>();
        for (int i = 0; i < 3; i++)
        {
            levelUps.Add(100 + i * 3 + 0, new List<int>(5) { 300, 900, 3000, 4500, 9000 });
            levelUps.Add(100 + i * 3 + 1, new List<int>(5) { 700, 2500, 6000, 12000, 30000 });
            levelUps.Add(100 + i * 3 + 2, new List<int>(5) { 1100, 1500, 3000, 4500, 9000 });
        }
    }
}
