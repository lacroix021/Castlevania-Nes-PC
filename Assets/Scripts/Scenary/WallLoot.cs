using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLoot : MonoBehaviour
{

    public GameObject loot;


    
    public void InstantiateLoot()
    {
        Instantiate(loot, transform.position, Quaternion.identity);
    }

}
