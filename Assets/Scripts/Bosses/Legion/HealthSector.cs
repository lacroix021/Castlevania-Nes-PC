using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSector : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Collider2D myColl;

    public Material original;
    public Material blink;

    public bool isDead;

    float nextBurnHolyTime;
    public float damageRate;

    public GameObject deadEffect;

    public ParticleSystem particleA;
    public ParticleSystem particleB;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        myColl = GetComponent<Collider2D>();
        currentHealth = maxHealth;
    }

    public void HealthCheck()
    {
        if (currentHealth <= 0)
        {

            isDead = true;
        }

        Death();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthCheck();
    }

    void Death()
    {
        if (isDead)
        {
            spr.enabled = false;
            this.gameObject.layer = 19;
            particleA.Play();
            particleB.Play();
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            Instantiate(part1, pos1.position, Quaternion.identity);
            Instantiate(part2, pos2.position, Quaternion.identity);
            Instantiate(part3, pos3.position, Quaternion.identity);
            StartCoroutine(TimeToDie(3));
        }
    }

    IEnumerator TimeToDie(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Weapon") && currentHealth > 0 && coll.GetComponent<DamagePlayer>())
        {
            TakeDamage(coll.GetComponent<DamagePlayer>().damage);
            StartCoroutine(FlashDamage());
        }

        if(coll.CompareTag("Weapon") && currentHealth > 0 && coll.GetComponent<DamageSubWeapon>())
        {
            TakeDamage(coll.GetComponent<DamageSubWeapon>().damage);
            StartCoroutine(FlashDamage());
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Weapon") && currentHealth > 0 && coll.gameObject.GetComponent<DamageSubWeapon>())
        {
            if (coll.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.holyFire 
                || coll.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.cross
                || coll.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.axe)
            {
                if (Time.time >= nextBurnHolyTime)
                {
                    StartCoroutine(FlashDamage());
                    TakeDamage(coll.GetComponent<DamageSubWeapon>().damage);
                    nextBurnHolyTime = Time.time + 1f / damageRate;
                }
            }
        }
    }

    IEnumerator FlashDamage()
    {
        spr.material = blink;
        yield return new WaitForSeconds(0.2f);
        spr.material = original;
    }
}
