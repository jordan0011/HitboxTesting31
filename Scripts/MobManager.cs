using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public GameObject enemyprefab;

    public GameObject[] enemies = new GameObject[10];
    public bool[] enemiesset = new bool[10];

    private void Start()
    {
        /*for(int i=0; i< 4; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));

            int a=0; 
            for(int h=0; h< enemies.Length; h++)
            {
                if(enemies[i] != null)
                {
                    a = h;
                }
            }
            if(a+1 < 10)
            {
                GameObject instance = Instantiate(enemyprefab, randomPos, Quaternion.identity);
                enemies[a+1] = instance;
            }
        }*/
        for(int i=0; i< enemiesset.Length; i++)
        {
            enemiesset[i] = false;
        }

        enemies[0] = Instantiate(enemyprefab, Vector3.zero, Quaternion.identity);
        enemies[1] = Instantiate(enemyprefab, Vector3.zero, Quaternion.identity);
        enemies[2] = Instantiate(enemyprefab, Vector3.zero, Quaternion.identity);
        enemies[3] = Instantiate(enemyprefab, Vector3.zero, Quaternion.identity);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        //if(other.gameObject.layer != 2)
       // {

           // Debug.Log(other.gameObject + "left");   
           /* Vector3 randomPos = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
            Instantiate(enemyprefab, randomPos, Quaternion.identity);*/
            CheckForEnemies();
       // }
    }
    void CheckForEnemies()
    {

        for(int i=0; i< enemies.Length; i++)
        {
            if(enemies[i] == null)
            {

                float time = 5;
                   // Random.Range(0, 5);
                if(enemiesset[i] == false)
                {
                    StartCoroutine(StartSpawn(time, i));
                    enemiesset[i] = true;
                }
                
            }
        }

    }
    void SpawnEnemy()
    {
        for(int i=0; i< enemies.Length; i++)
        {
            if (enemies[i] == null && enemiesset[i] == true)
            {
                Vector3 randomPos = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
                enemies[i] = Instantiate(enemyprefab, randomPos, Quaternion.identity);
                enemiesset[i] = false;
            }
        }
    }
    void SpawnTargetEnemy(int n)
    {
        if (enemies[n] == null && enemiesset[n] == true)
        {
            Vector3 randomPos = new Vector3(Random.Range(-7, 7), 1, Random.Range(-3, 11));
            enemies[n] = Instantiate(enemyprefab, randomPos, Quaternion.identity);
            enemiesset[n] = false;
        }
    }
    IEnumerator StartSpawn(float delay, int a)
    {
        yield return new WaitForSeconds(delay);
        // SpawnEnemy();
        SpawnTargetEnemy(a);
    }
    
}
