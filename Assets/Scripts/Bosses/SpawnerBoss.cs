using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    public enum typeBoss
    {
        batBoss,
        medusaBoss,
        mumiesBoss
    };

    public typeBoss TypeBoss;


    public GameObject bossPrefab;
    GameObject bossInstanced;
    public bool bossSpawned;


    private void OnEnable()
    {

        
    }
    // Start is called before the first frame update
    void Start()
    {
        print("este es Start");

        if (!bossSpawned)
        {
            bossInstanced = Instantiate(bossPrefab, transform.position, Quaternion.identity);
            bossSpawned = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
