using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Coffee.UIEffects;

public class ItemUI : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemObj item;
    public Material outLine;
    public Image image;
    public Vector2Int boxPos;
    public UIShiny shiny;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (shiny != null)
        {
            shiny.enabled = false;
        }
        image.material = outLine;
        GamePlayManager.Instance.SetMouseItem(this, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (shiny != null)
        {
            shiny.enabled = true;
        }
        image.material = null;
        GamePlayManager.Instance.SetMouseItem(this, false);
    }

    internal void Init(ItemObj p)
    {
        item = p;
        this.GetComponent<RectTransform>().sizeDelta=new(p.model.GetHigh() * 100,p.model.GetHigh() * 100);
        int spriteIndex= UnityEngine.Random.Range(0,p.model.sprites.Count);
        image.sprite=p.model.sprites[spriteIndex];
        image.alphaHitTestMinimumThreshold = 0.5f;
        switch (p.itemLevel)
        {
            case ItemLevel.None:
                break;
            case ItemLevel.Nice:
                shiny = this.gameObject.AddComponent<UIShiny>();
                image = GetComponent<Image>();
                shiny.effectPlayer.loop = true;
                shiny.Play(true);
                break;
            default:
                break;
        }
    }

    internal void SetParent(Transform mousePos)
    {
        image.raycastTarget = false;
        image.color = new(1, 1, 1, 0.5f);
        transform.SetParent(mousePos,false);
        transform.localPosition = new(-item.model.GetHigh() * 40,item.model.GetHigh() * 40, 0);
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
                this.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
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