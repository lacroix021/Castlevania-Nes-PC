using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteEnemyController : MonoBehaviour
{
    public bool debug;
    float nextSpawnTime;
    public float spawnRate;

    [Header("ENEMIGOS PARA SPAWNEAR")]
    public GameObject zombiePrefab;
    public GameObject redBatPrefab;
    public GameObject medusaHeadPrefab;
    public GameObject hawkPrefab;

    GameObject enemyPrefab;

    [Header("PUNTOS DE SPAWN")]

    public Transform posA;
    public Transform posAUp;
    public Transform posB;
    public Transform posBUp;
    public Transform posCUp;
    public Transform posDUp;

    [Header("BOUNDARY TO ACTIVE")]
    public GameObject [] bounds;

    Transform fatherBound;

    // Update is called once per frame
    void Update()
    {
        SpawnRandomly();
    }

    void SpawnRandomly()
    {
        if (!debug)
        {
            for (int i = 0; i < bounds.Length; i++)
            {

                if (bounds[i].activeInHierarchy == true)
                {
                    fatherBound = bounds[i].transform;

                    if (bounds[0].activeInHierarchy == true)
                    {
                        //enemyPrefab = zombiePrefab;
                        spawnRate = 0.6f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(zombiePrefab, posA.position, Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(zombiePrefab, posB.position, Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }

                    else if (bounds[1].activeInHierarchy == true)
                    {
                        //enemyPrefab = redBatPrefab;
                        spawnRate = 0.3f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(redBatPrefab, new Vector2(posAUp.position.x, posAUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(redBatPrefab, new Vector2(posBUp.position.x, posBUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }

                    else if (bounds[2].activeInHierarchy == true)
                    {
                        //enemyPrefab = redBatPrefab;
                        spawnRate = 0.5f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(redBatPrefab, new Vector2(posAUp.position.x, posAUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(redBatPrefab, new Vector2(posBUp.position.x, posBUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }

                    else if (bounds[3].activeInHierarchy == true)
                    {
                        //enemyPrefab = medusaHeadPrefab;
                        spawnRate = 0.9f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posAUp.position.x, posAUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posBUp.position.x, posBUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }
                    else if (bounds[4].activeInHierarchy == true || bounds[5].activeInHierarchy == true || bounds[6].activeInHierarchy == true)
                    {
                        //enemyPrefab = medusaHeadPrefab;
                        spawnRate = 0.3f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posAUp.position.x, posAUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posBUp.position.x, posBUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }
                    else if (bounds[7].activeInHierarchy == true || bounds[8].activeInHierarchy == true || bounds[9].activeInHierarchy == true)
                    {
                        //enemyPrefab = medusaHeadPrefab;
                        spawnRate = 0.5f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posAUp.position.x, posAUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posBUp.position.x, posBUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }
                    else if (bounds[10].activeInHierarchy == true)
                    {
                        //enemyPrefab = Hawk;
                        spawnRate = 0.3f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(hawkPrefab, new Vector2(posCUp.position.x, posCUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(hawkPrefab, new Vector2(posDUp.position.x, posDUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }
                    else if (bounds[11].activeInHierarchy == true || bounds[12].activeInHierarchy == true)
                    {
                        //enemyPrefab = Hawk;
                        spawnRate = 0.1f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(hawkPrefab, new Vector2(posCUp.position.x, posCUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(hawkPrefab, new Vector2(posDUp.position.x, posDUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }

                    else if (bounds[13].activeInHierarchy == true || bounds[14].activeInHierarchy == true)
                    {
                        //enemyPrefab = medusaHead;
                        spawnRate = 0.5f;

                        if (Time.time >= nextSpawnTime)
                        {
                            int num = Random.Range(0, 2);

                            if (num == 0)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posCUp.position.x, posCUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }
                            else if (num == 1)
                            {
                                GameObject instancedEnemy = Instantiate(medusaHeadPrefab, new Vector2(posDUp.position.x, posDUp.position.y), Quaternion.identity);
                                instancedEnemy.transform.parent = fatherBound;
                            }

                            nextSpawnTime = Time.time + 1f / spawnRate;
                        }
                    }
                    //aqui agregaria otro tipo de enemigos dependiendo el bound, tener en cuenta el numero de bound en el array hecho manualmente
                    //agregar los halcones en su respectivo boundary con los puntos C y D que son spawners exclusivos para ellos
                }
            }
        }
    }
}
