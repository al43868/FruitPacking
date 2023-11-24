using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioType
{
    public AudioClip clip;
    public AudioMixerGroup group;

    public Music music;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 1f)]
    public float pitch;
    /// <summary>
    ///  «∑Ò—≠ª∑
    /// </summary>
    public bool loop;
    public int ID;
    public AudioType()
    {
        ID = 11001;
        volume = 1;
        pitch = 1;
    }

}
