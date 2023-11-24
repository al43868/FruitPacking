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
   
    public DevelopMainPanel() : base(_type)
    {
        
    }
    public override void OnStart()
    {
        base.OnStart();
        UIHelper.GetComponentInChild<Button>(panelObj,
           "Play").onClick.AddListener(() => { AudioManager.Instance.PlayMusic(Music.Button1);
               Play();
           });
    }

    private void Play()
    {
        GameManager.Instance.LoadScene(1);
    }
}
