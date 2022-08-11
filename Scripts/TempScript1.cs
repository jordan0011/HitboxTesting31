using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript1 : MonoBehaviour
{
    public MasteryPanel mp1;

    public bool[] array1 = new bool[34];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Load1()
    {
        //
        mp1.LoadMasteries(array1);
    }
    public void Load2()
    {
        //
        array1 = mp1.SaveMasteries();
    }
}
