using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timer1 = 1;
    public float timer0 = 0;
    // Update is called once per frame
    void Update()
    {
        timer0 += Time.deltaTime;

        if(timer0 >= timer1)
        {
            Destroy(gameObject);
        }
    }
}
