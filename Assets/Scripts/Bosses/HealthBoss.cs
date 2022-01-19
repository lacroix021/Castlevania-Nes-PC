using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoss : MonoBehaviour
{
    public enum typeBoss
    {
        BossGiantBat,
        BossMedusa,
        BossMummyA,
        BossMummyB
    };

    public typeBoss boss;

    public float currentHealth;
    public float maxHealth;

    public bool isDead;

    public GameObject deadEffect;

    private float nextBurnHolyTime;
    private float burnRate = 2.5f;

    public GameObject sparkDamage;

    private BoxCollider2D mycollider;

    public float boundX;
    public float boundY;

    BossMapManager bossManager;

    

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        //DESCOMENTAREAR bossManager AL SALIR DE LAS PRUEBAS

        bossManager = GameManager.gameManager.GetComponentInChildren<BossMapManager>();
        mycollider = GetComponent<BoxCollider2D>();
    }

    public void HealthCheck()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            if(boss == typeBoss.BossGiantBat)
            {
                isDead = true;
                GameObject.Find("Stage1Music").GetComponent<ActivateMusic>().battle = false;    //se apaga el modo batalla para que la musica vuelva a la normalidad
            }
            else if(boss == typeBoss.BossMedusa)
            {
                isDead = true;
                GameObject.Find("MedusaStatue").GetComponent<MedusaStatueController>().medusaDefeated = true;
                GameObject.Find("Stage2Music").GetComponent<ActivateMusic>().battle = false;    //se apaga el modo batalla para que la musica vuelva a la normalidad
                bossManager.CheckBoss();
            }
            else if(boss == typeBoss.BossMummyA)
            {
                //el control de musica apagada se realiza desde el spawner de las momias
                isDead = true;
                GameObject.Find("CeilingMummies").GetComponent<CeilingSpawnerMummies>().isDeadA = true;
                bossManager.CheckBoss();
            }
            else if (boss == typeBoss.BossMummyB)
            {
                //el control de musica apagada se realiza desde el spawner de las momias
                isDead = true;
                GameObject.Find("CeilingMummies").GetComponent<CeilingSpawnerMummies>().isDeadB = true;
                bossManager.CheckBoss();
            }
            else if(GameObject.Find("CeilingMummies").GetComponent<CeilingSpawnerMummies>().isDeadA &&
                GameObject.Find("CeilingMummies").GetComponent<CeilingSpawnerMummies>().isDeadB)
            {
                GameObject.Find("Stage3Music").GetComponent<ActivateMusic>().battle = false;
            }
        }
    }

    void Death()
    {
        if (isDead)
        {
            GameObject instance = Instantiate(deadEffect, transform.position, Quaternion.identity);

            GetComponent<LootBoss>().DropLoot();
            //activador de evento de Pit
            //guardar tambien esto en el manager al salvar el juego

            if(boss == typeBoss.BossGiantBat)
            {
                GameObject.Find("EventPit").GetComponent<PitEvent>().pitEnable = true;
                GameObject.Find("EventPit").GetComponent<PitEvent>().PitEnable();
            }
            else if(boss == typeBoss.BossMedusa)
            {
                //poner evento de medusa aqui si lo hay
            }
            else if(boss == typeBoss.BossMummyA)
            {
                //poner evento de momia si lo hay
            }
            else if (boss == typeBoss.BossMummyB)
            {
                //poner evento de momia si lo hay
            }

            //poner algo en el pit para que valga la pena el retroceso

            Destroy(this.gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Weapon") && currentHealth > 0 && col.GetComponent<DamagePlayer>())
        {
            //spawnear la chispa indicando que si hubo daño
            AudioManager.instance.PlayAudio(AudioManager.instance.makeDamageBoss);
            GameObject spark = Instantiate(sparkDamage, transform.position, Quaternion.identity);
            Destroy(spark, 0.3f);

            currentHealth -= col.GetComponent<DamagePlayer>().damage;
            HealthCheck();
            Death();
        }
        else if (col.CompareTag("Weapon") && currentHealth > 0 && col.gameObject.GetComponent<DamageSubWeapon>())
        {
            //spawnear la chispa indicando que si hubo daño
            AudioManager.instance.PlayAudio(AudioManager.instance.makeDamageBoss);
            if (col.transform.position.x < mycollider.bounds.min.x)
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
            if (collision.gameObject.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.holyFire || collision.gameObject.GetComponent<DamageSubWeapon>().TypeSup == DamageSubWeapon.typeSub.cross)
            {
                if (Time.time >= nextBurnHolyTime)
                {
                    //spawnear la chispa indicando que si hubo daño

                    AudioManager.instance.PlayAudio(AudioManager.instance.makeDamageBoss);

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
