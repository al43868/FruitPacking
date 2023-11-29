using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : SerializedSingleTion<GamePlayManager>
{
 
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
                    currentItem.image.color = Color.white;
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
    [SerializeField]
    private ItemUI mouseItem;
    public ItemUI MouseItem 
    {
        get { return mouseItem; }
        set 
        {
            panel.SetItem(value);
            mouseItem = value; 
        }
    }
    /// <summary>
    /// ��ǰ���Ч��
    /// </summary>
    public ClickEff currentClickEff;
    /// <summary>
    /// ��Ʒprefab
    /// </summary>
    public ItemUI itemPrefab;
    /// <summary>
    /// �淨�е���Դ
    /// </summary>
    [SerializeField]
    private GamePlayRes res;
    /// <summary>
    /// ���˵�
    /// </summary>
    public GamePlayPanel panel;
    /// <summary>
    /// ������ڿ�ѡ���Ч��
    /// </summary>
    public ClickEff mouseEff;
    /// <summary>
    /// �Ƿ���һ��box
    /// </summary>
    public bool nextBox;
    /// <summary>
    /// �Ƿ񶯻���
    /// </summary>
    public bool isAnim;
    public void Init()
    {
        res = new();
        res.partons = new();

        int count = GameSaver.Instance.GetData().levelUPs[200] + 2;
        int maxParIndex = 1 +GameSaver.Instance.GetData().levelUPs[201]*2;
        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, maxParIndex);
            res.partons.Add(new(GameManager.Instance.GetDataList().partons[0][index]));
        }

        res.partonIndex = -1;
        res.smallEffCount = GameSaver.Instance.GetData().levelUPs[101] + 2;
        res.bigEffCount = GameSaver.Instance.GetData().levelUPs[102] + 2;
        res.randomEffCount = GameSaver.Instance.GetData().levelUPs[100]+2;
        int itemCount = GameSaver.Instance.GetData().levelUPs[202] +
            GameSaver.Instance.GetData().levelUPs[203] + 
            GameSaver.Instance.GetData().levelUPs[204]+6;
        Vector3 pos = new (-500,-300,0);
        for (int i = 0; i < itemCount; i++)
        {
            int index = UnityEngine.Random.Range(0,GameManager.Instance.GetDataList().items.Count);
            CreatNewItem(GameManager.Instance.GetDataList().items[index], pos);
            pos.x += 70;
        }

        isAnim = false;
    }
    public GamePlayRes GetRes()
    {
        return res;
    }
    [Button]
    public async void NextBox()
    {
        if (isAnim) return;
        if((res.partons.Count - 1)<=res.partonIndex)
        {
            EndDay();
        }
        else
        {
            panel.PlayBGAnim(1);
            panel.NextBox(true);
            isAnim = true;
            //��ǰ����
            if (currentBox != null)
            {
                currentBox.End();
                panel.CloseParton();
                int i = 0;
                foreach (var item in currentBox.items)
                {
                    i += item.item.GetValue();
                }
                GetCoin(i);
                //todo ������ȡ��Ǯ
                _ = currentBox.transform.DOLocalMove(new Vector3(-2000, 0, 0), 2f);
            }

            res.partonIndex++;
            res.smallEffCount+= GameSaver.Instance.GetData().levelUPs[104];
            res.bigEffCount += GameSaver.Instance.GetData().levelUPs[105];
            res.randomEffCount += GameSaver.Instance.GetData().levelUPs[103];
            panel.Reflash();

            //��һ������
            Box go = Instantiate(res.partons[res.partonIndex].model.box, boxs);
            
            go.gridsTr.SetActive(false);
            go.transform.localPosition = new Vector3(2000, 0, 0);
            await go.transform.DOLocalMove(Vector3.zero, 2f);
            currentBox = go;
            go.Init();
            
            panel.PlayBGAnim(0);
            panel.NextBox(false);
            await panel.ShowParton(res.partons[res.partonIndex]);
            isAnim=false;

        }
    }

    private void GetCoin(int i)
    {
        panel.GetCoin(GameSaver.Instance.GetData().coin, i);
        GameSaver.Instance.GetData().coin += i;
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
        if (MouseItem == null)
        {
            if (v)
            {
                MouseItem = itemObj;
            }
        }
        else
        {
            if (v)
            {
                if (MouseItem == itemObj)
                {
                    return;
                }
                else
                {
                    MouseItem = itemObj;
                }
            }
            else
            {
                if(MouseItem == itemObj)
                {
                    MouseItem = null;
                }
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
            CurrentItem.Rotate();
            SetMouseGrid(mouseGridPos, true);
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
                //�������
                if (currentBox.SetItem(mouseGridPos, currentItem))
                {
                    Vector3 v3 = currentBox.GetItemPos(mouseGridPos, currentItem);
                    CurrentItem.transform.SetParent(currentBox.transform, false);
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
            if (MouseItem != null)
            {
                if (currentBox != null)
                {
                    if (currentBox.items.Contains(MouseItem))
                    {
                        currentBox.RemoveItem(MouseItem);
                        CurrentItem = MouseItem;
                        MouseItem = null;
                        return;
                    }

                }
                if (currentClickEff == ClickEff.None)
                {
                    CurrentItem = MouseItem;
                    MouseItem = null;
                }
                else
                {
                    GetEffNewItem(currentClickEff, MouseItem);
                }
            }
            else
            {
                if (nextBox)
                {
                    NextBox();
                }
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
        AudioManager.Instance.PlayMusic(Music.GameEff1);
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
                newItem = GameManager.Instance.GetNewItem(mouseItem,ClickEff.Big);
                break;
            case ClickEff.Small:
                if (res.smallEffCount <= 0)
                {
                    GameManager.Instance.Log(2003);
                    return;
                }
                res.smallEffCount--;
                newItem = GameManager.Instance.GetNewItem(mouseItem, ClickEff.Small);
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