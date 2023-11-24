using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 游戏开始主菜单
/// </summary>
public class GameStartPanel : BasePanel
{
    private static readonly string _name = "GameStartPanel";
    private static readonly string _path = "GameStartPanel";
    private static readonly UIType _type = new(_name, _path);

    public GameStartPanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        UIHelper.GetComponentInChild<Button>(panelObj, "QuickLoad").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayMusic(Music.Button1);
            OpenSavePanel();
        });
        //UIHelper.GetComponentInChild<Button>(panelObj, "QuickLoad").onClick.AddListener(() =>
        //{
        //    AudioManager.Instance.PlayMusic(Music.Button1);
        //    QuickLoad();
        //});
        //UIHelper.GetComponentInChild<Button>(panelObj, "Setting").onClick.AddListener(() =>
        //{
        //    AudioManager.Instance.PlayMusic(Music.Button1);
        //    Setting();
        //});
        //UIHelper.GetComponentInChild<Button>(panelObj, "Exit").onClick.AddListener(() =>
        //{
        //    AudioManager.Instance.PlayMusic(Music.Button1);
        //    Exit();
        //});

        //UIHelper.GetComponentInChild<Button>(panelObj, "QuickFight").onClick.AddListener(() =>
        //{
        //    AudioManager.Instance.PlayMusic(Music.Button1); GameManager.Instance.QuickFight(); });
        
        TMP_Dropdown laDropdown = UIHelper.GetComponentInChild<TMP_Dropdown>(panelObj,
            "LaDropdown");
        laDropdown.value = GameSaver.Instance.GetLanguageIndex();
        laDropdown.RefreshShownValue();
        laDropdown.onValueChanged.AddListener
           ((x) => { AudioManager.Instance.PlayMusic(Music.Button1); ChoseLa(x); });
    }

    private void ChoseLa(int x)
    {
        GameSaver.Instance.ChangeLanguage(x);
        //GameRoot.Instance.ShowCutScenePanel();
        //GameRoot.Instance.PopOnePanel();
    }
    private void Exit()
    {
        Application.Quit();
    }
    private void Setting()
    {
        //GameRoot.Instance.ShowGameSettingPanel();
    }

    //private void QuickLoad()
    //{
    //    GameSaver.Instance.QuickLoad();
    //}

    private void OpenSavePanel()
    {
        //GameRoot.Instance.ShowChoseSavePanel();
        //todo 真正ui
        GameManager.Instance.QuickFight();
    }
}
