using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWallManager : MonoBehaviour
{
    public GameObject breakableWallPrefab;
    GameObject wallInstanced;

    public bool wasBroken;

    StructureManager structureManager;

    int num;

    private void Awake()
    {
        structureManager = GameObject.Find("GameManager").GetComponentInChildren<StructureManager>();
    }

    private void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasBroken)
        {
            if (!wallInstanced && num < 1)
            {
                num += 1;
                wallInstanced = Instantiate(breakableWallPrefab, transform.position, Quaternion.identity);
                wallInstanced.name = breakableWallPrefab.name;
                wallInstanced.transform.parent = transform;
            }
        }

        CheckWall();
    }

    void CheckWall()
    {
        if(num == 1)
        {
            if (wallInstanced == null)
            {
                wasBroken = true;
                structureManager.CheckWalls();
            }
        }
    }
}
