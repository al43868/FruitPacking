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
            levelUps.Add(100 + i * 3 + 0, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
            levelUps.Add(100 + i * 3 + 1, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
            levelUps.Add(100 + i * 3 + 2, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
        }
        levelUps.Add(200, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
        levelUps.Add(201, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
        levelUps.Add(202, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
        levelUps.Add(203, new List<int>(5) { 300, 600, 1200, 2000, 3000 }); 
        levelUps.Add(204, new List<int>(5) { 300, 600, 1200, 2000, 3000 });
    }
    public void  Additems(List<ItemModel> newItems)
    {
        if (!items.Contains(newItems[0]))
        {
            foreach (var item in newItems)
            {
                items.Add(item);
            }
        }
    }
}
