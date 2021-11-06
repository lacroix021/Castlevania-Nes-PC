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

    private BoxCollider2D mycollider;

    private float boundX;
    private float boundY;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        mycollider = GetComponent<BoxCollider2D>();
    }

    public void HealthCheck()
    {
        if (currentHealth <= 0)
        {
            
            isDead = true;
        }
    }

    void Death()
    {
        if (isDead)
        {
            GameObject instance = Instantiate(deadEffect, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Weapon") && currentHealth > 0 && col.GetComponent<DamagePlayer>())
        {
            //spawnear la chispa indicando que si hubo daño

            GameObject spark = Instantiate(sparkDamage, transform.position, Quaternion.identity);
            Destroy(spark, 0.3f);

            currentHealth -= col.GetComponent<DamagePlayer>().damage;
            HealthCheck();
            Death();
        }
        else if (col.CompareTag("Weapon") && currentHealth > 0 && col.gameObject.GetComponent<DamageSubWeapon>())
        {
            //spawnear la chispa indicando que si hubo daño
            
            if(col.transform.position.x < mycollider.bounds.min.x)
                boundX = col.transform.position.x + 0.1f;
            else if (col.transform.position.x > mycollider.bounds.min.x)
                boundX = col.transform.position.x - 0.1f;

            if (col.transform.position.y < mycollider.bounds.min.y)
                boundY = col.transform.position.y + 0.1f;
            else if (col.transform.position.y > mycollider.bounds.min.y)
                boundY = col.transform.position.y - 0.1f;

            GameObject spark = Instantiate(sparkDamage, new Vector2(boundX, boundY), Quaternion.identity);
            Destroy(spark, 0.3f);

            currentHealth -= col.GetComponent<DamageSubWeapon>().damage;
            HealthCheck();
            Death();
        }
    }

    //trigger especial para el fuego del agua bendita
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && currentHealth > 0 && collision.gameObject.GetComponent<DamageSubWeapon>())
        {
            if (collision.gameObject.GetComponent<DamageSubWeapon>().holyFire || collision.gameObject.GetComponent<DamageSubWeapon>().cross)
            {
                if (Time.time >= nextBurnHolyTime)
                {
                    //spawnear la chispa indicando que si hubo daño

                    if (collision.transform.position.x < mycollider.bounds.min.x)
                        boundX = collision.transform.position.x + 0.1f;
                    else if (collision.transform.position.x > mycollider.bounds.min.x)
                        boundX = collision.transform.position.x - 0.1f;

                    if (collision.transform.position.y < mycollider.bounds.min.y)
                        boundY = collision.transform.position.y + 0.1f;
                    else if (collision.transform.position.y > mycollider.bounds.min.y)
                        boundY = collision.transform.position.y - 0.1f;

                    GameObject spark = Instantiate(sparkDamage, new Vector2(boundX, boundY), Quaternion.identity);
                    Destroy(spark, 0.3f);

                    currentHealth -= collision.GetComponent<DamageSubWeapon>().damage;
                    HealthCheck();
                    Death();
                    nextBurnHolyTime = Time.time + 1f / burnRate;
                }
            }
        }
    }
}
