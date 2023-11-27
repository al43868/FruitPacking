using Coffee.UIEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2Int pos;
    public Image image;
    public Material choseMat,normalMat;
    public UIShiny shiny;
    public bool isSet;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //image.material = choseMat;
        GamePlayManager.Instance.SetMouseGrid(pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GamePlayManager.Instance.SetMouseGrid(pos,false);
    }

    public void Init()
    {
        shiny=this.gameObject.AddComponent<UIShiny>();
        image = GetComponent<Image>();
        shiny.effectPlayer.loop = true;
        shiny.Play(true);
    }
    internal void Choseing()
    {
        if (isSet)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.blue;
        }
    }
    internal void UnChoseing()
    {
        //image.material = choseMat;
        image.color = Color.white;
    }

    internal void Clear()
    {
        image.color = Color.white;
    }
}
