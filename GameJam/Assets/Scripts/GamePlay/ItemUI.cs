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
        this.GetComponent<RectTransform>().sizeDelta=new(p.model.wigh*100,p.model.high*100);
        image.sprite=p.model.sprite;
    }

    internal void SetParent(Transform mousePos)
    {
        image.raycastTarget = false;
        transform.SetParent(mousePos,false);
        transform.localPosition = new(-item.model.wigh * 45,item.model.high * 45, 0);
    }
}