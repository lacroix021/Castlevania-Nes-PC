using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    
    public float mSpeed;
    public int direction;

    Rigidbody2D rb;
    Animator anim;

    float timeStateRate;
    public float stateRate;

    public BoxCollider2D wallDetector;
    public BoxCollider2D floorDetector;
    bool wallFront;
    bool grounded;

    public LayerMask theGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));

        WallCheck();

        if (!wallFront && grounded)
        {
            if (Time.time >= timeStateRate)
            {
                direction = Random.Range(0, 3);

                timeStateRate = Time.time + 1 / stateRate;
            }
        }
        else if(wallFront && grounded)
        {
            if(direction == 2)
                direction = 1;
            else if(direction == 1)
                direction = 2;
        }
        else if (!grounded)
        {
            if (direction == 2)
                direction = 1;
            else if (direction == 1)
                direction = 2;
        }
    }


    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if(direction == 2)
        {
            rb.velocity = new Vector2(-1 * mSpeed * Time.deltaTime, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(direction == 1)
        {
            rb.velocity = new Vector2(1 * mSpeed * Time.deltaTime, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void WallCheck()
    {
        wallFront = Physics2D.IsTouchingLayers(wallDetector, theGround);
        grounded = Physics2D.IsTouchingLayers(floorDetector, theGround);
    }
}
