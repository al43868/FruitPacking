using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SerializedSingleTion<AudioManager>
{
    public List<AudioType> audioTypes;
    [SerializeField]private AudioPool audioPool;
    [SerializeField]private Audio bkAudio;
    [SerializeField]private Music bg;
    public AudioMixer miuscMixer;
    protected void Start()
    {
        audioPool = new();
        audioPool.Init();
        miuscMixer.SetFloat("EffVolume", GameSaver.Instance.GetSetting().effValue);
        miuscMixer.SetFloat("MusicVolume", GameSaver.Instance.GetSetting().musicValue);
        if (GameSaver.Instance.GetSetting().musicOpen)
        {
            miuscMixer.SetFloat("MusicVolume", GameSaver.Instance.GetSetting().musicValue);
        }
        else
        {
            miuscMixer.SetFloat("MusicVolume", -80);
        }
        if (GameSaver.Instance.GetSetting().effOpen)
        {
            miuscMixer.SetFloat("EffVolume", GameSaver.Instance.GetSetting().effValue);
        }
        else
        {
            miuscMixer.SetFloat("EffVolume", -80);
        }
        bg = Music.Button1;
        PlayMusic(Music.BG1, true);
    }
    /// <summary>
    ///≤•∑≈“Ù¿÷
    /// </summary>
    /// <param name="name">√˚◊÷</param>
    /// <param name="isBack"> «∑ÒŒ™±≥æ∞“Ù</param>
    public void PlayMusic(Music name, bool isBack = false)
    {
        if (bg == name) return;
        foreach (var item in audioTypes)
        {
            if (item.music == name)
            {
                if (isBack)
                {
                    if (bkAudio != null)
                    {
                        Release(bkAudio);
                    }
                    bg = name;
                    bkAudio = audioPool.Get(item);
                }
                else
                {
                    audioPool.Get(item);
                }
            }
        }
    }

    internal void ChangeMusicOpen()
    {
        if (GameSaver.Instance.GetSetting().musicOpen)
        {
            GameSaver.Instance.GetSetting().musicOpen = false;
            GameSaver.Instance.SaveGameSettingByJson();
           miuscMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            GameSaver.Instance.GetSetting().musicOpen = true;
            GameSaver.Instance.SaveGameSettingByJson();
            miuscMixer.SetFloat("MusicVolume", GameSaver.Instance.GetSetting().musicValue);
        }
    }
    internal void ChangeMusicValue(float volume)
    {
        miuscMixer.SetFloat("MusicVolume", volume);
        GameSaver.Instance.GetSetting().musicValue = volume;
        GameSaver.Instance.SaveGameSettingByJson();
    }

    internal void ChangeSoundValue(float v)
    {
        miuscMixer.SetFloat("EffVolume", v);
        GameSaver.Instance.GetSetting().effValue = v;
        GameSaver.Instance.SaveGameSettingByJson();
    }

    internal void ChangeSoundOpen()
    {
        if (GameSaver.Instance.GetSetting().effOpen)
        {
            GameSaver.Instance.GetSetting().effOpen = false;
            GameSaver.Instance.SaveGameSettingByJson();
            miuscMixer.SetFloat("EffVolume", -80);
        }
        else
        {
            GameSaver.Instance.GetSetting().effOpen = true;
            GameSaver.Instance.SaveGameSettingByJson();
            miuscMixer.SetFloat("EffVolume", GameSaver.Instance.GetSetting().effValue);
        }
    }

    internal void Release(Audio audio)
    {
        audioPool.Release(audio);
    }

    //internal void PlayBG()
    //{
    //    PlayMusic(bg, true);
    //}
}
public enum Music
{
    Button1,
    BG1,
    BG2,
    BG3,
    GameEff1
}