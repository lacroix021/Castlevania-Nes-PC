using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSubWeapon : MonoBehaviour
{
    public enum typeSub
    {
        knife,
        axe,
        holyWater,
        holyFire,
        cross
    };

    public typeSub TypeSup;

    public float damage;

    private float nextBurnHolyTime;
    private float burnRate = 3;
    
    private void Start()
    {
        if (TypeSup == typeSub.holyFire)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.holyWater);
            AudioManager.instance.PlayAudio(AudioManager.instance.burnHoly);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Fireplace"))
        {
            if (TypeSup == typeSub.knife)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.hit);
                Destroy(this.gameObject);
            }
            else if (TypeSup == typeSub.axe || TypeSup == typeSub.holyWater || TypeSup == typeSub.cross)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.hit);
            }
        }

        if (collision.CompareTag("BreakWall"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.breakWall);

            if (TypeSup == typeSub.knife)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Fireplace"))
        {
            if (TypeSup == typeSub.holyFire || TypeSup == typeSub.cross)
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
