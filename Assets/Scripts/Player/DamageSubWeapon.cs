using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSubWeapon : MonoBehaviour
{
    public float damage;

    public bool knife;
    public bool axe;
    public bool holyWater;
    public bool holyFire;
    public bool cross;

    private float nextBurnHolyTime;
    private float burnRate = 3;

    private void Start()
    {
        if (holyFire)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.holyWater);
            AudioManager.instance.PlayAudio(AudioManager.instance.burnHoly);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Fireplace"))
        {
            if (knife)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.hit);
                Destroy(this.gameObject);
            }
            else if (axe || holyWater || cross)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.hit);
            }
        }

        if (collision.CompareTag("BreakWall"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.breakWall);

            if (knife)
            {
                Destroy(this.gameObject);
            }
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
                    AudioManager.instance.PlayAudio(AudioManager.instance.hit);
                    nextBurnHolyTime = Time.time + 1f / burnRate;
                }
            }
        }

        if (collision.CompareTag("BreakWall"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.breakWall);
        }
    }
}
