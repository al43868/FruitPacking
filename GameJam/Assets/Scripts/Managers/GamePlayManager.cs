using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : SerializedSingleTion<GamePlayManager>
{
    /// <summary>
    /// 所有箱子
    /// </summary>
    public List<NewBox> boxes;

    //所有panel中的元素
    /// <summary>
    /// panel中的分层
    /// </summary>
    public Transform boxs, items;

    /// <summary>
    /// 鼠标位置
    /// </summary>
    public Transform mousePos;

    /// <summary>
    /// 当前物品
    /// </summary>
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
                    currentItem.SetParent(mousePos);

                }
                else
                {
                    currentItem.image.raycastTarget = true;
                    currentItem = null;

                    currentItem = value;
                    currentItem.SetParent(mousePos);
                }
            }
        }
    }
    /// <summary>
    /// 放下物品
    /// </summary>
    public bool canSet;
    /// <summary>
    /// 鼠标所在格子
    /// </summary>
    public Vector2Int mouseGridPos;
    /// <summary>
    /// 当前盒子
    /// </summary>
    public Box currentBox;
    /// <summary>
    /// 放进盒子
    /// </summary>
    public bool canInBox;
    /// <summary>
    /// 鼠标所在物品
    /// </summary>
    public ItemUI mouseItem;
    /// <summary>
    /// 当前点击效果
    /// </summary>
    public ClickEff currentClickEff;
    /// <summary>
    /// 鼠标所在可选点击效果
    /// </summary>
    //public ClickEff mouseEff;
    /// <summary>
    /// 物品prefab
    /// </summary>
    public ItemUI itemPrefab;
    /// <summary>
    /// 玩法中的资源
    /// </summary>
    private GamePlayRes res;
    public GamePlayPanel panel;
    public ClickEff mouseEff;

    public void Init()
    {
        res = new();

    }
    public GamePlayRes GetRes()
    {
        return res;
    }
    [Button]
    public async void NextBox()
    {
        if((res.partons.Count - 1)<=res.partonIndex)
        {
            EndDay();
        }
        else
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
                GameSaver.Instance.GetData().coin += i;
                //todo 真正获取金钱
                _ = currentBox.transform.DOLocalMove(new Vector3(-2000, 0, 0), 2f);
            }

            res.partonIndex++;
            //下一个盒子
            Box go = Instantiate(res.partons[res.partonIndex].model.box, boxs);
            
            go.gridsTr.SetActive(false);
            go.transform.localPosition = new Vector3(2000, 0, 0);
            await go.transform.DOLocalMove(Vector3.zero, 2f);
            currentBox = go;
            go.Init();
        }
    }

    private void EndDay()
    {
        GameManager.Instance.LoadScene(2);
    }

    internal void SetClickEff(ClickEff clickEff)
    {
        if (currentClickEff == clickEff)
        {
            currentClickEff = ClickEff.None;
        }
        else
        {
            currentClickEff = clickEff;
        }
        panel.Reflash();
    }

    internal void SetMouseItem(ItemUI itemObj, bool v)
    {
        if (mouseItem == null)
        {
            if (v)
            {
                mouseItem = itemObj;
            }
        }
        else
        {
            if (mouseItem == itemObj)
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
            currentBox.ChoseGrid(currentItem.item.GetRound(pos));
            canInBox = true;
        }
        else
        {
            if (mouseGridPos == pos)
            {
                currentBox.Clear();
                canInBox = false;
            }
        }

    }
    internal void Rotate()
    {
        if (CurrentItem != null)
        {

        }
    }
    internal void SetMouseEff(ClickEff clickEff, bool v)
    {
        if (v)
        {
            if (mouseEff != clickEff)
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
                    Vector3 v3 = currentBox.GetItemPos(mouseGridPos, currentItem);
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
                if (currentClickEff == ClickEff.None)
                {
                    CurrentItem = mouseItem;
                    mouseItem = null;
                }
                else
                {
                    GetEffNewItem(currentClickEff, mouseItem);
                }
            }
            else
            {
                if (mouseEff != ClickEff.None)
                {
                    if (mouseEff != currentClickEff)
                    {
                        currentClickEff = mouseEff;
                    }
                    else
                    {
                        currentClickEff = ClickEff.None;
                    }
                }
                else
                {
                    if (currentClickEff != ClickEff.None)
                    {
                        currentClickEff = ClickEff.None;
                    }
                }
            }
        }
        panel.Reflash();
    }

    private void GetEffNewItem(ClickEff eff, ItemUI mouseItem)
    {
        if (eff == ClickEff.None) return;
        ItemModel newItem = null;
        switch (eff)
        {
            case ClickEff.None:
                break;
            case ClickEff.Big:
                if (res.bigEffCount <= 0)
                {
                    GameManager.Instance.Log(2004);
                    return;
                }
                res.bigEffCount--;
                newItem = mouseItem.item.model.highItems[0];
                break;
            case ClickEff.Small:
                if (res.smallEffCount <= 0)
                {
                    GameManager.Instance.Log(2003);
                    return;
                }
                res.smallEffCount--;
                newItem = mouseItem.item.model.lowItems[0];
                break;
            default:
                break;
        }
        //todo pinzhi
        Vector3 pos = mouseItem.transform.localPosition;
        CreatNewItem(newItem, pos);
        GameObject.Destroy(mouseItem.gameObject);
    }
    public void CreatNewItem(ItemModel item, Vector3 pos)
    {
        var go = GameObject.Instantiate(itemPrefab, items);
        go.transform.localPosition = pos;
        go.Init(new(item));
    }
}
public enum ClickEff
{
    None,
    Big,
    Small,
    Random
}