using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private int slot;
    private InventorySystem inventory;
    private GameObject image;

    public void Set(int i, InventorySystem myinventory)
    {
        slot = i;
        inventory = myinventory;
    }
    public void UpdateSlotImage(GameObject myimage)
    {

        if (image)
            Destroy(image);

        GameObject temp = Instantiate(myimage);
        temp.transform.SetParent(transform);
        temp.transform.SetAsFirstSibling();

        image = temp;

        temp.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventory.SlotEntered(slot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.SlotExit(slot);
    }
    public void OnPointerClick()
    {
        inventory.SlotClicked(slot);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        inventory.SlotClicked(slot);
    }
}
