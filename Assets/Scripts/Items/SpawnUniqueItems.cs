using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUniqueItems : MonoBehaviour
{
    [Tooltip("Este item es el que se va a spawnear en este punto una sola vez en todo el juego")]
    public GameObject itemPrefab;

    GameObject itemInstanced;

    [Tooltip("esta casilla indica al manager si el item que se spawnea ya fue tomado o no")]
    public bool wasCaught;

    ItemMapManager itemMapManager;

    private void Awake()
    {
        itemMapManager = GameObject.Find("GameManager").GetComponentInChildren<ItemMapManager>();
    }

    private void Start()
    {
        if (!wasCaught)
        {
            if (!itemInstanced)
            {
                itemInstanced = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                itemInstanced.name = itemPrefab.name;
                itemInstanced.transform.parent = transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wasCaught = true;
            itemMapManager.CheckItems();
        }
    }
}
