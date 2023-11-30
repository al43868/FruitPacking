using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffManager : SerializedSingleTion<EffManager>
{
    public GamePlayPanel panel1;
    public List<GameObject> effs;
    public async void PlayEff(int effIndex, int panelIndex, Vector3 pos)
    {
        switch (panelIndex)
        {
            case 0:
                if (panel1 != null)
                {
                    GameObject go = GameObject.Instantiate(effs[effIndex], panel1.effsParent);
                    go.transform.position = pos;
                    await UniTask.Delay(1000);
                    GameObject.Destroy(go);
                }
                break;
            default:
                break;
        }
    }
}
