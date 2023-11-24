using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;

public class Box : MonoBehaviour
{
    public SerializableDictionary<Vector2Int, Grid> grids;
    public GameObject gridsTr;
    public int wigh, high;
    public List<ItemUI> items;
    [Button]
    public void AddGrids(List<Grid> mouseGrid)
    {
        grids = new();
        int index = 0;
        for (int i = 0; i < high; i++)
        {
            for (int j = 0; j < wigh; j++)
            {
                grids.Add(new Vector2Int(j, i), mouseGrid[index]);
                mouseGrid[index].pos = new Vector2Int(j, i);
                index++;
            }
        }
    }
    public async void Init()
    {
        gridsTr.SetActive(true);
        foreach (var item in grids)
        {
            item.Value.transform.localScale = Vector3.zero;
            _ = item.Value.transform.DOScale(Vector3.one, 0.5f);
        }
        await UniTask.Delay(100);
        foreach (var item in grids)
        {
            item.Value.Init();
        }
    }
    internal void End()
    {
        gridsTr.SetActive(false);
    }
    public void ChoseGrid(List<Vector2Int> rounds)
    {
        foreach (var item in rounds)
        {
            if (grids.ContainsKey(item))
            {
                grids[item].Choseing();
            }
        }
    }
    public void UnChoseGrid(List<Vector2Int> rounds)
    {
        foreach (var item in rounds)
        {
            if (grids.ContainsKey(item))
            {
                grids[item].UnChoseing();
            }
        }
    }
    internal void Clear()
    {
        foreach (var item in grids)
        {
            item.Value.Clear();
        }
    }

    internal bool SetItem(Vector2Int mouseGridPos, ItemUI currentItem)
    {
        foreach (var item in currentItem.item.GetRound(mouseGridPos))
        {
            if (!grids.ContainsKey(item))
            {
                GameManager.Instance.Log(2001);
                return false;
            }
            if (grids[item].isSet)
            {
                GameManager.Instance.Log(2002);
                return false;
            }
        }
        foreach (var item in currentItem.item.GetRound(mouseGridPos))
        {
            grids[item].isSet = true;
        }
        return true;
    }

    internal Vector3 GetItemPos(Vector2Int pos, ItemUI currentItem)
    {
        return grids[pos].transform.position + new Vector3(-(currentItem.item.model.wigh-1) * 50,
                        (currentItem.item.model.high-1) * 50, 0);
    }
        internal void RemoveItem(ItemUI mouseItem,Vector2Int pos)
    {
        foreach (var item in mouseItem.item.GetRound(pos))
        {
            if (grids.ContainsKey(item))
            {
                grids[item].isSet = false;
            }
        }
        if (items.Contains(mouseItem))
        {
            items.Remove(mouseItem);
        }
    }
}
