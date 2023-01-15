using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler
{
    public static Action onMouseOver;
    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseOver.Invoke();
    }
  
}
