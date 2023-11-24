using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ�趨
/// </summary>
[Serializable]
public class GameSetting
{
    /// <summary>
    /// ����
    /// </summary>
    public string language;
    /// <summary>
    /// ��Ļ��
    /// </summary>
    public int width;
    /// <summary>
    /// ��Ļ��
    /// </summary>
    public int height;
    /// <summary>
    /// �Ƿ�ȫ��
    /// </summary>
    public bool fullScreen;
    /// <summary>
    /// ����
    /// </summary>
    public float musicValue;
    /// <summary>
    /// �Ƿ�ȫ��
    /// </summary>
    public bool musicOpen;
    /// <summary>
    /// ��Ч
    /// </summary>
    public float effValue;
    /// <summary>
    /// �Ƿ�ȫ��
    /// </summary>
    public bool effOpen;
    /// <summary>
    /// �Զ�����
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
