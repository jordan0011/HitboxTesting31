using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardIteractive : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;

    public bool reset1 = false;
    public bool reset2 = false;
    public bool reset3 = false;

    public AbilityDashboard abilities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reset1)
        {
            float a = image1.color.a;
            a -= Time.deltaTime * 6f;
            image1.color = new Color(1, 1, 1, a);
            if (a <= 0.01f)
                reset1 = false;
        }
        if (reset2)
        {
            float a = image2.color.a;
            a -= Time.deltaTime * 6f;
            image2.color = new Color(1, 1, 1, a);
            if (a <= 0.01f)
                reset2 = false;
        }
        if (reset3)
        {
            float a = image3.color.a;
            a -= Time.deltaTime * 6f;
            image3.color = new Color(1, 1, 1, a);
            if (a <= 0.01f)
                reset3 = false;
        }
    }

    public void ResetAbility(int n)
    {
        if (n == 0)
        {
            reset1 = true;
            image1.color = Color.white;
        }
        else if (n == 1)
        {
            reset2 = true;
            image2.color = Color.white;
        }
        else if (n == 2)
        {
            reset3 = true;
            image3.color = Color.white;
        }
    }

    public void OnEnter(int n)
    {
        Debug.Log(AbilityDatabase.Instance.AllAbilities[abilities.array[n]].getName());
    }
    public void OnExit(int n)
    {

    }
    
}
