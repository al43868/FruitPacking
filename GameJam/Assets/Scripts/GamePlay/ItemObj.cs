using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemObj:SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Dir dir;
    public ItemModel model;
    public Material outLine;
    public ItemObj(ItemModel itemModel)
    {
        dir = Dir.Up;
        this.model = itemModel;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        image.material = outLine;
        GamePlayManager.Instance.mouseItem = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.material = null;
    }
    internal List<Vector2Int> GetRound(Vector2Int pos)
    {
        List<Vector2Int> result = new ();
        switch (dir)
        {
            case Dir.Up:
                foreach (var item in model.round)
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
}
public enum Dir
{
    Up,
    Down,
    Left,
    Right,
}