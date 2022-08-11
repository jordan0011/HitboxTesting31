using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningObject3 : MonoBehaviour
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

    public int maximumShots = 5;

    public Vector3 direction1;

    public float altitude;

    Rigidbody rb1;//= Source.GetComponent<Rigidbody>();
    Vector3 finalforce;

    // Start is called before the first frame update
    void Start()
    {
        maximumShots = 1;
        altitude = Source.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Source)
        {

            altitude = Source.gameObject.transform.position.y;

            Rigidbody rb1 = Source.GetComponent<Rigidbody>();
            
            if(altitude < 3.8)
                rb1.AddForce(0, 10, 0);
            else
            {
                if (altitude < 4)
                    rb1.AddForce(0, 3, 0);
            }

           // altitude.y += Time.deltaTime;

        }
        if (maximumShots <= 0)
        {
            rb1.AddForce(finalforce, ForceMode.Force);


            damagetimer0 += Time.deltaTime;
            if (damagetimer0 >= damagetimer1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetUp(PowerStats myplayer, int shots)
    {
        Source = myplayer;
        //maximumShots = shots;
    }
    public void SetFire(Vector3 direction)
    {
        direction1 = direction;
        CheckAmmo();

        // Camera.main.gameObject.transform.forward;
        Vector3 asdf = direction - transform.position;
        Quaternion rotation;
        float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;


        Vector2 direction2 = new Vector2(asdf.x, asdf.z);


       // Vector3 finalforce;

        finalforce.x = direction2.x;
        finalforce.y = -1f;
        finalforce.z = direction2.y;

        rb1 = Source.GetComponent<Rigidbody>();
        finalforce *= 80f;

        float xangle = Mathf.Atan2(transform.position.y, direction2.magnitude) * Mathf.Rad2Deg;

        rotation = Quaternion.Euler(0, angle1, 0);

        transform.rotation = rotation;
        // Instantiate(Ability1, direction, transform.rotation);

        Vector3 landposition = Vector3.zero;
        landposition.x = direction.x;
        landposition.z = direction.z;
        landposition.y = 0.1f;

        GameObject instance = Instantiate(Hitbox1, landposition, rotation);

        instance.GetComponent<Damage>().Source = Source;

        //StartCoroutine(RepeatShot(0.15f));
    }
    public void CheckAmmo()
    {
        maximumShots--;
       
    }
    public IEnumerator RepeatShot(float s)
    {
        yield return new WaitForSeconds(s);

        SetFire(direction1);

    }
}
