using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    float dir;

    float nextImpulseTime;
    public float impulseRate;

    Animator anim;
    Rigidbody2D rb;
    Transform playerPos;

    [SerializeField] private bool playerInRange;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJump;
    [SerializeField] private bool impulse;
    
    public LayerMask playerMask;
    public LayerMask groundMask;

    public BoxCollider2D feetPos;
    public BoxCollider2D jumper;
    public BoxCollider2D rangeAttack;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerRadius();
        Animations();
        Flip();
        ImpulseTimer();

    }

    private void FixedUpdate()
    {
        MoveEnemy();
        Jump();
    }

    void checkPlayerRadius()
    {
        playerInRange = Physics2D.IsTouchingLayers(rangeAttack, playerMask);
        isGrounded = Physics2D.IsTouchingLayers(feetPos, groundMask);
        isJump = Physics2D.IsTouchingLayers(jumper, groundMask);
    }


    void Flip()
    {
        if (playerPos.position.x > transform.position.x && isGrounded)
        {
            dir = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (playerPos.position.x < transform.position.x && isGrounded)
        {
            dir = -1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (!playerInRange)
        {
            dir = 0;
        }
    }


    void MoveEnemy()
    {
        //limite de velocidad
        if(rb.velocity.x > 1)
        {
            rb.velocity = new Vector2(1, rb.velocity.y);
        }
        else if(rb.velocity.x < -1)
        {
            rb.velocity = new Vector2(-1, rb.velocity.y);
        }



        if (playerInRange)
        {
            rb.AddForce(new Vector2(dir * moveSpeed * Time.deltaTime, rb.velocity.y), ForceMode2D.Force);
        }
    }

    void Animations()
    {
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
    }

    void Jump()
    {
        if (impulse)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
            impulse = false;
        }
    }

    void ImpulseTimer()
    {
        if (!isJump && isGrounded && playerInRange)
        {
            impulse = true;
        }

        if (Vector2.Distance(playerPos.position, transform.position) < 0.4f && isGrounded)
        {
            if (Time.time >= nextImpulseTime)
            {
                impulse = true;

                nextImpulseTime = Time.time + 1f / impulseRate;
            }
        }
    }
            
}
