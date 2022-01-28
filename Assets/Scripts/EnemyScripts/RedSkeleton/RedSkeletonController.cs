using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSkeletonController : MonoBehaviour
{
    public float moveSpeed;
    public float direction;

    public bool thereIsWall;
    public bool noHole;
    public bool isGrounded;

    public Collider2D wallDetector;
    public Collider2D holeDetector;
    public Collider2D feetPos;
    Collider2D mycollider;
    public LayerMask layerGround;

    Rigidbody2D rb;
    Animator anim;

    float nextFlipTime;
    public float flipRate;

    float nextTypeMoveTime;
    public float typeMoveRate;
    public int typeMove;
    public bool die;
    public GameObject sparkDamage;
    public Collider2D playerColl;
    public bool muriendo;
    public bool reconstruyendo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mycollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TypeMove();
        ChangeDirection();
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Die", die);
        muriendo = anim.GetCurrentAnimatorStateInfo(0).IsTag("RedSkeletonDie");
        reconstruyendo = anim.GetCurrentAnimatorStateInfo(0).IsTag("RedSkeletonRevive");
    }

    private void FixedUpdate()
    {
        Movement();
        Checkers();
    }
    private void OnEnable()
    {
        playerColl = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    void Movement()
    {
        if (!die && !muriendo && !reconstruyendo)
        {
            if (typeMove == 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else if (typeMove == 1)
            {
                rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);
            }
        }
    }

    void ChangeDirection()
    {
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

    void Checkers()
    {
        //flip
        if (direction == 1)
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
    }

    void TypeMove()
    {
        if (Time.time >= nextTypeMoveTime)
        {
            typeMove = Random.Range(0, 2);

            nextTypeMoveTime = Time.time + 1f / typeMoveRate;
        }
    }

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(2);
        die = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);
            GameObject spark = Instantiate(sparkDamage, transform.position, Quaternion.identity);
            Destroy(spark, 0.3f);
            die = true;
            StartCoroutine(Revive());
        }
    }

    public void Muerto()
    {
        mycollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void Reconstruido()
    {
        mycollider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
