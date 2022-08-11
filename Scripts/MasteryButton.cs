using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MasteryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int number;
    private MasteryPanel parent;

    public void Set(MasteryPanel mp, int value)
    {
        number = value;
        parent = mp;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        parent.EnterMastery(number);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        parent.ExitMastery(number);
    }
}
