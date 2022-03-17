using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosibleLoot : MonoBehaviour
{
    public GameObject[] loot;
    public int num;
    public int possibility;
       
    public void DropLoot()
    {
        possibility = Random.Range(0, 101);
        //poca posibilidad
        if (possibility >= 81 && possibility <= 85)
        {
            Instantiate(loot[2], transform.position, Quaternion.identity);
        }
        //posibilidad media
        else if (possibility >= 10 && possibility <= 20)
        {
            num = Random.Range(0, 101);
            if (num <= 70)
            {
                Instantiate(loot[0], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(loot[1], transform.position, Quaternion.identity);
            }
        }
    }
}
