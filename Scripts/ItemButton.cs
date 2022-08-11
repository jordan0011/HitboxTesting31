using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PowerItemBlock pib;
    public void OnPointerEnter(PointerEventData eventData)
    {
        pib.OnEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pib.OnExit();
    }
}
