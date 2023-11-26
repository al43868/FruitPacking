using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEffSet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UIShiny shiny;
    public Animator animator;
    public ClickEff clickEff;
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    GamePlayManager.Instance.mouseEff = clickEff;
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("high", true);
        GamePlayManager.Instance.SetMouseEff(clickEff,true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("high", false);
        GamePlayManager.Instance.SetMouseEff(clickEff, false);
    }
}
