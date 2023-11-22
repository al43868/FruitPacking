using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseIn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public PanelRound round;


    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    
}
public enum PanelRound
{
    panel,
    grid,
}