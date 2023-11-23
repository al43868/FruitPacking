using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : SerializedSingleTion<GamePlayManager>
{
    public List<NewBox> boxes;
    public Transform panel, boxs, items;
    public Transform mousePos;
    [SerializeField]
    private ItemUI currentItem;
    public ItemUI CurrentItem
    {
        get { return currentItem; }
        set
        {
            if (value == null)
            {
                if (currentItem == null)
                {
                    return;
                }
                else
                {
                    currentItem.image.raycastTarget = true;
                    currentItem = null;
                }
            }
            else
            {
                if (currentItem == null)
                {
                    currentItem = value;
                    currentItem.image.raycastTarget = false;
                    currentItem.transform.SetParent(mousePos);
                    currentItem.transform.localPosition = new(value.item.model.wigh*50,
                        value.item.model.high * 50,0);
                }
            }
        }
    }


    public bool canSet;
    public Vector2Int mouseGridPos;
    public Box currentBox;
    public List<Vector2Int> currentItemRound;
    public bool canInBox;
    internal ItemUI mouseItem;
    public ClickEff clickEff;
    public ClickEff mouseEff;
    public ItemUI itemPrefab;
    [Button]
    public async void NextBox()
    {
        //当前盒子
        if (currentBox != null)
        {
            currentBox.End();

            int i = 0;
            foreach (var item in currentBox.items)
            {
                i += item.item.GetValue();
            }
            print(i);//todo
            _ = currentBox.transform.DOLocalMove(new Vector3(-2000, 0, 0), 2f);
        }

        //下一个盒子
        Box go = GameObject.Instantiate(boxes[0].box, boxs);
        go.gridsTr.SetActive(false);
        go.transform.localPosition = new Vector3(2000, 0, 0);
        await go.transform.DOLocalMove(Vector3.zero, 2f);
        currentBox = go;
        go.Init();
    }

    internal void SetMouseItem(ItemUI itemObj, bool v)
    {
        if (mouseItem == null)
        {
            if (v)
            {
                mouseItem= itemObj;
            }
        }
        else
        {
            if(mouseItem == itemObj)
            {
                mouseItem = null;
            }
        }
    }
    internal void UpdateMousePos(Vector3 mousePostion)
    {
        mousePos.position = mousePostion;
    }
    public void SetMouseGrid(Vector2Int pos, bool set = true)
    {
        if (set)
        {
            if (currentItem == null) return;
            currentBox.Clear();
            mouseGridPos = pos;
            currentItemRound = currentItem.item.GetRound(pos);
            currentBox.ChoseGrid(currentItemRound);
            canInBox = true;
        }
        else
        {
            if (mouseGridPos == pos)
            {
                currentBox.Clear();
                canInBox=false;
            }
        }

    }
    internal void Rotate()
    {
        if (CurrentItem != null)
        {

        }
    }

    internal void SetMouseClickEff(ClickEff clickEff, bool v)
    {
        if (v)
        {
            if(mouseEff != clickEff)
            {
                mouseEff = clickEff;
            }
        }
        else
        {
            if (mouseEff == clickEff)
            {
                mouseEff = ClickEff.None;
            }
        }
    }
    internal void LeftClick()
    {
        if (CurrentItem != null)
        {
            if (canSet)
            {
                Vector3 v3 = CurrentItem.transform.position;
                CurrentItem.transform.SetParent(items, false);
                CurrentItem.transform.position = v3;
                CurrentItem = null;
            }
            else if (canInBox)
            {
                //放入盒子
                if (currentBox.SetItem(mouseGridPos, currentItem))
                {
                    Vector3 v3  = currentBox.GetItemPos(mouseGridPos,currentItem);
                    CurrentItem.transform.SetParent(currentBox.transform, false);
                    CurrentItem.transform.position = v3;
                    currentBox.items.Add(CurrentItem);
                    CurrentItem = null;
                    canSet = false;
                }
            }
        }
        else
        {
            if (mouseItem != null)
            {
                if (currentBox != null)
                {
                    if (currentBox.items.Contains(mouseItem))
                    {
                        currentBox.RemoveItem(mouseItem, mouseGridPos);
                        CurrentItem = mouseItem;
                        mouseItem = null;
                        return;
                    }
                    
                }
                if (clickEff == ClickEff.None)
                {
                    CurrentItem = mouseItem;
                    mouseItem = null;
                }
                else
                {
                    GetNewItem(clickEff, mouseItem);
                }
            }
            else
            {
                if (mouseEff != ClickEff.None)
                {
                    if (mouseEff != clickEff)
                    {
                        clickEff = mouseEff;
                    }
                }
                else
                {
                    if (clickEff != ClickEff.None)
                    {
                        clickEff = ClickEff.None;
                    }
                }
            }
        }
    }

    private void GetNewItem(ClickEff eff, ItemUI mouseItem)
    {
        if (eff == ClickEff.None) return;
        print(1);
        print(mouseItem);
        ItemModel newItem=null;
        switch (eff)
        {
            case ClickEff.None:
                break;
            case ClickEff.Big:
                newItem = mouseItem.item.model.highItems[0];
                break;
            case ClickEff.Small:
                newItem = mouseItem.item.model.lowItems[0];
                break;
            default:
                break;
        }
        //todo pinzhi
        Vector3 pos = mouseItem.transform.localPosition;
        var go= GameObject.Instantiate(itemPrefab, items);
        go.transform.localPosition = pos;
        go.Init(new(newItem));
        GameObject.Destroy(mouseItem.gameObject);
    }
}
public enum ClickEff
{
    None,
    Big,
    Small,

}