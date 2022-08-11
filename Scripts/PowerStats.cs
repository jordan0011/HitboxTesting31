using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int damage;
    public int magic;
    public int critical;
    public bool resetable = false;
    public HealthBarSystem healthbar;
    // Start is called before the first frame update
    void Start()
    {
        if (healthbar)
        {
            healthbar.SetBar(health, maxHealth);
        }
    }

    public void Damage(int value, PowerStats killer)
    {
        if(value >= 0)
        { 
            if((-value) + health > 0)
            {
                health -= value;
            }
            else
            {
                health = 0;
                killer.gameObject.GetComponent<PlayerController>().AddGold(100);
                if (resetable)
                {
                    ResetEntity();
                    //else destroy
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        
        if (healthbar)
        {
            healthbar.SetBar(health, maxHealth);
        }
    }
    public void ResetEntity()
    {
        Debug.Log("player Reset");
        transform.position = Vector3.zero;
        health = maxHealth;
    }
}
