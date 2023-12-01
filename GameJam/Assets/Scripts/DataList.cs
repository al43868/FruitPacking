using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            levelUps.Add(100 + i * 3 + 0, new List<int>(5) { 200, 400, 600, 800, 1000 });
            levelUps.Add(100 + i * 3 + 1, new List<int>(5) { 200, 400, 600, 800, 1000 });
            levelUps.Add(100 + i * 3 + 2, new List<int>(5) { 200, 400, 600, 800, 1000 });
        }
        levelUps.Add(200, new List<int>(5) { 200, 400, 600, 800, 1000 });
        levelUps.Add(201, new List<int>(5) { 200, 400, 600, 800, 1000 });
        levelUps.Add(202, new List<int>(5) { 200, 400, 600, 800, 1000 });
        levelUps.Add(203, new List<int>(5) { 200, 400, 600, 800, 1000 }); 
        levelUps.Add(204, new List<int>(5) { 200, 400, 600, 800, 1000 });
    }
    [Button]
    public void SetItems()
    {
        foreach (var item in items)
        {
            item.smallItems = new();
            item.bigItems = new();
            int round = item.roundID / 100;
            
            foreach (var i in items)
            {
                if (item == i) { continue; }
                int itemRound = i.roundID / 100;
                if (round == 1)
                {
                    if (itemRound == 1)
                    {
                        TryIntoList(item, i, item.smallItems);
                    }
                    else if(itemRound == 2)
                    {
                        TryIntoList(item, i, item.bigItems);
                    }
                    else
                    {
                        continue;
                    }
                } 
                else if (round == 5)
                {
                    if (itemRound == 4)
                    {
                        TryIntoList(item, i, item.smallItems);
                    }
                    else if (itemRound == 5)
                    {
                        TryIntoList(item, i, item.bigItems);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (itemRound == (round-1))
                    {
                        TryIntoList(item, i, item.smallItems);
                    }
                    else if (itemRound == (round+1))
                    {
                        TryIntoList(item, i, item.bigItems);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        AssetDatabase.SaveAssets();
    }

    private void TryIntoList(ItemModel item, ItemModel i, List<ItemModel> itemList)
    {
        if (itemList.Contains(i)) return;
        if (item.bindID == i.bindID)
        {
            itemList.Add(i);
            EditorUtility.SetDirty(item);
        }
        foreach (var color in item.tags)
        {
            if (color != ItemType.roundLow && color != ItemType.roundNormal && color != ItemType.roundHigh)
            {
                if (i.tags.Contains(color))
                {
                    EditorUtility.SetDirty(item);
                    itemList.Add(i);
                    return;
                }
            }
        }
    }
}
