using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damagetimer1 = 1;
    public float damagetimer0 = 0;
    public float startdamage = 0.3f;
    public Collider collider1;
    public PowerStats Source;

    // Start is called before the first frame update
    void Start()
    {
        collider1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        damagetimer0 += Time.deltaTime;

        if(damagetimer0 >= startdamage)
        {
            collider1.enabled = true;
        }
        if(damagetimer0 >= damagetimer1)
        {
            collider1.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerStats powerstats = other.GetComponent<PowerStats>();
        if (powerstats)
        {
            if (Source != powerstats)
            {
                powerstats.Damage(20, Source);

            }
        }
        //Debug.Log(other.gameObject);
    }
}
