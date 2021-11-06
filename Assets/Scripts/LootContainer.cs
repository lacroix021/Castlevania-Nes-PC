using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : MonoBehaviour
{

    public GameObject containerPrefab;

    GameObject containerSpawned;


    [Header("LOOT POSIBLE")]
    [Tooltip("siempre poner como primer objeto en la casilla 0 el latigo y en la casilla numero 5 el item mas raro que se dropeara")]    
    public GameObject[] loot;



    private void OnEnable()
    {
        if (!containerSpawned)
        {
            containerSpawned = Instantiate(containerPrefab, transform.position, Quaternion.identity);
            containerSpawned.transform.parent = transform;
        }
        else
        {
            containerSpawned.transform.position = transform.position;
        }
    }
}
