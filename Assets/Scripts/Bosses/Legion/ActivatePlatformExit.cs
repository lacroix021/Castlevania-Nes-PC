using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatformExit : MonoBehaviour
{

    LegionSpawner legionSpwnr;

    Collider2D myColl;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        legionSpwnr = GameObject.FindObjectOfType<LegionSpawner>();
        myColl = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (legionSpwnr.defeated)
        {
            myColl.enabled = true;
            spr.enabled = true;
        }
        else
        {
            myColl.enabled = false;
            spr.enabled = false;
        }
    }
}
