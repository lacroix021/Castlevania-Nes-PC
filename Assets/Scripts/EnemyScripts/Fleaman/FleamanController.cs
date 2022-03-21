using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleamanController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float direction;
    public bool isGrounded;
    public LayerMask thisGround;
    public Collider2D feetPos;
    public Collider2D detector;

    float nextJumpTime;
    public float jumpRate;
    public bool isJump;

    Rigidbody2D rb;
    Animator anim;

    public bool inRange;
    public LayerMask layerPlayer;

    Transform playerPos;

    public int typeMove;
    float nextMoveTime;
    public float moveRate;

    public PhysicsMaterial2D slide;
    Collider2D myColl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", isGrounded);
        Moves();
        CheckGround();
        JumpTimer();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(feetPos, thisGround);

        inRange = Physics2D.IsTouchingLayers(detector, layerPlayer);

        if (!isGrounded)
        {
            myColl.sharedMaterial = slide;
        }
        else
        {
            myColl.sharedMaterial = null;
        }
    }

    

    void JumpTimer()
    {
        if (isGrounded && inRange && typeMove != 0)
        {
            if (Time.time >= nextJumpTime)
            {
                isJump = true;
                nextJumpTime = Time.time + 1f / jumpRate;
            }
        }
    }

    void Jump()
    {
        if (isJump)
        {
            isJump = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    void Movement()
    {
        if (typeMove == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(typeMove != 0)
        {
            if (inRange)
            {
                if (isGrounded)
                {
                    playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

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
                }

                rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);
            }
        }
    }

    void Moves()
    {
        if (inRange)
        {
            if (Time.time >= nextMoveTime)
            {
                typeMove = Random.Range(0, 5);
                jumpRate = Random.Range(1, 3);
                nextMoveTime = Time.time + 1f / moveRate;
            }
        }        
    }
}
