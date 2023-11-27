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
    private List<MouseEffSet> effs;
    public Animator bgAnim, nextBoxAnim;
    public TMP_Text Eff1Count, Eff2Count, Eff3Count;
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
        effs = new();
        for (int i = 0; i < 3; i++)
        {
            effs.Add(effsTr.GetChild(i).GetComponent<MouseEffSet>());
        }
        //effs = new();
        //effs.Add(UIHelper.GetComponentInChild<Toggle>(panelObj, "Eff1"));
        //effs.Add(UIHelper.GetComponentInChild<Toggle>(panelObj, "Eff2"));
        //effs.Add(UIHelper.GetComponentInChild<Toggle>(panelObj, "Eff3"));
        //for (int i = 0; i < 3; i++) Effs
        //{
        //    int j = i;
        //    effs[i].onValueChanged.AddListener((x) => { ChangeEff(x,j); });
        //}
        Eff1Count = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "Eff1Text");
        Eff2Count = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "Eff2Text");
        Eff3Count = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "Eff3Text");

        GamePlayManager.Instance.panel = this;
        Transform bgs = UIHelper.GetComponentInChild<Transform>(panelObj, "Bgs");
        //todo zhenzheng bg«–ªª
        int bgIndex = GameSaver.Instance.GetData().levelUPs[200];
        bgAnim = bgs.GetChild(0).GetComponent<Animator>();
        nextBoxAnim = UIHelper.GetComponentInChild<Animator>(panelObj, "NextBox");
        PlayBGAnim(0);

        Reflash();
    }

    public void Reflash()
    {
        int count = GamePlayManager.Instance.GetRes().partons.Count - 1 - GamePlayManager.Instance.GetRes().partonIndex;
        boxCountText.text = count.ToString();

        foreach (var item in effs)
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
        DOTween.To(() => endCoin, (value) =>
        {
            coinText.text = value.ToString();
        }, coin, 0.5f).SetEase(Ease.Linear);
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
}