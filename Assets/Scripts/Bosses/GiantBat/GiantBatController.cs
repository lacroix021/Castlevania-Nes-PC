using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBatController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float moveAttack = 0.9f;
    private float moveBase;
    public float radius = 2;
    public LayerMask playerMask;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprBoss;

    GameObject playerPos;
    HealthPlayer playerHealth;
    private bool inRange;

    public float posX;
    public float posY;

    private float nextTypeMovTime;
    public float typeMovRate= 0.5f;

    [SerializeField] private int num;

    /*
     typemove 0 = randomplace
     typemove 1 = playerposition place
     typemove 2 = firing bullets
    */
    public Transform fireSpawner;
    private GameObject bulletSpawned;
    public GameObject prefabBullet;

    private float nextFireTime;
    public float fireRate = 1.2f;

    HealthBoss bossHealth;
    public bool idle;


    public bool healing;

    float currentDamageTime;
    public float damageTime;
    public float vidaACurar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveBase = moveSpeed;
        sprBoss = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerPos = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerPos.GetComponent<HealthPlayer>();
        bossHealth = GetComponent<HealthBoss>();
        idle = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Idle", idle);
        RangeDetector();

        TypeOfMove();
        HealingBoss();
    }

    private void FixedUpdate()
    {
        MovementBat();
    }

    void MovementBat()
    {
        if (inRange && anim.GetBool("Enable"))
        {
            idle = false;
            healing = false;
            //vida mayor del 40% y el jugador no esta muerto
            if (bossHealth.currentHealth > 40 * bossHealth.maxHealth / 100 && !playerHealth.isDead)
            {
                if (num == 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, posY, 0), moveSpeed * Time.deltaTime);
                }
                else if (num == 1 || num == 3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, posY, 0), moveAttack * Time.deltaTime);
                }
                else if (num == 2)
                {
                    rb.velocity = new Vector2(0, 0);
                    //disparar bala de fuego a la posicion del jugador
                    FireBullet();
                }
            }
            //vida menor del 40% y el jugador no esta muerto
            else if(bossHealth.currentHealth <= 40 * bossHealth.maxHealth / 100 && !playerHealth.isDead)
            {
                if (num == 1 || num == 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, posY, 0), moveAttack * Time.deltaTime);
                }
                else if (num == 2 || num == 3)
                {
                    rb.velocity = new Vector2(0, 0);
                    //disparar bala de fuego a la posicion del jugador
                    FireBullet();
                }
            }
            //el jugador murio y esta en rango
            else if (playerHealth.isDead)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.92f, 0), moveSpeed * Time.deltaTime);
                num = 6;
                if (transform.position.y == 0.92f)
                {
                    idle = true;

                    if (bossHealth.currentHealth < bossHealth.maxHealth)
                    {
                        healing = true;
                    }
                }
            }
                
        }
        else if (!inRange)
        {
            //el jugador no esta muerto
            if (!playerHealth.isDead)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.92f, 0), moveSpeed * Time.deltaTime);
                num = 6;
                if(transform.position.y == 0.92f)
                {
                    idle = true;

                    if(bossHealth.currentHealth < bossHealth.maxHealth)
                    {
                        healing = true;
                    }
                }
            }
            else if (playerHealth.isDead && num == 6)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.92f, 0), moveSpeed * Time.deltaTime);

                if (transform.position.y == 0.92f)
                {
                    idle = true;

                    if (bossHealth.currentHealth < bossHealth.maxHealth)
                    {
                        healing = true;
                    }
                }
            }
        }
    }

    void HealingBoss()
    {
        if (healing)
        {
            currentDamageTime += Time.deltaTime;

            if (currentDamageTime > damageTime)
            {
                bossHealth.currentHealth++;
                vidaACurar--;

                currentDamageTime = 0;
            }

            vidaACurar = bossHealth.maxHealth;
            bossHealth.HealthCheck();
        }
        else
        {
            vidaACurar = 0;
        }

        if (vidaACurar == 0 || bossHealth.currentHealth == bossHealth.maxHealth)
        {
            healing = false;
        }
    }


    void TypeOfMove()
    {
        //mas dificultad cuando el boss llega al 40% de vida
        if(bossHealth.currentHealth < 40 * bossHealth.maxHealth / 100)
        {
            moveSpeed = 1f;
            moveAttack = 1.5f;
            sprBoss.color = Color.red;
            fireRate = 1.8f;
        }
        else if(bossHealth.currentHealth > 40 * bossHealth.maxHealth / 100)
        {
            moveSpeed = 0.5f;
            moveAttack = 0.9f;
            sprBoss.color = Color.white;
            fireRate = 1.2f;
        }

        if (!playerHealth.isDead)
        {
            if (Time.time >= nextTypeMovTime)
            {
                num = Random.Range(0, 4);

                if (bossHealth.currentHealth > 40 * bossHealth.maxHealth / 100)
                {
                    if (num == 0 )
                    {
                        RandomPlace();
                    }
                    else if (num == 1 || num == 3)
                    {
                        PlayerPlace();
                    }
                    else if (num == 2)
                    {
                        PlayerPlace();
                    }
                }
                else if (bossHealth.currentHealth <= 40 * bossHealth.maxHealth / 100)
                {

                    if (num == 1 || num == 0)
                    {
                        PlayerPlace();
                    }
                    else if (num == 2 || num == 3)
                    {
                        PlayerPlace();
                    }
                }

                nextTypeMovTime = Time.time + 1f / typeMovRate;
            }
        }
        else
        {
            num = 6;
        }
    }

    void RandomPlace()
    {
        posX = Random.Range(25, 32);
        posY = Random.Range(-0.2f, 1f);
    }

    void PlayerPlace()
    {
        posX = playerPos.transform.position.x;
        posY = playerPos.transform.position.y;
    }


    void FireBullet()
    {
        if (Time.time >= nextFireTime)
        {
            PlayerPlace();
            bulletSpawned = Instantiate(prefabBullet, fireSpawner.position, Quaternion.identity);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
    

    void RangeDetector()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, playerMask);

        if (inRange && bossHealth.currentHealth > 40 * bossHealth.maxHealth / 100)
        {
            radius = 2.2f;
            anim.SetBool("Enable", true);
        }
        else if(inRange && bossHealth.currentHealth < 40 * bossHealth.maxHealth / 100)
        {
            radius = 3f;
        }
    }

    private void OnDrawGizmos()
    {
        if(inRange)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
