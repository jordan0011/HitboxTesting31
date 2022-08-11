using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Object[] myObjects;
    public int capacity = 16;
    public GameObject SingleSlot;

    private SlotObject[] buttons;

    public int currentslot = -1;
    public int previousslot = -1;

    public bool OnButton = false;

    private GeneralInventorySystem parent;

    public bool issmall = false;

    public AbilityStuffInfoPanel infopanel;
    public Object[] getObjects()
    {
        return myObjects;
    }

    public void LoadInventory(int[] myabilities)
    {
        int[] myabilities1 = { -1, -1, 1, 1};

        for(int i=0; i< gameObject.transform.childCount; i++)
        {
            //Debug.Log(gameObject.transform.GetChild(i).gameObject);
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        capacity = myabilities.Length;

        myObjects = new Object[capacity];
        buttons = new SlotObject[capacity];

        for (int i = 0; i < myObjects.Length; i++)
        {
            GameObject temp = Instantiate(SingleSlot);
            int a = i;
            temp.transform.SetParent(transform);
            temp.GetComponent<SlotObject>().Set(a, this);
            temp.GetComponent<SlotObject>().UpdateSlotImage(AbilityDatabase.Instance.EmptyIcon);

            buttons[a] = temp.GetComponent<SlotObject>();
            if (issmall)
            {
                //buttons[a].gameObject.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
               // Debug.Log(buttons[a].gameObject.transform.GetChild(0).name);
                for(int h=0; h< buttons[a].gameObject.transform.childCount; h++)
                {
                    buttons[a].gameObject.transform.GetChild(h).transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
                }
            }
        }

        for (int i = 0; i < myabilities.Length; i++)
        {
            int a = myabilities[i];
            if(a > -1)
            {
                Object temp = new Object(AbilityDatabase.Instance.AllAbilities[a]);
                myObjects[i] = temp;
            }
        }

        RefreshSlots();
    }

    public void SetParent(GeneralInventorySystem myparent)
    {
        parent = myparent;
    }
    // Start is called before the first frame update
    void Start()
    {
        /*myObjects = new Object[capacity];
        buttons = new SlotObject[capacity];

        for (int i = 0; i < myObjects.Length; i++)
        {
            GameObject temp = Instantiate(SingleSlot);
            int a = i;
            temp.transform.SetParent(transform);
            temp.GetComponent<SlotObject>().Set(a, this);
            temp.GetComponent<SlotObject>().UpdateSlotImage(AbilityDatabase.Instance.EmptyIcon);

            buttons[a] = temp.GetComponent<SlotObject>();
        }

        RefreshSlots();*/


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(currentslot != -1 && previousslot != -1 && currentslot != previousslot)
            {
                SwitchPlaceObject(currentslot, previousslot);
            }
            if(currentslot != -1)
            {
                parent.TransferObjectTo(this, currentslot);
            }
            previousslot = -1;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (OnButton)
            {
                previousslot = currentslot;
            }
        }
    }

    public void RefreshSlots()
    {
        for(int i=0; i< myObjects.Length; i++)
        {
            if (myObjects[i] != null)
            {
                //Debug.Log(myObjects[i].getAbility().getName() + " + "+i);
                buttons[i].UpdateSlotImage(myObjects[i].getAbility().getIcon());
            }
            else
            {
                //Debug.Log(i);
                buttons[i].UpdateSlotImage(AbilityDatabase.Instance.EmptyIcon);
            }
            if (issmall)
            {
                for (int h = 0; h < buttons[i].gameObject.transform.childCount; h++)
                {
                    buttons[i].gameObject.transform.GetChild(h).transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
                }
            }
        }
    }

    public void AddObject(int n)
    {
        bool empty = true;
        for(int i=0; i< myObjects.Length; i++)
        {
            if(empty && myObjects[i] == null)
            {
                Object temp = new Object(AbilityDatabase.Instance.AllAbilities[n]);
                myObjects[i] = temp;
                empty = false;
            } 
        }
        RefreshSlots();
    }
    public void PasteObject(Object myobject, int pos)
    {
        if(myObjects[pos] == null)
        {
            myObjects[pos] = myobject;
        }
        RefreshSlots();
    }
    public void DestroyObject(int n)
    {
        myObjects[n] = null;

        RefreshSlots();
    }
    public void SwitchPlaceObject(int b, int a)
    {
        if (myObjects[a] != null)
        {
            Object temp = myObjects[a];
            myObjects[a] = myObjects[b];
            myObjects[b] = temp;
        }

        RefreshSlots();
    }
    public Object getItemAtPos(int a)
    {
        Object temp = myObjects[a];
        return temp;
    }

    public void SlotClicked(int n)
    {
        currentslot = n;
    }
    public void SlotEntered(int n)
    {
        OnButton = true;

        currentslot = n;

        if (myObjects[n] != null)
        {
            Ability temp = AbilityDatabase.Instance.AllAbilities[myObjects[n].getAbility().getCode()];
            if(temp != null)
            {
                infopanel.gameObject.SetActive(true);
                infopanel.SetUp(temp.getName(), temp.getDescription(), "-", temp.getIcon());
            }
        }
    }
    public void SlotExit(int n)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (previousslot == n)
            {
                parent.TransferObjectFrom(this, previousslot);
            }
        }
        
        OnButton = false;
        currentslot = -1;

        infopanel.gameObject.SetActive(false);
    }
    public void SlotDown(int n)
    {

    }
}
public class Object
{
    private Ability ability;

    public Object(Ability myability)
    {
        ability = myability;
    }
    public Ability getAbility()
    {
        return ability;
    }
}
