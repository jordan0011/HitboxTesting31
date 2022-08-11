using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInventorySystem : MonoBehaviour
{
    public InventorySystem[] inventories = new InventorySystem[2];

    public AbilityStuff parent;

    public InventorySystem temp1 = null;
    public InventorySystem temp2 = null;

    public int pos1 = -1;
    public int pos2 = -1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i< inventories.Length; i++)
        {
            if (inventories[i] != null)
                inventories[i].SetParent(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMasteries(bool[] array)
    {
        parent.UpdatePoweritemMasteries(array);
    }

    public void LoadPowerItemInventories(int[] allplayerabilities, int[] myitemabilities, int[] myplayset)
    {

        inventories[0].LoadInventory( allplayerabilities);
        inventories[1].LoadInventory( myitemabilities);
        inventories[2].LoadInventory(myplayset);

       // inventories[0] = // allplayerabilities;
        //inventories[1] = //item abilities;
       // inventories[2] = // playset;
    }
    public void TransferObject(InventorySystem from, InventorySystem to, int a, int b)
    {
        if (a != -1 && b != -1)
        {
           // Debug.Log("from " + from + " pos: " + a + " to " + to + "pos: " + b);

            Object tempa = from.getItemAtPos(a);
            Object tempb = to.getItemAtPos(b);

            from.DestroyObject(a);
            to.DestroyObject(b);

            from.PasteObject(tempb, a);
            to.PasteObject(tempa, b);

            temp1 = null;
            temp2 = null;
            pos1 = -1;
            pos2 = -1;

            if(from == inventories[0] || to == inventories[0])
            {
                int[] myabilities = new int[inventories[0].getObjects().Length];

                for(int i=0; i< inventories[0].getObjects().Length; i++)
                {
                    if(inventories[0].getObjects()[i]!= null)
                    {
                        myabilities[i] = inventories[0].getObjects()[i].getAbility().getCode();
                    }
                    else
                    {
                        myabilities[i] = -1;
                    }
                }
                parent.UpdateAllPlayerAbilities(myabilities);
            }

            int[] poweritemabilities = new int[inventories[1].getObjects().Length];

            for (int i = 0; i < inventories[1].getObjects().Length; i++)
            {
                if (inventories[1].getObjects()[i] != null)
                {
                //Debug.Log(inventories[1].getObjects()[i].getAbility().getName());
                    poweritemabilities[i] = inventories[1].getObjects()[i].getAbility().getCode();
                }
                else
                {
                    poweritemabilities[i] = -1;
                }
            }
            parent.UpdatePowerItemAbilities(poweritemabilities);

            int[] poweritemplayset = new int[inventories[2].getObjects().Length];

            for (int i = 0; i < inventories[2].getObjects().Length; i++)
            {
                if (inventories[2].getObjects()[i] != null)
                {
                    poweritemplayset[i] = inventories[2].getObjects()[i].getAbility().getCode();
                }
                else
                {
                    poweritemplayset[i] = -1;
                }
            }
            parent.UpdatePoweritemPlayset(poweritemplayset);
        }
    }

    public void UpdatePowerItemAbilities(int[] array)
    {
        parent.UpdatePowerItemAbilities(array);
    }
    public void UpdatePowerItemPlayset(int[] array)
    {
        parent.UpdatePoweritemPlayset(array);
    }

    public void TransferObject()
    {
       // TransferObject(1, 5);
    }
    public void TransferObjectFrom(InventorySystem inventory, int a)
    {
       /* if(inventory== inventories[0])
        {
            Debug.Log("From AllPlayerAbilities: item " + a);
        }
        else
        {
            Debug.Log("From Spellbook: item " + a);
        }*/
            
        ComparePositionFrom(inventory, a);
    }
    public void TransferObjectTo(InventorySystem inventory, int a)
    {
       /* if (inventory == inventories[0])
        {
            Debug.Log("To AllPlayerAbilities: item " + a);
            
        }
        else
        {
            Debug.Log("To Spellbool: item " + a);
        }*/

        ComparePositionsTo(inventory, a);
    }

    public void ComparePositionFrom(InventorySystem newposition, int newpos)
    {
        temp1 = newposition;
        pos1 = newpos;

        if(temp1 != null && temp2 !=null && temp1 != temp2)
        {
            TransferObjectFull(temp1, temp2, pos1, pos2);
        }
    }
    public void ComparePositionsTo(InventorySystem newposition, int newpos)
    {
        if(newposition != temp1)
        {
            temp2 = newposition;
            pos2 = newpos;
        }

        if(newposition == null)
        {
            temp1 = null;
            pos1 = -1;
            pos2 = -1;
        }

        if (temp1 != null && temp2 != null && temp1 != temp2)
        {
            TransferObjectFull(temp1, temp2, pos1, pos2);
        }
    }
    public void TransferObjectFull(InventorySystem from, InventorySystem to, int posa,int posb)
    {
        if(from != null && to != null && from != to)
        {
            //Debug.Log(from + "  " + to);

            temp1 = null;
            temp2 = null;
            pos1 = -1;
            pos2 = -1;

            TransferObject(from, to, posa, posb);
        }
    }
}
