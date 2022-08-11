using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerItemBlock : MonoBehaviour
{
    public GameObject image;
    public GameObject AbilitiesParent;

    public InventorySystem abilitysystem;
    public Text name1;

    private AbilityStuff parent;

    public int number = -1;

    public void Set(Poweritem poweritem, AbilityStuff myparent, int mynumber)
    {
        parent = myparent;
        number = mynumber;
        name1.text = poweritem.getName();

        if (image.transform.childCount > 0)
        {
            Destroy(image.transform.GetChild(0).gameObject);
        }


        GameObject temp = Instantiate(poweritem.getIcon());
        temp.transform.SetParent(image.transform);

        temp.transform.localPosition = new Vector3(0, 0, 0);

        abilitysystem = AbilitiesParent.GetComponent<InventorySystem>();
    }

    public void OnClick()
    {
        if (number > 0)
        {
            parent.ItemClicked(number);
        }
        else
        {
            parent.MainItemClicked();
        }
    }

    public void OnEnter()
    {
        parent.OnEnter(number);
    }
    public void OnExit()
    {
        parent.OnExit(number);
    }
    public void UpdatePowerItemBlock(int[] abilityarray)
    {
        abilitysystem.LoadInventory(abilityarray);
    }
    public int getNumber()
    {
        return number;
    }
}
