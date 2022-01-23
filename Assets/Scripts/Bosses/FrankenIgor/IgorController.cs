using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgorController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float direction;
    public bool isGrounded;
    public LayerMask thisGround;
    public Collider2D feetPos;

    Rigidbody2D rb;
    Animator anim;
    Transform playerPos;

    //disparo
    public GameObject bulletPrefab;
    public float Force;
    public float dropForceX;
    public float dropForceY;
    float nextAttackTime;
    public float attackRate;
    public Transform fireEmiter;
    int tDrop;

    //tipos de botella
    int tItem;
    public GameObject bottleA;
    public GameObject bottleB;
    public GameObject bottleC;
    public GameObject bottleD;

    //saltos
    public PhysicsMaterial2D slide;
    public PhysicsMaterial2D sticky;
    BoxCollider2D myColl;
    float nextJumpTime;
    public float jumpRate;
    int tJump;

    //cantidad de bolitas
    public IgorBallControl[] balls;

    //tipos de movimiento
    int tMove;

    //rates
    float nextJumpTypeTime;
    public float jumpTypeRate;

    float nextMoveTypeTime;
    public float moveTypeRate;

    float nextDropTypeTime;
    public float dropTypeRate;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));
        Moves();
        CheckGround();

        balls = GameObject.FindObjectsOfType<IgorBallControl>();
        
    }

    private void FixedUpdate()
    {
        if(tMove == 0)
            DontMove();
        else if(tMove != 0)
            Movement();

        if (tJump == 0)
            return;
        else if (tJump != 0)
            Jump();

        if (tDrop == 0)
            return;
        else if (tDrop != 0)
            Fire();
    }

    void Fire()
    {
        if (Time.time >= nextAttackTime)
        {
            if(balls.Length < 1)
            {
                anim.SetTrigger("Drop");
            }
            
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    
    public void Drop()
    {
        //citada desde la animacion
        
        if (tItem == 0 || tItem == 5)
        {
            GameObject bulletInst = Instantiate(bulletPrefab, fireEmiter.position, Quaternion.identity);
            bulletInst.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * Force * Time.fixedDeltaTime, Force * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        else if(tItem == 1)
        {
            GameObject bulletInst = Instantiate(bottleA, fireEmiter.position, Quaternion.identity);
            bulletInst.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * dropForceX * Time.fixedDeltaTime, dropForceY * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        else if (tItem == 2)
        {
            GameObject bulletInst = Instantiate(bottleB, fireEmiter.position, Quaternion.identity);
            bulletInst.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * dropForceX * Time.fixedDeltaTime, dropForceY * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        else if (tItem == 3)
        {
            GameObject bulletInst = Instantiate(bottleC, fireEmiter.position, Quaternion.identity);
            bulletInst.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * dropForceX * Time.fixedDeltaTime, dropForceY * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        else if (tItem == 4)
        {
            GameObject bulletInst = Instantiate(bottleD, fireEmiter.position, Quaternion.identity);
            bulletInst.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * dropForceX * Time.fixedDeltaTime, dropForceY * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
    }
    
    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(feetPos, thisGround);
        if (!isGrounded)
            myColl.sharedMaterial = slide;
        else
            myColl.sharedMaterial = sticky;
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Time.time >= nextJumpTime)
            {
                jumpForce = Random.Range(110, 180);
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
                nextJumpTime = Time.time + 1f / jumpRate;
            }
        }
    }

    void Movement()
    {
        if (isGrounded)
        {
            //flip
            if (playerPos.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                direction = -1;
            }
            else if (playerPos.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                direction = 1;
            }

            rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    void DontMove()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Moves()
    {
        if (Time.time >= nextJumpTypeTime)
        {
            tJump = Random.Range(0, 3);

            nextJumpTypeTime = Time.time + 1f / jumpTypeRate;
        }

        if (Time.time >= nextMoveTypeTime)
        {
            tMove = Random.Range(0, 3);

            nextMoveTypeTime = Time.time + 1f / moveTypeRate;
        }

        if (Time.time >= nextDropTypeTime)
        {
            tDrop = Random.Range(0, 3);
            tItem = Random.Range(0, 6); //ampliar segun la cantidad de botellas

            nextDropTypeTime = Time.time + 1f / dropTypeRate;
        }
    }
}
