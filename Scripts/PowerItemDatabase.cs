using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemDatabase : MonoBehaviour
{
    private static PowerItemDatabase _instance;

    public static PowerItemDatabase Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("PoweritemDatabase is null");
            }
            return _instance;
        }
    }

    public Poweritem[] AllPowerItems = new Poweritem[10];

    public GameObject[] PowerItemIcons = new GameObject[10];
    public GameObject EmptyIcon;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        int[][] array1 = new int[10][];
        array1[0] = new int[] { 0, 1, 3, -1 };
        array1[1] = new int[] { 1, 2, 3, -1 };
        array1[2] = new int[] { 2, 3, 4, -1 };
        array1[3] = new int[] { 3, 4, 5, -1 };
        array1[4] = new int[] { 4, 5, 6, -1 };
        array1[5] = new int[] { 5, 6, 7, -1 };
        array1[6] = new int[] { 6, 7, 8, -1 };
        array1[7] = new int[] { 7, 8, 9, -1 };
        array1[8] = new int[] { 10, 11, 12, -1 };
        array1[9] = new int[] { 12, 13, 14, -1 };


        for (int i = 0; i < PowerItemIcons.Length; i++)
        {
            if(PowerItemIcons[i] != null)
            {
                GameObject temp = PowerItemIcons[i];
                int[] itemsab = { 0, 1, 3, -1};
                int[] itempl = { -1, -1, -1};

                AllPowerItems[i] = new Poweritem(i, temp, "Power Item " + i.ToString(), "the type", 1,2,3, array1[i], itempl, 34);
                bool[] array = new bool[34];
                for(int h=0; h< 34; h++)
                {
                    array.SetValue(false, h);
                }
                AllPowerItems[i].setMasteries(array);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class Poweritem
{
    private int id;
    private string name;
    private string type;
    private int attack;
    private int ability1;
    private int ability2;
    private int[] abilities;
    private int[] playset;
    private GameObject icon;
    private bool[] masteries;
    public Poweritem(int myid, GameObject myicon, string myname, string mytype, int myattack, int myability1, int myability2, int[] myabilities, int[] myplayset, int mlength)
    {
        id = myid;
        name = myname;
        type = mytype;
        attack = myattack;
        ability1 = myability1;
        ability2 = myability2;
        icon = myicon;
        abilities = myabilities;
        playset = myplayset;
        masteries = new bool[mlength];
    }
    public void setMasteries(bool[] array)
    {
        masteries = array;
    }
    public bool[] getMasteries()
    {
        return masteries;
    }
    public int[] getPlayset()
    {
        return playset;
    }
    public void setPlaySet(int[] myplayset)
    {
        playset = myplayset;
    }
    public int[] getAbilities()
    {
        return abilities;
    }
    public void setAbilities(int[] myabilities)
    {
        abilities = myabilities;
    }
    public GameObject getIcon()
    {
        return icon;
    }
    public int GetId()
    {
        return id;
    }
    public string getName()
    {
        return name;
    }
    public string GetTheType()
    {
        return type;
    }
    public int GetAttack()
    {
        return attack;
    }
    public int GetAbility1()
    {
        return ability1;
    }
    public int GetAbility2()
    {
        return ability2;
    }
    public void SetAttack(int newid)
    {
        attack = newid;
    }
    public void SetAbility1(int newid)
    {
        ability1 = newid;
    }
    public void SetAbility2(int newid)
    {
        ability2 = newid;
    }
}

