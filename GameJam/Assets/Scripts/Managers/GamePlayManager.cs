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
    /// ��������
    /// </summary>
    public List<NewBox> boxes;

    //����panel�е�Ԫ��
    /// <summary>
    /// panel�еķֲ�
    /// </summary>
    public Transform boxs, items;
    /// <summary>
    /// ���λ��
    /// </summary>
    public Transform mousePos;

    /// <summary>
    /// ��ǰ��Ʒ
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
    /// ������Ʒ
    /// </summary>
    public bool canSet;
    /// <summary>
    /// ������ڸ���
    /// </summary>
    public Vector2Int mouseGridPos;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    public Box currentBox;
    /// <summary>
    /// �Ž�����
    /// </summary>
    public bool canInBox;
    /// <summary>
    /// ���������Ʒ
    /// </summary>
    public ItemUI mouseItem;
    /// <summary>
    /// ��ǰ���Ч��
    /// </summary>
    public ClickEff currentClickEff;
    /// <summary>
    /// ������ڿ�ѡ���Ч��
    /// </summary>
    public ClickEff mouseEff;
    /// <summary>
    /// ��Ʒprefab
    /// </summary>
    public ItemUI itemPrefab;
    /// <summary>
    /// �淨�е���Դ
    /// </summary>
    private GamePlayRes res;
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
            //��ǰ����
            if (currentBox != null)
            {
                currentBox.End();

                int i = 0;
                foreach (var item in currentBox.items)
                {
                    i += item.item.GetValue();
                }
                GameSaver.Instance.GetData().coin += i;
                //todo ������ȡ��Ǯ
                _ = currentBox.transform.DOLocalMove(new Vector3(-2000, 0, 0), 2f);
            }

            res.partonIndex++;
            //��һ������
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

    internal void SetMouseClickEff(ClickEff clickEff, bool v)
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
                //�������
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
    Small
}