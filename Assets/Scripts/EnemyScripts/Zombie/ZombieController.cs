using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float moveSpeed;

    Transform playerPos;
    Rigidbody2D rb;
    Animator anim;
    
    int direction;

    public BoxCollider2D wallDetector;
    private bool touchWall;
    public LayerMask thisWall;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //direccion 1 hacia la derecha 0 para la izquierda
        if (playerPos.position.x > transform.position.x)
        {
            direction = 1;
        }
        else if (playerPos.position.x < transform.position.x)
        {
            direction = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
        ChangeDirection();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    void Animations()
    {
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));

        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Movement()
    {
        if(direction == 1)
        {
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if(direction == 0)
        {
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    void ChangeDirection()
    {
        touchWall = Physics2D.IsTouchingLayers(wallDetector, thisWall);

        if (touchWall)
        {
            if (direction == 0)
                direction = 1;
            else if (direction == 1)
                direction = 0;

            touchWall = false;
        }
    }
}


