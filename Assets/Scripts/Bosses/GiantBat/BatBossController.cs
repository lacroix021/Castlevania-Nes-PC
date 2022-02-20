using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossController : MonoBehaviour
{
    [Header("Tipo de Movimiento")]
    public int typeMove;
    //random timers to movements
    float nextMoveTime;
    public float moveRate;

    [Header("Variables Modificables")]
    public float moveSpeed;
    public float moveSpeedAttack;
    float speed;
    Vector2 startPosition;
    public bool inRange;
    public float range;
    float rangeLive;
    public float bigRange;
    public LayerMask layerPlayer;
    Animator anim;
    SpriteRenderer sprBoss;


    //disparo
    public GameObject bulletPrefab;
    public Transform fireEmiter;
    float nextAttackTime;
    public float attackRate;    //0.7f
    public float Force;
    float initialForce;
    Transform playerPos;
    public Vector3 targetOrientation;

    //embestida
    float nextTackleTime;
    public float tackleRate;
    Vector2 newPosition;

    //movimiento normal
    float nextMotionTime;
    public float motionRate;
    float randX;
    float randY;

    //invoking
    public Transform pointA;
    public Transform pointB;
    public GameObject batPrefab;
    float nextInvokeTime;
    public float invokeRate;    //0.7f

    //healing
    public bool healing;
    float currentDamageTime;
    public float damageTime;    //0.8f
    HealthBoss bossHealth;
    float vidaACurar;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rangeLive = range;
        playerPos = GameObject.FindGameObjectWithTag("HeadPlayer").transform;
        startPosition = transform.position;
        speed = moveSpeed;
        bossHealth = GetComponent<HealthBoss>();
        sprBoss = GetComponent<SpriteRenderer>();
        initialForce = Force;
    }
    //ponerle vida, depronto una dificultad, entre menos vida tenga mas rapido castee los poderes
    //agregar los colliders y triggers respectivos
    //hacer que se cure si vuelve a Idle
    //hacer que si mata al jugador vuelva a su posicion inicial y se cure

    // Update is called once per frame
    void Update()
    {
        GeneratorMovement();
        DetectorRange();
        HealingBoss();
        ColorControl();

        if (inRange)
        {
            if (typeMove <= 4)
            {
                Movement();
            }
            else if (typeMove > 9 && typeMove <= 14)
            {
                AttackMove();
            }
            else if (typeMove > 14 && typeMove <= 19)
            {
                InvokeLegion();
            }
        }
        else
        {
            ReturnStartPosition();
        }
    }

    private void FixedUpdate()
    {
        if (inRange)
        {
            if (typeMove > 4 && typeMove <= 9)
            {
                Fire();
            }
        }
    }
    void DetectorRange()
    {
        inRange = Physics2D.OverlapCircle(transform.position, rangeLive, layerPlayer);

        if (inRange)
        {
            healing = false;
            rangeLive = bigRange;
            anim.SetBool("Idle", false);
            GameObject.Find("Stage1Music").GetComponent<ActivateMusic>().battle = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeLive);
    }

    void Fire()
    {
        targetOrientation = playerPos.position - fireEmiter.position;
        anim.SetBool("Attack", false);
        anim.SetBool("Invoke", false);

        if (inRange)
        {
            Debug.DrawRay(transform.position, targetOrientation, Color.green);

            if (Time.time >= nextAttackTime)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);
                GameObject bulletInst = Instantiate(bulletPrefab, fireEmiter.position, Quaternion.identity);
                bulletInst.GetComponent<Rigidbody2D>().AddForce(targetOrientation * Force * Time.deltaTime);
                
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void AttackMove()
    {
        if (Time.time >= nextTackleTime)
        {
            newPosition = playerPos.position;

            nextTackleTime = Time.time + 1f / tackleRate;
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(newPosition.x, newPosition.y), moveSpeedAttack * Time.deltaTime);

        if (Vector2.Distance(transform.position, new Vector2(newPosition.x, newPosition.y)) <= 0.5f)
        {
            //cambiar velocidad, atacar o animacion
            anim.SetBool("Attack", false);
            anim.SetBool("Invoke", false);
        }
        else
        {
            //reset variables
            anim.SetBool("Attack", true);
            anim.SetBool("Invoke", false);
        }
    }

    void Movement()
    {
        //X -1.325    2.192
        //Y -0.498    0.588
        anim.SetBool("Attack", false);
        anim.SetBool("Invoke", false);

        if (Time.time >= nextMotionTime)
        {
            randX = Random.Range(-1.325f, 2f);
            randY = Random.Range(-0.498f, 0.488f);

            nextMotionTime = Time.time + 1f / motionRate;
        }

        
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(randX, randY), moveSpeed * Time.deltaTime);
    }


    void InvokeLegion()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Invoke", true);

        if (Time.time >= nextInvokeTime)
        {
            GameObject subBat = Instantiate(batPrefab, pointA.position, Quaternion.identity);
            GameObject subBatB = Instantiate(batPrefab, pointB.position, Quaternion.identity);

            nextInvokeTime = Time.time + 1f / invokeRate;
        }
    }

    void ReturnStartPosition()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Invoke", false);

        transform.position = Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

        if(transform.position.x == startPosition.x && transform.position.y == startPosition.y)
        {
            anim.SetBool("Idle", true);
            rangeLive = range;
            healing = true;
        }
    }

    void GeneratorMovement()
    {
        if (inRange)
        {
            if (Time.time >= nextMoveTime)
            {
                typeMove = Random.Range(0, 20);

                nextMoveTime = Time.time + 1f / moveRate;
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

    void ColorControl()
    {
        if (bossHealth.currentHealth < 40 * bossHealth.maxHealth / 100)
        {
            sprBoss.color = Color.red;
            Force = 2000;
        }
        else if (bossHealth.currentHealth > 40 * bossHealth.maxHealth / 100)
        {
            sprBoss.color = Color.white;
            Force = initialForce;
        }
    }
}
