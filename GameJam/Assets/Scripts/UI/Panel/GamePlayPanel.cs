using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : BasePanel
{
    private static readonly string _name = "GamePlayPanel";
    private static readonly string _path = "GamePlayPanel";
    private static readonly UIType _type = new(_name, _path);
    private TMP_Text boxCountText, coinText;
    private List<MouseEffSet> clickEffs;
    public Animator bgAnim, nextBoxAnim;
    public TMP_Text Eff1Count, Eff2Count, Eff3Count;
    public Transform parton;
    public TMP_Text partonText;
    public Transform itemDes;
    public TMP_Text itemDesText;
    public Transform effsParent;
    public GamePlayPanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        GamePlayManager.Instance.mousePos = UIHelper.GetComponentInChild<Transform>(panelObj, "Mouse");
        GamePlayManager.Instance.boxs = UIHelper.GetComponentInChild<Transform>(panelObj, "Boxs");
        GamePlayManager.Instance.items = UIHelper.GetComponentInChild<Transform>(panelObj, "Items");

        boxCountText = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "BoxCountText");
        coinText = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "CoinText");
        coinText.text = GameSaver.Instance.GetData().coin.ToString();

        Transform effsTr = UIHelper.GetComponentInChild<Transform>(panelObj, "Effs");
        clickEffs = new();
        for (int i = 0; i < 3; i++)
        {
            clickEffs.Add(effsTr.GetChild(i).GetComponent<MouseEffSet>());
        }
       
        Eff1Count = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "Eff1Text");
        Eff2Count = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "Eff2Text");
        Eff3Count = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "Eff3Text");

        GamePlayManager.Instance.panel = this;
        EffManager.Instance.panel1 = this;
        Transform bgs = UIHelper.GetComponentInChild<Transform>(panelObj, "Bgs");
        //todo zhenzheng bg«–ªª
        int bgIndex = GameSaver.Instance.GetData().levelUPs[200];
        bgAnim = bgs.GetChild(0).GetComponent<Animator>();
        nextBoxAnim = UIHelper.GetComponentInChild<Animator>(panelObj, "NextBox");
        PlayBGAnim(0);

        parton = UIHelper.GetComponentInChild<Transform>(panelObj, "Parton");
        partonText = parton.GetChild(0).GetComponent<TMP_Text>();
        parton.localPosition = new Vector3(parton.localPosition.x, 850, 0);

        itemDes = UIHelper.GetComponentInChild<Transform>(panelObj, "ItemDes");
        itemDesText = itemDes.GetChild(0).GetComponent<TMP_Text>();
        itemDes.gameObject.SetActive(false);

        effsParent = UIHelper.GetComponentInChild<Transform>(panelObj, "EffsTr");
        Reflash();
    }

    public void Reflash()
    {
        int count = GamePlayManager.Instance.GetRes().partons.Count - 1 - GamePlayManager.Instance.GetRes().partonIndex;
        boxCountText.text = count.ToString();

        foreach (var item in clickEffs)
        {
            if (item.clickEff == GamePlayManager.Instance.currentClickEff)
            {
                item.animator.Play("choseing");
            }
            else
            {
                item.animator.Play("idle");
            }
        }
        Eff1Count.text = GamePlayManager.Instance.GetRes().randomEffCount.ToString();
        Eff2Count.text = GamePlayManager.Instance.GetRes().smallEffCount.ToString();
        Eff3Count.text = GamePlayManager.Instance.GetRes().bigEffCount.ToString();
    }

    private void NextBox()
    {
        GamePlayManager.Instance.NextBox();
        int count = GamePlayManager.Instance.GetRes().partons.Count - 1 - GamePlayManager.Instance.GetRes().partonIndex;
        if (count > 0)
        {
            boxCountText.text = GameManager.Instance.GetDescriptionByID(4001) + count;
        }
        else
        {
            boxCountText.text = GameManager.Instance.GetDescriptionByID(4002);
        }
    }

    internal void GetCoin(int coin, int i)
    {
        int endCoin = coin + i;
        DOTween.To(() => coin, (value) =>
        {
            coinText.text = value.ToString();
        }, endCoin, 0.5f).SetEase(Ease.Linear);
    }

    public void PlayBGAnim(int speed)
    {
        bgAnim.speed = speed;
    }
    public void NextBox(bool isnext)
    {
        if (isnext)
        {
            nextBoxAnim.Play("moveing");
        }
        else
        {
            nextBoxAnim.Play("idle");
        }
    }
    public void CloseParton()
    {
        partonText.text = "";
        parton.DOLocalMoveY(850, 1f);
    }
    public async UniTask ShowParton(PartonObj par)
    {
        await parton.DOLocalMoveY(245, 1f);
        string des = "";
        foreach (var item in par.rewards)
        {
            des += GameManager.Instance.GetDescriptionByID(item.model.ID);
            des += "<color=blue>"+item.model.reward.GetDes(GamePlayManager.Instance.currentBox)+ "</color>";
            if (item.endLevel > 0)
            {
                des += "\r\n+<color=yellow>" + item.endLevel + "%</color>\r\n";
            }
            else
            {
                des += "\r\n-<color=red>" + item.endLevel + "%</color>\r\n";
            }
        }
        partonText.text = des;
    }
    public void SetItem(ItemUI item)
    {
        if(item == null)
        {
            itemDes.gameObject.SetActive(false);
        }
        else
        {
            itemDes.gameObject.SetActive(true);
            string str = "";
            str+= GameManager.Instance.GetNameByID(item.item.model.ID);
            str+= "\r\n<color=yellow>" + item.item.model.value + "</color>\r\n";
            foreach (var tag in item.item.model.tags)
            {
               str+= GameManager.Instance.GetNameByID((int)tag)+" ";
            }
            itemDesText.text = str;
        }
    }
}