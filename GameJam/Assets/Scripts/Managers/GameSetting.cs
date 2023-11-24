using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏设定
/// </summary>
[Serializable]
public class GameSetting
{
    /// <summary>
    /// 语言
    /// </summary>
    public string language;
    /// <summary>
    /// 屏幕宽
    /// </summary>
    public int width;
    /// <summary>
    /// 屏幕高
    /// </summary>
    public int height;
    /// <summary>
    /// 是否全屏
    /// </summary>
    public bool fullScreen;
    /// <summary>
    /// 音乐
    /// </summary>
    public float musicValue;
    /// <summary>
    /// 是否全屏
    /// </summary>
    public bool musicOpen;
    /// <summary>
    /// 音效
    /// </summary>
    public float effValue;
    /// <summary>
    /// 是否全屏
    /// </summary>
    public bool effOpen;
    /// <summary>
    /// 自动保存
    /// </summary>
    public bool autoSave;
    public GameSetting()
    {
        language = "Chinese (Simplified) (zh)";
        width = 1920;
        height = 1080;
        fullScreen = true;
        musicValue = 0;
        musicOpen = true;
        effValue = 0;
        effOpen = true;
        autoSave = false;
    }
    public void Remake()
    {
        language = "Chinese (Simplified) (zh)";
    }
    public void ChangeScreen(int windth, int height)
    {
        this.height = height;
        this.width = windth;
        Screen.SetResolution(width, height, fullScreen);
    }
    public void ChangeFullScreen(bool full)
    {
        fullScreen = full;
        Screen.fullScreen = fullScreen;
    }
}
