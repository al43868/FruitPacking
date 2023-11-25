using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevelopMainPanel : BasePanel
{
    private static readonly string _name = "DevelopPanel";
    private static readonly string _path = "DevelopPanel";
    private static readonly UIType _type = new(_name, _path);
    private TMP_Text coinText;
    private int panelIndex;
    private List<Transform> panels;
    private List<TMP_Text> clickLevels, clickValues;
    public DevelopMainPanel() : base(_type)
    {
        
    }
    public override void OnStart()
    {
        base.OnStart();
        UIHelper.GetComponentInChild<Button>(panelObj,
           "Play").onClick.AddListener(() => 
            { 
               AudioManager.Instance.PlayMusic(Music.Button1);
               Play();
           });
        coinText = UIHelper.GetComponentInChild<TMP_Text>(panelObj, "CoinText");
        UIHelper.GetComponentInChild<Button>(panelObj, "LeftButton").onClick.AddListener(
            () => {
                AudioManager.Instance.PlayMusic(Music.Button1);
                ChangePanel(-1);
            });
        UIHelper.GetComponentInChild<Button>(panelObj, "RightButton").onClick.AddListener(
            () => {
                AudioManager.Instance.PlayMusic(Music.Button1);
                ChangePanel(1);
            });
        panelIndex = 1;
        Transform panelsTr = UIHelper.GetComponentInChild<Transform>(panelObj, "Panels");
        panels = new();
        for (int i = 0; i < panelsTr.childCount; i++)
        {
            panels.Add(panelsTr.GetChild(i));
        }

        //¿Æ¼¼panel
        clickLevels = new();
        clickValues= new();
        Transform clickEffs = UIHelper.GetComponentInChild<Transform>(panelObj, "ClickEffLevelUP");
        for (int i = 0; i < clickEffs.childCount; i++)
        {
            Transform clickUp = clickEffs.GetChild(i);
            int j = i;
            clickUp.GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayMusic(Music.Button1);
                LevelUP(j);
            });
            clickLevels.Add(clickUp.GetChild(1).GetComponent<TMP_Text>());
            clickValues.Add(clickUp.GetChild(2).GetComponent<TMP_Text>());
        }
        Reflash();
    }

    private void LevelUP(int j)
    {
        int i = 100 + j;
        int needCoin = GameManager.Instance.GetDataList().levelUps[i][GameSaver.Instance.GetData().levelUPs[i]];
        if (needCoin> GameSaver.Instance.GetData().coin)
        {
            GameManager.Instance.Log(2005);
        }
        else
        {
            if (GameSaver.Instance.GetData().levelUPs[i] >= 5)
            {
                GameManager.Instance.Log(2006);
            }
            else
            {
                GameSaver.Instance.GetData().coin -= needCoin;
                GameSaver.Instance.GetData().levelUPs[i]++;
                Reflash();
            }
        }
    }

    private void ChangePanel(int v)
    {
        //todo need
        panels[panelIndex].gameObject.SetActive(false);
        int allPanelCount = 1;
        if (panelIndex+v > allPanelCount)
        {
            panelIndex = 0;
        }
        else if (panelIndex + v < 0)
        {
            panelIndex = allPanelCount;
        }
        else
        {
            panelIndex += v;
        }
        panels[panelIndex].gameObject.SetActive(true);
        Reflash();

    }

    public void Reflash()
    {
        coinText.text = GameSaver.Instance.GetData().coin.ToString();
        switch (panelIndex)
        {
            case 1:
                for (int i = 0; i < clickLevels.Count; i++)
                {
                    int index = 100 + i;
                    clickLevels[i].text = GameSaver.Instance.GetData().levelUPs[index].ToString();
                    clickValues[i].text = GameManager.Instance.GetDataList().
                       levelUps[index][GameSaver.Instance.GetData().levelUPs[index]].ToString();
                }
                break;
            default:
                break;
        }
    }
    private void Play()
    {
        GameManager.Instance.LoadScene(1);
    }
}
