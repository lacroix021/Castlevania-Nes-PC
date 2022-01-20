using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnerBoss : MonoBehaviour
{
    [SerializeField] SpawnerBoss [] BossSpawners;

    // Start is called before the first frame update
    void Start()
    {
        BossSpawners = GameObject.FindObjectsOfType<SpawnerBoss>();

        for (int i = 0; i < BossSpawners.Length; i++)
        {
            if(BossSpawners[i].TypeBoss == SpawnerBoss.typeBoss.batBoss)
            {
                //print("hay un murcielago: " + i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
