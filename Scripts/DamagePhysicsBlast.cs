using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePhysicsBlast : MonoBehaviour
{
    public float damagetimer1 = 1;
    public float damagetimer0 = 0;
    public float startdamage = 0.3f;
    public Collider collider1;
    public PowerStats Source;
    public float projectilespeed = 5;
    public Vector3 towards;
    public Rigidbody rb;

    public GameObject Ability1;
    public GameObject Hitbox1;

    public Fracture decorative = null;

    // Start is called before the first frame update
    void Start()
    {
        collider1.enabled = false;
        towards = transform.forward + new Vector3(0, 0.08f, 0);
        rb.AddForce(towards * 44, ForceMode.VelocityChange);



    }

    // Update is called once per frame
    void Update()
    {
        damagetimer0 += Time.deltaTime;

        if (damagetimer0 >= startdamage)
        {
            collider1.enabled = true;
        }
       

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

                Destroy(gameObject);

                SpawnAbility();
                decorative.FractureObject();
            }
        }
        else
        {
            if(other.gameObject.layer == 3)
            {

            Destroy(gameObject);

            SpawnAbility();
            decorative.FractureObject();

            }
        }
        
        // Debug.Log(other.gameObject);

        //if (other.gameObject.layer == 3)
        // {// Ground
    }
    public void SpawnAbility()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        Instantiate(Ability1, position, rotation);
        GameObject instance = Instantiate(Hitbox1, position, rotation);

        instance.GetComponent<AreaOfEffect>().Source = Source;
    }
}
