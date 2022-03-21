using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorBattle : MonoBehaviour
{
    public DoppelGangerSpawner bossSpawner;

    Door doorScript;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossSpawner.active)
        {
            doorScript.needKey = true;
            doorScript.requiredKey = 7;
        }

        if (bossSpawner.defeated)
        {
            doorScript.needKey = false;
            doorScript.requiredKey = 0;
        }

        if (!bossSpawner.gameObject.activeInHierarchy)
        {
            doorScript.needKey = false;
            doorScript.requiredKey = 0;
        }
    }
}
