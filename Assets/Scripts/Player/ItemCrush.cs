using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCrush : MonoBehaviour
{
    public float damage;

    float nextDamageTime;
    public float damageRate;
    public ParticleSystem psAxe;
    List<ParticleCollisionEvent> eventCol = new List<ParticleCollisionEvent>(); //lista de eventos del PS


    // Start is called before the first frame update
    void Start()
    {
        psAxe = GetComponent<ParticleSystem>();
    }
   

    private void OnParticleCollision(GameObject enemy)
    {
        int events = psAxe.GetCollisionEvents(enemy, eventCol); //tamaño de eventos

        for (int i = 0; i < events; i++)
        {
            if (Time.time >= nextDamageTime)
            {
                enemy.GetComponent<Health>().TakeDamage(damage);
                AudioManager.instance.PlayAudio(AudioManager.instance.hit);
                nextDamageTime = Time.time + 1f / damageRate;
            }
        }
    }
}
