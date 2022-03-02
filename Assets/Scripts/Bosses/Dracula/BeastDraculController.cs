using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastDraculController : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    public float direction;

    public Collider2D feetPos;
    public bool isGrounded;
    public LayerMask thisGround;

    Rigidbody2D rb;
    Animator anim;
    Transform playerPos;

    //jumps
    float nextJumpTime;
    public float jumpRate;
    bool isJump;
    public Transform spitEmiter;
    public GameObject spitPrefab;
    float nextSpitTime;
    public float spitRate;

    //Fire
    public float radius;
    public bool inRange;
    public LayerMask layerPlayer;
    public Transform fireEmiter;
    float nextFireTime;
    public float fireRate;
    public GameObject firePrefab;
    public float force;

    //type movements
    float typeMoveTime;
    public float moveRate;
    public int typeMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
        Flip();
        RangeDetector();

        if(Time.time >= typeMoveTime)
        {
            typeMove = Random.Range(0, 3);

            typeMoveTime = Time.time + 1 / moveRate;
        }

        if(typeMove == 0 || typeMove == 1)
        {
            Inputs();
            Spit();
        }
        else if(typeMove == 2)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        Jump();
    }

    void Inputs()
    {
        if (Time.time >= nextJumpTime)
        {
            isJump = true;

            nextJumpTime = Time.time + 1f / jumpRate;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(feetPos, thisGround);
    }

    void Animations()
    {
        anim.SetFloat("VelY", rb.velocity.y);
        anim.SetBool("Grounded", isGrounded);
    }

    void Jump()
    {
        if (isJump)
        {
            isJump = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(direction * moveSpeed * Time.deltaTime, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }

        //limite de velocidad de caida
        if (rb.velocity.y <= -0.7f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -0.7f);
        }
    }

    void Flip()
    {
        if(playerPos.position.x > transform.position.x + 0.2f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            direction = 1;
        }
        else if(playerPos.position.x < transform.position.x - 0.2f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = -1;
        }
    }

    void Spit()
    {
        if (!isGrounded)
        {
            if(Time.time >= nextSpitTime)
            {
                Instantiate(spitPrefab, spitEmiter.position, Quaternion.identity);

                nextSpitTime = Time.time + 1f/ spitRate;
            }
        }
    }

    void Fire()
    {
        Vector3 targetOrientation = playerPos.position - fireEmiter.position;

        if (inRange)
        {
            Debug.DrawRay(transform.position, targetOrientation, Color.green);

            if (Time.time >= nextFireTime)
            {
                anim.SetTrigger("Fire");
                AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);
                GameObject bulletInst = Instantiate(firePrefab, fireEmiter.position, Quaternion.Euler(0,0,-90));
                bulletInst.GetComponent<Rigidbody2D>().AddForce(targetOrientation * force * Time.fixedDeltaTime);
                if(direction == 1)
                {
                    bulletInst.transform.eulerAngles = new Vector3(0, 0, 90);
                }

                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void RangeDetector()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, layerPlayer);

        if (inRange)
        {
            //activar la musica de batalla final
            GameObject.Find("Stage7Music").GetComponent<ActivateMusic>().battle = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
