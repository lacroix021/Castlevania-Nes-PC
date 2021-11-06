using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Tooltip("Este Boss es el que se va a spawnear en este punto una sola vez en todo el juego")]
    public GameObject bossPrefab;
    GameObject bossSpawned;

    [Tooltip("esta casilla indica al manager si el Boss que se spawnea ya fue derrotado o no")]
    public bool isDead;

    private int numSpawns;

    BossMapManager bossManager;

    HealthBoss hBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossManager = GameObject.Find("GameManager").GetComponentInChildren<BossMapManager>();
        numSpawns = 0;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!bossSpawned && numSpawns < 1)
            {
                numSpawns += 1;
                bossSpawned = Instantiate(bossPrefab, transform.position, Quaternion.identity);
                bossSpawned.transform.parent = transform;
                bossSpawned.name = bossPrefab.name;
                hBoss = bossSpawned.GetComponent<HealthBoss>();
            }
        }

        HealthBossCheck();
    }

    
    void HealthBossCheck()
    {
        if (numSpawns == 1)
        {
            if (hBoss.currentHealth <= 0)
            {
                isDead = true;
                bossManager.CheckBoss();
            }
        }
    }
}
