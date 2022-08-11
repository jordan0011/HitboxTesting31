using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityStuff : MonoBehaviour
{
    public int capacity = 20;
    public GameObject PoweritemBlock;
    public GameObject MainPowerItem;

    public GameObject PowerItems;

    public Poweritem[] poweritems;

    public Poweritem mainpoweritem;


    public GameObject[] powerItemsPanel = new GameObject[10];

    public PowerItemPanel[] poweritemspanels = new PowerItemPanel[10];

    public int[] allplayerabilities = new int[16];

    public int poweritemopened = -1;

    public PowerItemBlock[] poweritemblocks;

    public PowerItemBlock maimpoweritemblock;

    public AbilityDashboard dashboard;

    public GameObject InfoPanel;
    public AbilityStuffInfoPanel info;
    // Start is called before the first frame update
    void Start()
    {
        poweritems = new Poweritem[capacity];
        poweritemblocks = new PowerItemBlock[capacity];
    }
    private void Awake()
    {
        for (int i = 0; i < allplayerabilities.Length; i++)
        {
            allplayerabilities[i] = -1;
        }
        for (int i = 0; i < powerItemsPanel.Length; i++)
        {
            if (powerItemsPanel[i] != null)
            {
                poweritemspanels[i] = powerItemsPanel[i].GetComponent<PowerItemPanel>();
            }
        }
        info = InfoPanel.GetComponent<AbilityStuffInfoPanel>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadItems()
    {
        //???
    }
    public void OnEnter(int n)
    {
        InfoPanel.SetActive(true);
        info.SetUp(poweritems[n].getName(), "not yet available", "-", poweritems[n].getIcon());
    }
    public void OnExit(int n)
    {
        InfoPanel.SetActive(false);
    }
    public void MainItemClicked()
    {
        int n = 0;
        poweritemopened = n;
        if (poweritems[n]!= null)
        {
            powerItemsPanel[poweritems[n].GetId()].SetActive(true);

            int[] array1 = new int[poweritems[n].getAbilities().Length];
            for (int i = 0; i < poweritems[n].getAbilities().Length; i++)
            {
                int a;
                a = poweritems[n].getAbilities()[i];
                array1.SetValue(a, i);
            }
            int[] array2 = new int[poweritems[n].getPlayset().Length];
            for (int i = 0; i < poweritems[n].getPlayset().Length; i++)
            {
                int a = poweritems[n].getPlayset()[i];
                array2.SetValue(a, i);
            }
            bool[] array3 = new bool[poweritems[n].getMasteries().Length];
            for(int i =0; i < poweritems[n].getMasteries().Length; i++)
            {
                bool a = poweritems[n].getMasteries()[i];
                array3.SetValue(a, i);
            }
            poweritemspanels[poweritems[n].GetId()].LoadPowerItemInventories(allplayerabilities, array1, array2, array3);
            //poweritemspanels[0].LoadPowerItemInventories();
        }
    }
    public void ItemClicked(int n)
    {
        poweritemopened = n;
        if (poweritems[n]!= null)
        {
            powerItemsPanel[poweritems[n].GetId()].SetActive(true);

            int[] array1 = new int[poweritems[n].getAbilities().Length];
            for(int i=0; i< poweritems[n].getAbilities().Length; i++)
            {
                int a;
                a = poweritems[n].getAbilities()[i];
                array1.SetValue(a, i);
            }
            int[] array2 = new int[poweritems[n].getPlayset().Length];
            for(int i=0; i< poweritems[n].getPlayset().Length; i++)
            {
                int a = poweritems[n].getPlayset()[i];
                array2.SetValue(a, i);
            }
            bool[] array3 = new bool[poweritems[n].getMasteries().Length];
            for(int i=0; i< poweritems[n].getMasteries().Length; i++)
            {
                bool a = poweritems[n].getMasteries()[i];
                array3.SetValue(a, i);
            }
            poweritemspanels[poweritems[n].GetId()].LoadPowerItemInventories(allplayerabilities, array1, array2, array3);
            //poweritemspanels[0].LoadPowerItemInventories();
        }
    }

    public void UpdateAllPlayerAbilities(int[] myabilities)
    {
        for (int i = 0; i < myabilities.Length; i++)
        {
            allplayerabilities[i] = myabilities[i];
        }
    }

    public void UpdatePowerItemAbilities(int[] myabilities)
    {
        if(poweritemopened > -1)
        {
            for(int i=0; i< myabilities.Length; i++)
            {
                poweritems[poweritemopened].getAbilities().SetValue(myabilities[i], i);
            }
        }
    }
    public void UpdatePoweritemPlayset(int[] myplayset)
    {
        if(poweritemopened > -1)
        {
            for(int i=0; i< myplayset.Length; i++)
            {
                //poweritems[poweritemopened].setPlaySet(array);
                poweritems[poweritemopened].getPlayset().SetValue(myplayset[i], i);
            }

            if (poweritemopened > 0)
                poweritemblocks[poweritemopened].UpdatePowerItemBlock(myplayset);
            else
            {
                maimpoweritemblock.UpdatePowerItemBlock(myplayset);
                dashboard.LoadDashboard(myplayset);
            }
        }
    }
    public void UpdatePoweritemMasteries(bool[] mymasteries)
    {
        if(poweritemopened > -1)
        {
            for(int i=0; i< mymasteries.Length; i++)
            {
                poweritems[poweritemopened].getMasteries().SetValue(mymasteries[i], i);
            }
        }
    }

    public void AddMainPowerItem(int a)
    {
        mainpoweritem = PowerItemDatabase.Instance.AllPowerItems[a];

        GameObject temp = Instantiate(PoweritemBlock);
        temp.GetComponent<PowerItemBlock>().Set(mainpoweritem, this, 0);
        temp.transform.SetParent(MainPowerItem.transform);
        //temp.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void MakeMainItem(int a)
    {
        if(poweritems[0] == null)
        {
            Poweritem temp = poweritems[a];
            int[] array1 = new int[temp.getAbilities().Length];
            for (int h = 0; h < temp.getAbilities().Length; h++)
            {
                array1.SetValue(temp.getAbilities()[h], h);
            }
            int[] array2 = new int[temp.getPlayset().Length];
            for (int h = 0; h < temp.getPlayset().Length; h++)
            {
                array2.SetValue(temp.getPlayset()[h], h);
            }
            bool[] array3 = new bool[temp.getMasteries().Length];
            for(int h= 0; h< temp.getMasteries().Length; h++)
            {
                array3.SetValue(temp.getMasteries()[h], h);
            }
            Poweritem temp1 = new Poweritem(temp.GetId(), temp.getIcon(), temp.getName(), temp.GetTheType(), temp.GetAttack(), temp.GetAbility1(), temp.GetAbility2(), array1, array2, array3.Length);
            temp1.setMasteries(array3);
            poweritems[0] = temp1;

            if (MainPowerItem.transform.childCount>0)
            {
                Destroy(MainPowerItem.transform.GetChild(0).gameObject);
            }

            GameObject tempbutton = Instantiate(PoweritemBlock);
            tempbutton.GetComponent<PowerItemBlock>().Set(poweritems[0], this, 0);
            maimpoweritemblock = tempbutton.GetComponent<PowerItemBlock>();
            tempbutton.transform.SetParent(MainPowerItem.transform);
            maimpoweritemblock.UpdatePowerItemBlock(poweritems[0].getPlayset());

            dashboard.LoadDashboard(poweritems[0].getPlayset());

            DeletePowerItem(a);

        }
        else
        {
            // krata to main                  // poweritems[0];
            int[] array1 = new int[poweritems[0].getAbilities().Length];
            for (int h = 0; h < poweritems[0].getAbilities().Length; h++)
            {
                array1.SetValue(poweritems[0].getAbilities()[h], h);
            }
            int[] array2 = new int[poweritems[0].getPlayset().Length];
            for (int h = 0; h < poweritems[0].getPlayset().Length; h++)
            {
                array2.SetValue(poweritems[0].getPlayset()[h], h);
            }
            bool[] array31 = new bool[poweritems[0].getMasteries().Length];
            for( int h = 0; h< poweritems[0].getMasteries().Length; h++)
            {
                array31.SetValue(poweritems[0].getMasteries()[h], h);
            }
            Poweritem maintemp0 = new Poweritem(poweritems[0].GetId(), poweritems[0].getIcon(), poweritems[0].getName(), poweritems[0].GetTheType(), poweritems[0].GetAttack(), poweritems[0].GetAbility1(), poweritems[0].GetAbility2(), array1, array2, array31.Length);
            maintemp0.setMasteries(array31);
            //adeiase main

            Destroy(maimpoweritemblock.gameObject);
            poweritems[0] = null;

            // vale sti 8esi tou main to a

            Poweritem temp = poweritems[a];
            int[] array3 = new int[temp.getAbilities().Length];
            for (int h = 0; h < temp.getAbilities().Length; h++)
            {
                array3.SetValue(temp.getAbilities()[h], h);
            }
            int[] array4 = new int[temp.getPlayset().Length];
            for (int h = 0; h < temp.getPlayset().Length; h++)
            {
                array4.SetValue(temp.getPlayset()[h], h);
            }

            bool[] array34 = new bool[temp.getMasteries().Length];
            for(int h =0; h< temp.getMasteries().Length; h++)
            {
                array34.SetValue(temp.getMasteries()[h], h);
            }
            Poweritem temp1 = new Poweritem(temp.GetId(), temp.getIcon(), temp.getName(), temp.GetTheType(), temp.GetAttack(), temp.GetAbility1(), temp.GetAbility2(), array3, array4, array34.Length);
            temp1.setMasteries(array34);
            poweritems[0] = temp1;

            if (MainPowerItem.transform.childCount > 0)
            {
                Destroy(MainPowerItem.transform.GetChild(0).gameObject);
            }

            GameObject tempbutton = Instantiate(PoweritemBlock);
            tempbutton.GetComponent<PowerItemBlock>().Set(poweritems[0], this, 0);
            maimpoweritemblock = tempbutton.GetComponent<PowerItemBlock>();
            tempbutton.transform.SetParent(MainPowerItem.transform);

            maimpoweritemblock.UpdatePowerItemBlock(poweritems[0].getPlayset());

            dashboard.LoadDashboard(poweritems[0].getPlayset());

            // adease a 
            DeletePowerItem(a);

            // vale sti 8esi tou a to main pou kratises

            bool empty = true;

            for (int i = 1; i < poweritems.Length; i++)
            {
                if (empty && poweritems[i] == null)
                {
                    Poweritem tempA = maintemp0;
                    int[] array5 = new int[tempA.getAbilities().Length];
                    for (int h = 0; h < tempA.getAbilities().Length; h++)
                    {
                        array5.SetValue(tempA.getAbilities()[h], h);
                    }
                    int[] array6 = new int[tempA.getPlayset().Length];
                    for (int h = 0; h < tempA.getPlayset().Length; h++)
                    {
                        array6.SetValue(tempA.getPlayset()[h], h);
                    }
                    bool[] array7 = new bool[tempA.getMasteries().Length];
                    for(int h =0; h< tempA.getMasteries().Length; h++)
                    {
                        array7.SetValue(tempA.getMasteries()[h], h);
                    }
                    Poweritem temp2 = new Poweritem(tempA.GetId(), tempA.getIcon(), tempA.getName(), tempA.GetTheType(), tempA.GetAttack(), tempA.GetAbility1(), tempA.GetAbility2(), array5, array6, array7.Length);
                    temp2.setMasteries(array7);
                    poweritems[i] = temp2;
                    int b = i;
                    GameObject tempblock = Instantiate(PoweritemBlock);
                    tempblock.GetComponent<PowerItemBlock>().Set(poweritems[i], this, b);
                    poweritemblocks[i] = tempblock.GetComponent<PowerItemBlock>();
                    tempblock.transform.SetParent(PowerItems.transform);
                    poweritemblocks[b].UpdatePowerItemBlock(poweritems[b].getPlayset());
                    //maimpoweritemblock.UpdatePowerItemBlock(temp2.getPlayset());

                    empty = false;
                }
            }
        }
    }
    public void DeletePowerItem(int a)
    {
        if(poweritemblocks[a].gameObject != null)
            Destroy(poweritemblocks[a].gameObject);
        poweritemblocks[a] = null;
        for(int i=0; i< poweritemblocks.Length; i++)
        {
            if(a+i+1 < poweritemblocks.Length)
            if(poweritemblocks[a+i+1] != null)
            {
                PowerItemBlock temp = poweritemblocks[a + i + 1];
                
                poweritemblocks[a+i] = temp;
                poweritemblocks[a+i].Set(poweritems[a+i+1], this, a);
                poweritemblocks[a+i].UpdatePowerItemBlock(poweritems[a+i + 1].getPlayset());

                    poweritemblocks[a +i+ 1] = null;
            }
        }
        poweritems[a] = null;
        for(int i=0; i < poweritems.Length; i++)
        {
            if(a+i+1 < poweritems.Length)
            if(poweritems[a+i+1]!= null)
            {
                Poweritem temp = poweritems[a+i+1];
                int[] array1 = new int[temp.getAbilities().Length];
                for (int h = 0; h < temp.getAbilities().Length; h++)
                {
                    array1.SetValue(temp.getAbilities()[h], h);
                }
                int[] array2 = new int[temp.getPlayset().Length];
                for (int h = 0; h < temp.getPlayset().Length; h++)
                {
                    array2.SetValue(temp.getPlayset()[h], h);
                }
                    bool[] array3 = new bool[temp.getMasteries().Length];
                    for(int h=0; h< temp.getMasteries().Length; h++)
                    {
                        array3.SetValue(temp.getMasteries()[h], h);
                    }
                Poweritem temp1 = new Poweritem(temp.GetId(), temp.getIcon(), temp.getName(), temp.GetTheType(), temp.GetAttack(), temp.GetAbility1(), temp.GetAbility2(), array1, array2, array3.Length);
                    temp1.setMasteries(array3);
                    poweritems[a+i] =  temp1;
                poweritems[a +i+ 1] = null;
            }
        }
    }

    public void AddPowerItem(int a)
    {
        bool empty = true;

        for(int i=1; i< poweritems.Length; i++)
        {
            if(empty && poweritems[i] == null)
            {
                Poweritem temp1 = PowerItemDatabase.Instance.AllPowerItems[a];
                int[] array1 = new int[temp1.getAbilities().Length];
                for (int h = 0; h < temp1.getAbilities().Length; h++)
                {
                    array1.SetValue(temp1.getAbilities()[h], h);
                }
                int[] array2 = new int[temp1.getPlayset().Length];
                for (int h = 0; h < temp1.getPlayset().Length; h++)
                {
                    array2.SetValue(temp1.getPlayset()[h], h);
                }
                bool[] array3 = new bool[temp1.getMasteries().Length];
                for(int h =0; h< temp1.getMasteries().Length; h++)
                {
                    array3.SetValue(temp1.getMasteries()[h], h);
                }
                Poweritem temp2 = new Poweritem(temp1.GetId(), temp1.getIcon(), temp1.getName(), temp1.GetTheType(), temp1.GetAttack(), temp1.GetAbility1(), temp1.GetAbility2(), array1, array2, array3.Length);
                temp2.setMasteries(array3);
                poweritems[i] = temp2;
                int b = i;
                GameObject temp = Instantiate(PoweritemBlock);
                temp.GetComponent<PowerItemBlock>().Set(poweritems[i], this, b);
                poweritemblocks[i] = temp.GetComponent<PowerItemBlock>();
                temp.transform.SetParent(PowerItems.transform);
                
                empty = false;
            }
        }
    }
}


