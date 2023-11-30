using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemsSet : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GamePlayManager.Instance.canSet = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GamePlayManager.Instance.canSet = false;
    }
}
