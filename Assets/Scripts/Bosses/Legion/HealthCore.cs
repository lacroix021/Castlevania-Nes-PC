using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCore : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    Rigidbody2D rb;
    SpriteRenderer spr;
    Collider2D myColl;
    LegionSpawner legionSpawner;
    LegionController legionControl;

    public Material original;
    public Material blink;

    public bool isDead;

    float nextBurnHolyTime;
    public float burnRate;

    public GameObject deadEffect;

    [Header("Loot")]
    public GameObject itemLoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        myColl = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        legionSpawner = GameObject.Find("LegionSpawner").GetComponent<LegionSpawner>();
        legionControl = GameObject.FindObjectOfType<LegionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            legionSpawner.defeated = true;
            this.gameObject.layer = 19;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = Vector2.zero;
            myColl.isTrigger = false;
            
            StartCoroutine(TimeToDie(3));
        }
    }

    IEnumerator TimeToDie(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        Instantiate(itemLoot, transform.position, Quaternion.identity);
        Destroy(legionControl.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Weapon") && currentHealth > 0 && coll.GetComponent<DamagePlayer>())
        {
            TakeDamage(coll.GetComponent<DamagePlayer>().damage);
            StartCoroutine(FlashDamage());
        }

        if (coll.CompareTag("Weapon") && currentHealth > 0 && coll.GetComponent<DamageSubWeapon>())
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
                    nextBurnHolyTime = Time.time + 1f / burnRate;
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
