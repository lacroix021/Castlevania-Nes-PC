using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaSpawner : MonoBehaviour
{
    [Tooltip("Este Boss es el que se va a spawnear en este punto una sola vez en todo el juego")]
    public GameObject bossPrefab;
    GameObject bossSpawned;

    [Tooltip("esta casilla indica al manager si el Boss que se spawnea ya fue derrotado o no")]
    public bool isDead;


    HealthBoss hBoss;


    public bool activeDracula;
    public Collider2D activatorColl;
    public LayerMask layerPlayer;
    public Transform boundaryFather;


    // Start is called before the first frame update
    void Start()
    {
        activatorColl.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        Active();

        if (!isDead)
        {
            if (!bossSpawned && activeDracula)
            {
                bossSpawned = Instantiate(bossPrefab, transform.position, Quaternion.identity);
                bossSpawned.transform.parent = boundaryFather.transform;
                bossSpawned.name = bossPrefab.name;
                hBoss = bossSpawned.GetComponent<HealthBoss>();
                activatorColl.enabled = false;
            }
        }

        HealthBossCheck();
    }


    void HealthBossCheck()
    {
        if (bossSpawned)
        {
            if (hBoss.currentHealth <= 0)
            {
                isDead = true;
                //bossManager.CheckBoss();
            }
        }
    }

    void Active()
    {
        activeDracula = Physics2D.IsTouchingLayers(activatorColl, layerPlayer);
    }
}
