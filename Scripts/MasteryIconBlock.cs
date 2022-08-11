using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasteryIconBlock : MonoBehaviour
{
    public int state = 0;
    public Image border;
    public Image smallborder;
    public Image icon;
    Color unavailable;
    Color available;
    Color green;

    Color smallgrey;
    Color grey;

    Color smallyellow;
    Color yellow;

    // Start is called before the first frame update
    void Start()
    { 
        unavailable = new Color(0.3f, 0.3f, 0.3f);
        available = new Color(1, 1, 1);

        green = new Color(0.254f, 0.717f, 0.376f);

        smallgrey = new Color(0.188f, 0.188f, 0.188f);
        //smallgrey = new Color(0.258f, 0.258f, 0.258f);
        //grey = new Color(0.321f, 0.321f, 0.321f);
        grey = new Color(0.364f, 0.364f, 0.364f);

        smallyellow = new Color(0, 0, 0);
        yellow = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetState(int mystate)
    {
        if (mystate == 0)
        {
            Unavailable();
        }
        else if (mystate == 1)
        {
            Available();
        }
        else if (mystate == 2)
        {
            Selected();
        }
    }
    public void Unavailable()
    {
       // Debug.Log("Made grey");
        border.color = grey;
        icon.color = unavailable;
        smallborder.color = new Color(smallgrey.r, smallgrey.b, smallgrey.g, 1);
            
    }
    public void Available() {
       // Debug.Log("Made green");
        border.color = green;
        icon.color = Color.white;
        smallborder.color = Color.clear;
    }
    public void Selected()
    {
       // Debug.Log("Made yellow");

        border.color = yellow;
        icon.color = Color.white;
        smallborder.color = Color.clear;
    }
}
