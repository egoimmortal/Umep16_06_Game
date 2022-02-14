﻿using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public delegate void UIEventProxy(GameObject gb);
    public event UIEventProxy OnClick;
    public event UIEventProxy OnMouseEnter;
    public event UIEventProxy OnMouseExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
            OnClick(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnMouseEnter != null)
            OnMouseEnter(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnMouseExit != null)
            OnMouseExit(this.gameObject);
    }
}
