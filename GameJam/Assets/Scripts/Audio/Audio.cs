using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    internal async void Play(AudioType audioType)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioType.clip;
        //audioSource.name = audioType.name;
        audioSource.loop = audioType.loop;
        audioSource.volume = audioType.volume;
        audioSource.pitch = audioType.pitch;
        if (audioType.group != null)
        {
            audioSource.outputAudioMixerGroup = audioType.group;
        }

        if (audioType.loop)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(audioSource.clip);
            await UniTask.Delay((int)(audioSource.clip.length * 1000));
            AudioManager.Instance.Release(this);
        }
    }
}
