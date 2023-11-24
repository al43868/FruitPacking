using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameRoot : SerializedSingleTion<GameRoot>
{
    private UIManager uiManager;
    public SceneController sceneController;
    public GameObject log;
    private TMP_Text logText;
    [SerializeField]private bool isLoging;
    protected override void Awake()
    {
       base.Awake();
        if (uiManager == null)
            uiManager = new UIManager();
        uiManager.canvas = UIHelper.FindCanvas();
        if (sceneController == null)
            sceneController = new SceneController();
        DontDestroyOnLoad(gameObject);
        //InitLog();
    }

    private void InitLog()
    {
        logText = log.GetComponentInChildren<TMP_Text>();
        log.SetActive(false);
    }

    #region panel
    public void ShowCutScenePanel()
    {
        CutScenePanel cutScenePanel = new();
        uiManager.Push(cutScenePanel);
    }
    public void ShowGameStartPanel()
    {
        GameStartPanel gameStartPanel = new ();
        uiManager.Push(gameStartPanel);
    }
    public void ShowDevelopMainPanel()
    {
        DevelopMainPanel developMainPanel = new();
        uiManager.Push(developMainPanel);
    }
    public void ShowGamePlayPanel()
    {
        GamePlayPanel gamePlayPanel = new();
        uiManager.Push(gamePlayPanel);
    }
    #endregion
    #region ������

    internal async void Log(string text)
    {
        if (isLoging) return;
        log.SetActive(true);
        log.GetComponent<RectTransform>().SetAsLastSibling();
        logText.text=text;
        isLoging = true;
        await UniTask.Delay(500);
        isLoging = false;
        log.SetActive(false);
    }


    /// <summary>
    /// ���panel����һ������panel������pop
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    internal void PopOnePanel()
    {
        if (uiManager.panelStack.Count > 1)
        {
            uiManager.Pop();
        }
    }
    internal void Pop()
    {
        uiManager.Pop();
    }
    [Button]
    public void ShowUiManager()
    {
        print(uiManager.panelStack.Count);
        foreach (var item in uiManager.panelStack)
        {
            print(item);
        }
        
        print(uiManager.panelDictionary.Count);
        foreach (var item in uiManager.panelDictionary)
        {
            print(item.Key+":"+ item.Value);
        }
    }
    internal void Clear()
    {
        uiManager.Clear();
    }

    #endregion
}
