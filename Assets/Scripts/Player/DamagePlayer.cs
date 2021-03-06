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
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);
        }

        if (collision.CompareTag("BreakWall"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.breakWall);
        }
    }
}
