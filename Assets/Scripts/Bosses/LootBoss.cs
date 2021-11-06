using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoss : MonoBehaviour
{
    public GameObject lootPrefab;



    public void DropLoot()
    {
        Instantiate(lootPrefab, transform.position, Quaternion.identity);
    }
}
