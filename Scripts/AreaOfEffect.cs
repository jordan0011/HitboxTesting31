using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    public float damagetimer1 = 1;
    public float damagetimer0 = 0;
    public float startdamage = 0.3f;
    public Collider collider1;
    public PowerStats Source;
    public float projectilespeed = 5;
    public Vector3 towards;

    public bool isExpanding = true;
    public float value = 0;

    // Start is called before the first frame update
    void Start()
    {
        collider1.enabled = false;
        towards = transform.forward;
        towards = Vector3.zero;
        transform.localScale = towards;

    }

    // Update is called once per frame
    void Update()
    {
        damagetimer0 += Time.deltaTime;

        if (damagetimer0 >= startdamage)
        {
            collider1.enabled = true;
        }
        if (damagetimer0 >= damagetimer1)
        {
            collider1.enabled = false;
            Destroy(gameObject);
        }

        value += Time.deltaTime * 50f;
        towards = new Vector3(value, value, value);
        transform.localScale = towards;
        //transform.position = transform.position + towards * projectilespeed * Time.deltaTime;
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
        if(other.gameObject.layer == 9)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 3);
            Debug.Log("Here");
        }
       // Debug.Log(other.gameObject);
    }
}
