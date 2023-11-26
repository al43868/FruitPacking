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
    private TMP_Text boxCountText;
    private List<MouseEffSet> effs;
    public GamePlayPanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        GamePlayManager.Instance.mousePos = UIHelper.GetComponentInChild<Transform>(panelObj, "Mouse");
        GamePlayManager.Instance.boxs = UIHelper.GetComponentInChild<Transform>(panelObj, "Boxs");
        GamePlayManager.Instance.items = UIHelper.GetComponentInChild<Transform>(panelObj, "Items");

        Button nextBox = UIHelper.GetComponentInChild<Button>(panelObj, "NextBox");
        nextBox.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayMusic(Music.Button1);
            NextBox();
        });
        boxCountText = nextBox.transform.GetChild(0).GetComponent<TMP_Text>();
        GamePlayManager.Instance.CreatNewItem(GameManager.Instance.GetDataList().items[0],
            Vector3.zero);
        Transform effsTr= UIHelper.GetComponentInChild<Transform>(panelObj, "Effs");
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
        GamePlayManager.Instance.panel = this;
        Reflash();
    }

    public void Reflash()
    {
        int count = GamePlayManager.Instance.GetRes().partons.Count - 1 - GamePlayManager.Instance.GetRes().partonIndex;
        if (count > 0)
        {
            boxCountText.text = GameManager.Instance.GetDescriptionByID(4001) + count;
        }
        else
        {
            boxCountText.text = GameManager.Instance.GetDescriptionByID(4002);
        }
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
}