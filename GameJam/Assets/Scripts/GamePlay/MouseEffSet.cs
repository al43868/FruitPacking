using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEffSet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ClickEff clickEff;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GamePlayManager.Instance.SetMouseClickEff(clickEff,true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GamePlayManager.Instance.SetMouseClickEff(clickEff,false);
    }
}
