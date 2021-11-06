using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public float damage;
    
    public SoundsSimon soundSimon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Fireplace"))
        {
            soundSimon.audioWhip.clip = soundSimon.hit;
            soundSimon.audioWhip.Play();
            soundSimon.audioWhip.loop = false;
        }

        if (collision.CompareTag("BreakWall"))
        {
            soundSimon.audioWhip.clip = soundSimon.breakWall;
            soundSimon.audioWhip.Play();
            soundSimon.audioWhip.loop = false;
        }
    }
}
