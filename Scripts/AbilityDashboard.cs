using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDashboard : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;

    public bool activate = false;


    public int[] array = new int[3];

    public InventorySystem slots;

    public PlayerController player;

    public DashboardIteractive dashboard;
    void Start()
    {
        for(int i=0; i< array.Length; i++)
        {
            array[i] = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            ReloadAbilities(array);
            activate = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dashboard.ResetAbility(0);
            player.CommandAbility(array[0]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dashboard.ResetAbility(1);
            player.CommandAbility(array[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            dashboard.ResetAbility(2);
            player.CommandAbility(array[2]);
        }
    }
    public void OpenClosePanel()
    {
        // Debug.Log("Open Close Panel");
        panel.SetActive(!panel.activeSelf);
    }
    public void ReloadAbilities(int[] myarray)
    {
        string newstring = "";
        for (int i = 0; i < myarray.Length; i++)
        {
            newstring += myarray[i] + ", ";
        }
        Debug.Log(newstring);
        array = myarray;
        slots.LoadInventory(myarray);
    }
    public void LoadDashboard(int[] myarray)
    {
        ReloadAbilities(myarray);
    }
    

}
