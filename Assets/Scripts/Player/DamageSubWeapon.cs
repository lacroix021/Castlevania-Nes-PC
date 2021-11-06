using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSubWeapon : MonoBehaviour
{
    public float damage;

    SoundsSimon soundSimon;

    public bool knife;
    public bool axe;
    public bool holyWater;
    public bool holyFire;
    public bool cross;

    private float nextBurnHolyTime;
    private float burnRate = 3;

    private void Start()
    {
        soundSimon = GameObject.FindGameObjectWithTag("Player").GetComponent<SoundsSimon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Fireplace"))
        {
            if (knife)
            {
                soundSimon.audioWhip.clip = soundSimon.hit;
                soundSimon.audioWhip.Play();
                soundSimon.audioWhip.loop = false;
                Destroy(this.gameObject);
            }
            else if (axe || holyWater || cross)
            {
                soundSimon.audioWhip.clip = soundSimon.hit;
                soundSimon.audioWhip.Play();
                soundSimon.audioWhip.loop = false;
            }
        }

        if (collision.CompareTag("BreakWall"))
        {
            soundSimon.audioWhip.clip = soundSimon.breakWall;
            soundSimon.audioWhip.Play();
            soundSimon.audioWhip.loop = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Fireplace"))
        {
            if (holyFire || cross)
            {
                if (Time.time >= nextBurnHolyTime)
                {
                    soundSimon.audioWhip.clip = soundSimon.hit;
                    soundSimon.audioWhip.Play();
                    soundSimon.audioWhip.loop = false;
                    nextBurnHolyTime = Time.time + 1f / burnRate;
                }
            }
        }

        if (collision.CompareTag("BreakWall"))
        {
            soundSimon.audioWhip.clip = soundSimon.breakWall;
            soundSimon.audioWhip.Play();
            soundSimon.audioWhip.loop = false;
        }
    }
}
