using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEx : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public float needTime;
    public float currentTime;
    public bool isClick;
    public Slider slider;
    public Action fire;
    private void Start()
    {
        slider.value = 0;
    }
    public async void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        AddTime().Forget();
       
        var time = UniTask.WaitUntil(() => currentTime >= needTime);
        var noClick= UniTask.WaitUntil(() => !isClick);
        await UniTask.WhenAny(time, noClick);
        if (isClick)
        {
            currentTime = 0;
            slider.value = 0;
            isClick = false;
            fire?.Invoke();
        }
        else
        {
            currentTime = 0;
            slider.value = 0;
        }
    }

    private async UniTaskVoid AddTime()
    {
        currentTime = 0;
        while (currentTime < needTime && isClick)
        {
            currentTime += Time.deltaTime;
            slider.value = currentTime / needTime;
            await UniTask.NextFrame();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick=false;
    }
}
