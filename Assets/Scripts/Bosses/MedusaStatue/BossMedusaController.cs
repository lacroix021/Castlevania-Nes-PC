using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMedusaController : MonoBehaviour
{
    public float radiusAttack;
    public bool playerInRange;
    public LayerMask playerLayer;

    Transform startPosition;
    MedusaStatueController medusaStatueController;
    HealthPlayer hPlayer;

    public float moveSpeed;
    public float direction;
    Rigidbody2D rb;
    Transform playerPos;

    public GameObject snakePrefab;
    public Transform snakeSpawnA, snakeSpawnB;
    public bool snakeAttack;
    public bool moveHead;


    GameObject snakeInstance;
    GameObject snakeInstanceB;

    float nextAttackTime;
    public float attackRate;

    float nextSnakeTime;
    public float snakeRate;
    public int typeMove;

    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float timer, ySen;
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = GameObject.Find("SpawnMedusaHead").transform;
        medusaStatueController = GameObject.Find("MedusaStatue").GetComponent<MedusaStatueController>();
        hPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRange();

        TypeofMoves();

        if (!playerInRange || hPlayer.currentHealth <= 0)
        {
            typeMove = 2;
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                typeMove = Random.Range(0, 2);

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (playerPos.position.x > transform.position.x)
        {
            direction = 1;
        }
        else if (playerPos.position.x < transform.position.x)
        {
            direction = -1;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (moveHead && playerInRange)
        {
            //movimiento senoidal
            timer = timer + (frecuency / 100);
            ySen = Mathf.Sin(timer);

            //movimiento horizontal y senoidal aplicado al RB
            rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, (ySen * cycleWidth) * Time.fixedDeltaTime);
        }
    }

    void SerpentSpawn()
    {
        if (snakeAttack)
        {
            snakeAttack = false;
            rb.velocity = Vector2.zero;

            if (Time.time >= nextSnakeTime)
            {
                //spawn snake Left
                snakeInstance = Instantiate(snakePrefab, snakeSpawnA.position, Quaternion.identity);
                snakeInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * 150 * Time.deltaTime, 120 * Time.fixedDeltaTime), ForceMode2D.Impulse);
                snakeInstance.GetComponent<Snake>().dirSnake = -1;
                Destroy(snakeInstance, 6);

                //spawn snake Right
                snakeInstanceB = Instantiate(snakePrefab, snakeSpawnB.position, Quaternion.identity);
                snakeInstanceB.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * 150 * Time.deltaTime, 120 * Time.fixedDeltaTime), ForceMode2D.Impulse);
                snakeInstanceB.GetComponent<Snake>().dirSnake = 1;
                Destroy(snakeInstanceB, 6);

                nextSnakeTime = Time.time + 1f / snakeRate;
            }

            StartCoroutine(Seconds());
        }
    }

    void TypeofMoves()
    {
        if(typeMove == 0)
        {
            moveSpeed = 20;
            moveHead = true;
        }
        else if(typeMove == 1)
        {
            moveSpeed = 0;
            moveHead = false;
            snakeAttack = true;
            SerpentSpawn();
        }
        else if(typeMove == 2)
        {
            moveHead = false;
            snakeAttack = false;
            rb.velocity = Vector2.zero;

            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, 0.3f * Time.deltaTime);

            if (transform.position == startPosition.position)
            {
                medusaStatueController.anim.SetBool("Spawned", false);
                medusaStatueController.collActivator.enabled = true;
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Seconds()
    {
        yield return new WaitForSeconds(0.8f);
        typeMove = 0;
    }

    public void PlayerRange()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, radiusAttack, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }
}
