using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseIn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject des;
    public TMP_Text nameText, destext;
    public int id;
    private void Start()
    {
        des.SetActive(false);
        nameText.text=GameManager.Instance.GetNameByID(id);
        destext.text=GameManager.Instance.GetDescriptionByID(id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        des.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        des.SetActive(false);

    }
}