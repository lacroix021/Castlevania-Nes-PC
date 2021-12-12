using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public GameObject boundaryFather;
    GameObject instanceSpawned;

    public bool instanceGhost;
    public bool obstacle;

    
    private void OnEnable()
    {
        if (!instanceSpawned)
        {
            instanceSpawned = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            instanceSpawned.transform.parent = boundaryFather.GetComponent<Transform>();
        }
        else
        {
            instanceSpawned.transform.position = transform.position;

            if (instanceGhost)
            {
                instanceSpawned.GetComponentInChildren<Health>().currentHealth = instanceSpawned.GetComponentInChildren<Health>().maxHealth;
            }
            else if (obstacle)
            {
                return;
            }
            else
            {
                instanceSpawned.GetComponent<Health>().currentHealth = instanceSpawned.GetComponent<Health>().maxHealth;
            }
            
        }
    }
}
