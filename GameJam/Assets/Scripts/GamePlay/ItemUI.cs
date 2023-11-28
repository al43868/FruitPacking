using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemObj item;
    public Material outLine;
    public Image image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.material = outLine;
        GamePlayManager.Instance.SetMouseItem(this, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.material = null;
        GamePlayManager.Instance.SetMouseItem(this, false);
    }

    internal void Init(ItemObj p,ItemLevel level=ItemLevel.None)
    {
        item = p;
        this.GetComponent<RectTransform>().sizeDelta=new(p.model.round*100,p.model.round * 100);
        int spriteIndex= UnityEngine.Random.Range(0,p.model.sprites.Count);
        image.sprite=p.model.sprites[spriteIndex];
    }

    internal void SetParent(Transform mousePos)
    {
        image.raycastTarget = false;
        image.color = new(1, 1, 1, 0.5f);
        transform.SetParent(mousePos,false);
        transform.localPosition = new(-item.model.round * 50,item.model.round * 50, 0);
    }

    internal void Rotate()
    {
        switch (item.dir)
        {
            case Dir.Up:
                item.dir = Dir.Right;
                break;
            case Dir.Down:
                item.dir = Dir.Left;
                break;
            case Dir.Left:
                item.dir = Dir.Up;
                break;
            case Dir.Right:
                item.dir = Dir.Down;
                break;
            default:
                break;
        }
        switch (item.dir)
        {
            case Dir.Up:
                this.transform.rotation =Quaternion.Euler(Vector3.zero);
                break;
            case Dir.Down:
                this.transform.rotation = Quaternion.Euler(new Vector3(0,0,-180));
                break;
            case Dir.Left:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case Dir.Right:
                this.transform.rotation = Quaternion.Euler(new Vector3(0,0,-90));
                break;
            default:
                break;
        }
    }
}