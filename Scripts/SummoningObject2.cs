using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningObject2 : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(PowerStats myplayer, int shots)
    {
        Source = myplayer;
        maximumShots = shots;
    }
    public void SetFire(Vector3 direction)
    {
        CheckAmmo();

        // Camera.main.gameObject.transform.forward;
        Vector3 asdf = direction - transform.position;
        Quaternion rotation;
        float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;


        Vector2 direction2 = new Vector2(asdf.x, asdf.z);


       float xangle = Mathf.Atan2(transform.position.y, direction2.magnitude)* Mathf.Rad2Deg;

        rotation = Quaternion.Euler(xangle, angle1, 0);

        transform.rotation = rotation;
       // Instantiate(Ability1, direction, transform.rotation);

        GameObject instance = Instantiate(Hitbox1, transform.position, rotation);

        instance.GetComponent<Damage>().Source = Source;
    }
    public void CheckAmmo()
    {
        maximumShots--;
        if (maximumShots <= 0)
        {
            Destroy(gameObject);
        }
    }
}
