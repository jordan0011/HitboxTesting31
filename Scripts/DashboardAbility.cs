using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashboardAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public DashboardIteractive parent;
    public void OnPointerEnter(PointerEventData eventData)
    {
        parent.OnEnter(id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        parent.OnExit(id);
    }
}
