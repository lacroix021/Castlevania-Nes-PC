using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public bool isDead;

    public GameObject deadEffect;

    private float nextBurnHolyTime;
    private float burnRate = 2.5f;

    public GameObject sparkDamage;

    public Material original;
    public Material blink;
    public SpriteRenderer sprRenderer;

    public bool haveLoot = false;
    PosibleLoot posibleLoot;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        if (haveLoot)
        {
            posibleLoot = GetComponent<PosibleLoot>();
        }
    }

    public void HealthCheck()
    {
        if (currentHealth <= 0)
        {
            
            isDead = true;
        }

        Death();
    }

    void Death()
    {
        if (isDead)
        {
            GameObject instance = Instantiate(deadEffect, transform.position, Quaternion.identity);

            if (haveLoot)
            {
                posibleLoot.DropLoot();
            }

            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthCheck();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Weapon") && currentHealth > 0 && col.GetComponent<DamagePlayer>())
        {
            StartCoroutine(FlashDamage());
            TakeDamage(col.GetComponent<DamagePlayer>().damage);
            Death();
            
        }
        else if (col.CompareTag("Weapon") && currentHealth > 0 && col.gameObject.GetComponent<DamageSubWeapon>())
        {
            StartCoroutine(FlashDamage());

            TakeDamage(col.GetComponent<DamageSubWeapon>().damage);
            Death();
        }
    }

    //trigger especial para el fuego del agua bendita
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && currentHealth > 0 && collision.gameObject.GetComponent<DamageSubWeapon>())
        {
            if (collision.gameObject.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.holyFire 
                || collision.gameObject.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.cross)
            {
                if (Time.time >= nextBurnHolyTime)
                {
                    StartCoroutine(FlashDamage());
                    TakeDamage(collision.GetComponent<DamageSubWeapon>().damage);
                    Death();
                    nextBurnHolyTime = Time.time + 1f / burnRate;
                }
            }
        }
    }

    IEnumerator FlashDamage()
    {
        sprRenderer.material = blink;
        yield return new WaitForSeconds(0.2f);
        sprRenderer.material = original;
    }
}
