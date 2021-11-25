using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public float timer;
    public float timeRestradius;
    //public bool patrolling;

    public float moveSpeed;
    public float jumpForce;
    public float throwForceX;
    public float throwForceY;
    public float direction;
    
    public float radius;
    public LayerMask layerPlayer;
    public bool inRange;



    public bool thereIsWall;
    public bool noHole;
    public bool isGrounded;

    public bool isJump;
    public bool isAttack;

    public Collider2D wallDetector;
    public Collider2D holeDetector;
    public Collider2D feetPos;

    public LayerMask layerGround;
    


    Rigidbody2D rb;
    Animator anim;

    float nextFlipTime;
    public float flipRate;

    Transform playerPos;


    /**/
    public Transform boneHand;

    float nextTypeMoveTime;
    public float typeMoveRate;
    public int typeMove;

    public GameObject bonePrefab;
    GameObject boneThrowed;

    // Start is called before the first frame update
    void Start()
    {
        
        boneHand.GetComponent<SpriteRenderer>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
    }

    private void FixedUpdate()
    {
        Movement();
        Checkers();
        Jump();
        AttackBone();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    void Movement()
    {
        if (rb.velocity.x != 0)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
        if(!inRange)
            rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);


        if (inRange)
        {
            //movimiento
            if (typeMove == 0)
            {
                rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);
                //si encuentro una pared y estoy en el piso y estoy en rango, cambia el tipo de movimiento a ataque a distancia
                if (thereIsWall && isGrounded)
                {
                    typeMove = 1;
                }
                //si encuentro un hueco, estoy en el piso y estoy en rango, SALTO!
                if(!noHole && isGrounded)
                {
                    isJump = true;
                }
            }
        }
    }

    void ChangeDirection()
    {
        if (!inRange)
        {
            //patrullaje
            //si hay pared y estoy en el piso, cambio de direccion
            if (thereIsWall && isGrounded)
            {
                if (direction == 1)
                {
                    if (Time.time >= nextFlipTime)
                    {
                        direction = -1;
                        nextFlipTime = Time.time + 1f / flipRate;
                    }
                }
                else
                {
                    if (Time.time >= nextFlipTime)
                    {
                        direction = 1;
                        nextFlipTime = Time.time + 1f / flipRate;
                    }
                }
            }

            //si encuentra un hueco y esta caminando, cambio de direccion
            if (!noHole && isGrounded)
            {
                if (direction == 1)
                {
                    if (Time.time >= nextFlipTime)
                    {
                        direction = -1;
                        nextFlipTime = Time.time + 1f / flipRate;
                    }
                }
                else
                {
                    if (Time.time >= nextFlipTime)
                    {
                        direction = 1;
                        nextFlipTime = Time.time + 1f / flipRate;
                    }
                }
            }
        }
        else
        {
            //SI ESTA EN RANGO

            playerPos = GameObject.FindGameObjectWithTag("HeadPlayer").GetComponent<Transform>();

            TypeMove();
            //escoger la direccion del jugador
            if (playerPos.position.x < transform.position.x)
            {
                direction = -1;
            }
            else if (playerPos.position.x > transform.position.x)
            {
                direction = 1;
            }
            

            if (playerPos.position.x < transform.position.x)
            {
                direction = -1;
            }
            else if(playerPos.position.x > transform.position.x)
            {
                direction = 1;
            }
        }
    }

    void Checkers()
    {
        //flip
        if(direction == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        isGrounded = Physics2D.IsTouchingLayers(feetPos, layerGround);
        noHole = Physics2D.IsTouchingLayers(holeDetector, layerGround);
        thereIsWall = Physics2D.IsTouchingLayers(wallDetector, layerGround);
        inRange = Physics2D.OverlapCircle(transform.position, radius, layerPlayer);

    }

    void Jump()
    {
        if (isJump)
        {
            //si encuentro un hueco estando en rango, como estoy persiguiendo entonces, SALTO
            if (!noHole && isGrounded)
            {
                isJump = false;
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator RestoreRadius()
    {
        yield return new WaitForSeconds(timeRestradius);
        radius = 1.4f;
    }

    void AttackBone()
    {
        if (isAttack)
        {
            boneHand.GetComponent<SpriteRenderer>().enabled = true;

            if (!boneThrowed)
            {
                
                timer += Time.deltaTime;

                if(timer >= 0.5f)
                {
                    isAttack = false;
                    throwForceX = Random.Range(40, 100);
                    throwForceY = Random.Range(80, 150);
                    boneThrowed = Instantiate(bonePrefab, boneHand.position, Quaternion.identity);
                    boneHand.GetComponent<SpriteRenderer>().enabled = false;

                    boneThrowed.GetComponent<Rigidbody2D>().AddForce(
                        new Vector2(direction * throwForceX * Time.fixedDeltaTime, throwForceY * Time.fixedDeltaTime)
                        , ForceMode2D.Impulse);
                    radius = 0;
                    StartCoroutine(RestoreRadius());
                    timeRestradius = Random.Range(0.7f, 1.8f);
                    typeMove = 0;
                    timer = 0;
                }
            }
        }
    }
    

    void TypeMove()
    {
        if (inRange)
        {
            if (Time.time >= nextTypeMoveTime)
            {
                typeMove = Random.Range(0, 3);

                nextTypeMoveTime = Time.time + 1f / typeMoveRate;
            }
        }

        if(typeMove != 0)
        {
            isAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
