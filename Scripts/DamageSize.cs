using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSize : MonoBehaviour
{
    public float damagetimer1 = 1;
    public float damagetimer0 = 0;
    public float startdamage = 0.3f;
    public Collider collider1;
    public PowerStats Source;
    public float projectilespeed = 5;
    public Vector3 towards;

    public Vector3 direction;

    public bool isExpanding = true;
    public float value = 0;

    public GameObject hitbox;

    public bool push = false;
    // Start is called before the first frame update
    void Start()
    {
        collider1.enabled = false;
        //towards = transform.forward;
       // towards = transform.localScale;

        hitbox = collider1.gameObject;

        towards.z = value;
        
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

        if (value < 8f)
            value += Time.deltaTime * 60f;
        else
            value = 8f;
            //towards = new Vector3(value, value, value);
        towards.z = value;

        Vector3 finalposition = Vector3.zero;// + new Vector3(0, 0, value / 2f);

        finalposition.z = value / 2f;

        hitbox.transform.localPosition  = finalposition;

        hitbox.transform.localScale = towards;

        //transform.localScale = towards;
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

        if(other.gameObject.layer == 7)
        {
            Vector3 force = transform.rotation * Vector3.forward * 1800;
            other.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
        // Debug.Log(other.gameObject);
    }
    public void SetUp(Vector3 mydirection, PowerStats player)
    {
        Source = player;

        
    }
}

