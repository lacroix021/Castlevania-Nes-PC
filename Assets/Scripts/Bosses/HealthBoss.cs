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
        BossMummyB,
        BossFranky,
        BossDeath,
        BossDracula,
        BeastDracula
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

    float boundX;
    float boundY;

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
            else if (boss == typeBoss.BossFranky)
            {
                isDead = true;
                
                if (GetComponentInParent<FrankenController>().igorBossInstance)
                {
                    GetComponentInParent<FrankenController>().igorBossInstance.GetComponent<Health>().currentHealth = 0;
                    GetComponentInParent<FrankenController>().igorBossInstance.GetComponent<Health>().HealthCheck();
                }
                else
                    return;
                
                GameObject.Find("Stage4BMusic").GetComponent<ActivateMusic>().battle = false;  //descomentar al poner en el nivel real
            }
            else if (boss == typeBoss.BossDeath)
            {
                isDead = true;
                GameObject.Find("Stage5Music").GetComponent<ActivateMusic>().battle = false;    //se apaga el modo batalla para que la musica vuelva a la normalidad

            }
            else if (boss == typeBoss.BossDracula)
            {
                //descomentar la linea de abajo ya que esta desactiva la musica de batalla cuando se finalicen las pruebas
               
                isDead = true;
                //GameObject.Find("Stage7Music").GetComponent<ActivateMusic>().battle = false;    //se apaga el modo batalla para que la musica vuelva a la normalidad
            }
            else if (boss == typeBoss.BeastDracula)
            {
                isDead = true;
                //se apaga el modo batalla para que la musica vuelva a la normalidad
                //GameObject.Find("Stage7Music").GetComponent<ActivateMusic>().battle = false;

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
            if(boss != typeBoss.BossDracula)
            {
                GameObject instance = Instantiate(deadEffect, transform.position, Quaternion.identity);
            }

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
            else if (boss == typeBoss.BossDracula)
            {
                //minievento para que la cabeza de dracula salga volando al morir el cuerpo
                //e inmediatamente se invoque el boss final dracula en modo bestia desde el script de dracula controller
                //con la funcion BodyDead
                float direction = 0;

                if (transform.eulerAngles.y == 180)
                    direction = -1;
                else
                    direction = 1;

                GameObject headDracula = GameObject.Find("HeadDracula");
                headDracula.transform.parent = null;
                headDracula.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                headDracula.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                headDracula.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 250 * Time.fixedDeltaTime, 250 * Time.fixedDeltaTime), ForceMode2D.Impulse);
                GetComponent<DraculaController>().BodyDead();

            }
            else if (boss == typeBoss.BossMummyB)
            {
                //poner evento de momia si lo hay
            }
            

            //poner algo en el pit para que valga la pena el retroceso

            if(boss != typeBoss.BossDracula)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        HealthCheck();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Weapon") && currentHealth > 0 && col.GetComponent<DamagePlayer>())
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.makeDamageBoss);
            
            TakeDamage(col.GetComponent<DamagePlayer>().damage);
            //spawnear la chispa indicando que si hubo daño
            //GameObject spark = Instantiate(sparkDamage, transform.position, Quaternion.identity);
            boundX = mycollider.bounds.min.x - col.transform.position.x / 100;

            if (col.transform.position.y < mycollider.bounds.min.y)
                boundY = mycollider.bounds.min.y - col.transform.position.y / 48;
            else if (col.transform.position.y > mycollider.bounds.min.y)
                boundY = col.transform.position.y + 0.2f;

            GameObject spark = Instantiate(sparkDamage, new Vector2(boundX, boundY), Quaternion.identity);
            Destroy(spark, 0.3f);
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
            
            TakeDamage(col.GetComponent<DamageSubWeapon>().damage);
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

                    TakeDamage(collision.GetComponent<DamageSubWeapon>().damage);
                    Death();
                    nextBurnHolyTime = Time.time + 1f / burnRate;
                }
            }
        }
    }
}
