using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDatabase : MonoBehaviour
{
    private static AbilityDatabase _instance;

    public static AbilityDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AbilityDatabase is null");
            }
            return _instance;
        }
    }

    public Ability[] AllAbilities = new Ability[15];

    public GameObject[] AbilityIcons = new GameObject[15];
    public GameObject EmptyIcon;
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i< AbilityIcons.Length; i++)
        {
            if(AbilityIcons[i] != null)
            {
                GameObject temp = AbilityIcons[i];
                AllAbilities[i] = new Ability(temp, "Spell " + i.ToString(), i, "adsf");
               // Debug.Log(AllAbilities[i].getName() + " Spawned");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class Ability
{
    private GameObject icon;
    private string name;
    private int code;
    private string description;

    public Ability(GameObject myicon, string myname)
    {
        icon = myicon;
        name = myname;
    }
    public Ability(GameObject myicon, string myname, int mycode, string mydescription)
    {
        icon = myicon;
        code = mycode;
        description = mydescription;
        name = myname;
    }

    public GameObject getIcon()
    {
        return icon;
    }
    public int getCode()
    {
        return code;
    }
    public string getDescription()
    {
        return description;
    }
    public string getName()
    {
        return name;
    }
}