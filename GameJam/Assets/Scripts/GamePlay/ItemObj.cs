using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemObj
{
    public Dir dir;
    public ItemModel model;
    public ItemLevel itemLevel;
   
    public ItemObj(ItemModel itemModel)
    {
        dir = Dir.Up;
        this.model = itemModel;
        itemLevel = ItemLevel.None;//todo need 物品等级
    }
    internal List<Vector2Int> GetRound(Vector2Int pos)
    {
        List<Vector2Int> result = new ();
        switch (dir)
        {
            case Dir.Up:
                foreach (var item in model.GetRound())
                {
                    result.Add(item+pos);
                }
                break;
            case Dir.Down:
                break;
            case Dir.Left:
                break;
            case Dir.Right:
                break;
            default:
                break;
        }
        return result;
    }

    internal int GetValue()
    {
        switch (itemLevel)
        {
            case ItemLevel.None:
                return model.value;
            default:
                break;
        }
        return model.value;
    }
}
public enum Dir
{
    Up,
    Down,
    Left,
    Right,
}
public enum ItemLevel
{
    None,
}