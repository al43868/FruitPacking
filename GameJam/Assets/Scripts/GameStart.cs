using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        GameRoot.Instance.ShowGameStartPanel();
        Debug.Log(1);
        GameRoot.Instance.ShowCutScenePanel();
        await UniTask.Delay(1000);
        GameRoot.Instance.PopOnePanel();
        Debug.Log(2);

    }
}
