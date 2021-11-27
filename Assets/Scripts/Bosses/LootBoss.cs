using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoss : MonoBehaviour
{
    public bool haveLoot;
    public GameObject lootPrefab;


    public void DropLoot()
    {
        if (haveLoot)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }
}
