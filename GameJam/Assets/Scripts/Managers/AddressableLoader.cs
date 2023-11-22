using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

public class AddressableLoader
{
    public static T SyncLoad<T>(string objName) where T : Object
    {
        var op = Addressables.LoadAssetAsync<T>(objName);
        var go = op.WaitForCompletion();
        return go;
    }
    public static async UniTask<T> UniTaskLoad<T>(string objName)
    {
        var res = Addressables.LoadAssetAsync<T>(objName);
        return await res.Task;
    }
    public static async UniTask<GameObject> UniTaskIns(string objName)
    {
        var res = Addressables.InstantiateAsync(objName);
        return await res.Task;
    }
    public static async UniTask<GameObject> UniTaskIns(string objName, Transform parent)
    {
        var res = Addressables.InstantiateAsync(objName, parent);
        return await res.Task;
    }
}
