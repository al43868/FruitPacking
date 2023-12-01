using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Drawing.Drawing2D;

public class ItemObj
{
    public Dir dir;
    public ItemModel model;
    public ItemLevel itemLevel;
   
    public ItemObj(ItemModel itemModel,ItemLevel level)
    {
        dir = Dir.Up;
        this.model = itemModel;
        itemLevel = level;//todo need 物品等级 补充
    }
    internal List<Vector2Int> GetRound(Vector2Int pos)
    {
        List<Vector2Int> result = TransformList(model.GetRound(), dir);
       
        for (int i = 0; i < result.Count; i++)
        {
            result[i] = result[i] + pos;
        }
        return result;
    }
    
    public List<Vector2Int> TransformList(List<Vector2Int> originalList,Dir dir)
    {
        int rows = 0;
        int cols = 0;

        foreach (var vector in originalList)
        {
            rows = Math.Max(rows, vector.y + 1);
            cols = Math.Max(cols, vector.x + 1);
        }
        int round=Math.Max(rows, cols);

        int[,] array = new int[round, round];

        foreach (var vector in originalList)
        {
            array[vector.y, vector.x] = 1;
        }

        int[,] rotatedArray = new int[round, round];
        switch (dir)
        {
            case Dir.Up:
                rotatedArray = array;
                break;
            case Dir.Down:
                rotatedArray= RotateArray(array, 180);
                break;
            case Dir.Left:
                rotatedArray = RotateArray(array, 270);
                break;
            case Dir.Right:
                rotatedArray = RotateArray(array, 90);
                break;
            default:
                break;
        }
        List<Vector2Int> transformedList = new ();

        for (int i = 0; i < rotatedArray.GetLength(0); i++)
        {
            for (int j = 0; j < rotatedArray.GetLength(1); j++)
            {
                if (rotatedArray[i, j] == 1)
                {
                    transformedList.Add(new Vector2Int(j, i));
                }
            }
        }
        return transformedList;
    }
    public int[,] RotateArray(int[,] array, int angle)
    {
        int rows = array.GetLength(0);
        int[,] rotatedArray = new int[rows, rows];

        if (angle == 90)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    rotatedArray[j, rows - 1 - i] = array[i, j];
                }
            }
        }
        else if (angle == 180)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    rotatedArray[rows - 1 - i, rows - 1 - j] = array[i, j];
                }
            }
        }
        else if (angle == 270)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    rotatedArray[rows - 1 - j, i] = array[i, j];
                }
            }
        }
        return rotatedArray;
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
    None=4001,
    Nice,
}