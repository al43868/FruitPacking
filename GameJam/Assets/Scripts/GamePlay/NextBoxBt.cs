using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextBoxBt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GamePlayManager.Instance.nextBox = true;
        animator.SetBool("high", true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GamePlayManager.Instance.nextBox = false;
        animator.SetBool("high", false);

    }
   
}
