using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerItemPanel : MonoBehaviour
{
    [SerializeField]
    private GeneralInventorySystem system;

    [SerializeField]
    private GameObject masteriesPanel;


    public MasteryPanel mp1;

    public bool[] array1 = new bool[34];

    // Start is called before the first frame update
    void Start()
    {
        mp1 = masteriesPanel.GetComponent<MasteryPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void LoadPowerItemInventories(int[] myabilities, Poweritem mypoweritem)
    {
        Debug.Log("Here");
    }
    public void LoadPowerItemInventories(int[] myabilities, int[] myitemabilities, int[] myplayset, bool[] mymasteries)
    {
        system.LoadPowerItemInventories(myabilities, myitemabilities, myplayset);
        /*for(int i=0; i< myabilities.Length; i++)
        {
            Debug.Log(myabilities + " " + myabilities[i]);
        }
        for (int i = 0; i < myitemabilities.Length; i++)
        {
            Debug.Log(myitemabilities + " " + myitemabilities[i]);
        }
        for (int i = 0; i < myplayset.Length; i++)
        {
            Debug.Log(myplayset + " " + myplayset[i]);
        }*/

        array1 = mymasteries;
    }

    public void OpenItemMasteries()
    {
        Debug.Log("OpenClose Masteries");
        masteriesPanel.SetActive(!masteriesPanel.activeSelf);
        if (masteriesPanel.activeSelf)
        {
            Debug.Log("Load");

            mp1.LoadMasteries(array1);
        }
        else
        {
            Debug.Log("Save");

            array1 = mp1.SaveMasteries();
            system.UpdateMasteries(array1);
        }
    }
}
