using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermanController : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;

    [SerializeField] int direction;

    Rigidbody2D rb;
    BoxCollider2D myColl;
    Animator anim;

    [SerializeField] bool isGrounded;
    [SerializeField] bool wallDetected;

    public BoxCollider2D feetPos;
    
    public LayerMask ground;
    

    Transform playerPos;

    [SerializeField] int typeMove;
    float nextMoveTime;
    public float MoveRate;

    /*Disparo de fuego*/
    public Transform canon;
    public GameObject firePrefab;
    GameObject fireBall;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Jump();
        StartCoroutine(ChangeColl());
    }

    // Update is called once per frame
    void Update()
    {

        ActivatorFeet();
        TypeMovement();
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);

        CheckGround();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(feetPos, ground);
        
    }

    IEnumerator ChangeColl()
    {
        myColl.enabled = false;
        yield return new WaitForSeconds(0.4f);
        myColl.enabled = true;
    }

    void Movement()
    {
        if (isGrounded)
        {
            switch (typeMove)
            {
                //0 movimiento random
                case 0:
                    Flip();
                    rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, 0);
                    break;
                //1 ataque al jugador
                case 1:
                    Flip();
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    //disparar fuego
                    Fire();
                    break;
                case 2:
                    Flip();
                    rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, 0);
                    break;
            }
        }
    }

    void Flip()
    {
        if (typeMove == 0 || typeMove == 2)
        {
            if (direction == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            
        }
        else if (typeMove == 1)
        {
            if (playerPos.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                direction = 1;
            }
            else if (playerPos.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                direction = -1;
            }
        }
    }


    void ActivatorFeet()
    {
        if (rb.velocity.y > 0.4f)
        {
            feetPos.enabled = false;
        }
        else if (rb.velocity.y <= 0)
        {
            feetPos.enabled = true;
        }
    }


    void TypeMovement()
    {
        if (Time.time >= nextMoveTime)
        {
            typeMove = Random.Range(0, 3);
            if(typeMove == 0 || typeMove == 2)
                direction = Random.Range(-1, 2);

            nextMoveTime = Time.time + 1f / MoveRate;
        }
    }

    void Fire()
    {
        if (!fireBall)
        {
            anim.SetTrigger("Attack");
            fireBall = Instantiate(firePrefab, canon.position, Quaternion.identity);
            fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * 35 * Time.deltaTime, 0);
        }
    }
}
