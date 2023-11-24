using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : BasePool<Audio>
{
    public void Init()
    {
        prefab = AddressableLoader.SyncLoad<GameObject>("Audio").GetComponent<Audio>();
        Initialize();
    }
    protected override Audio OnCreatePoolItem()
    {
        return GameObject.Instantiate(prefab, AudioManager.Instance.gameObject.transform);
    }
    public Audio Get(AudioType audioType)
    {
        var audio = base.Get();
        audio.Play(audioType);
        return audio;
    }
}
