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
    private ItemObj currentItem;
    public ItemObj CurrentItem
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
                    currentItem.transform.localPosition = Vector3.zero;
                }
            }
        }
    }


    public bool canSet;
    public Vector2Int mouseGridPos;
    public Box currentBox;
    public List<Vector2Int> currentItemRound;
    public bool canInBox;
    internal ItemObj mouseItem;

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
                i += item.GetValue();
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

    internal void SetMouseItem(ItemObj itemObj, bool v)
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
            currentItemRound = currentItem.GetRound(pos);
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
                    }
                    else
                    {
                        CurrentItem = mouseItem;
                        mouseItem = null;
                    }
                }
                else
                {
                    CurrentItem = mouseItem;
                    mouseItem = null;
                }
            }
        }
    }
}
