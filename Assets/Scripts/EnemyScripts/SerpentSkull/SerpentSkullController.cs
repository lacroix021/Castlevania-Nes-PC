using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentSkullController : MonoBehaviour
{
    public float moveSpeed;

    float nextMoveTime;
    public float moveRate;

    Animator anim;

    [Header("INFORMATIVO")]
    public Vector3 posActual;

    float minLimitX = -0.45f;
    float maxLimitX = -0.1f;
    float minLimitY = -0.48f;
    float maxLimitY = 0.04f;
    public float moveY;
    public float moveX;
    public float directionY;
    public float directionX;

    public int typeMove;


    float nextTypeMoveTime;
    public float typeMoveRate;

    float nextAttackTime;
    public float attackRate;

    [Header("SPIT ATTACK")]
    
    public Transform fangsPos;
    public GameObject bulletPrefab;
    GameObject instanceBullet;
    Rigidbody2D rb;
    
    public Transform tail;
    public float myDirecton;
    Vector3 newPos;

    //
    public Vector3 targetOrientation;
    Transform playerPos;
    public bool inRange;
    public float range;
    public float force;
    public LayerMask layerPlayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("HeadPlayer").transform;

        moveY = 0;
        moveX = 0;
        directionY = -1;


        if(tail.position.x < transform.position.x)
        {
            myDirecton = 1;
        }
        else if(tail.position.x > transform.position.x)
        {
            myDirecton = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTypeMoveTime)
        {
            typeMove = Random.Range(0, 10);

            nextTypeMoveTime = Time.time + 1f / typeMoveRate;
        }

        RangeDetector();
    }

    private void FixedUpdate()
    {

        if(typeMove >= 4)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            MovementHead();

            moveY += directionY * moveSpeed * Time.deltaTime;
            moveX += directionX * moveSpeed * Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x + moveX, transform.localPosition.y + moveY, 0);
            Vector3 newPos = transform.localPosition;
        }
        else if(typeMove < 4)
        {
            rb.bodyType = RigidbodyType2D.Static;
            Attack();
        }
    }
        

    void MovementHead()
    {
        //limite de Y
        if(transform.localPosition.y >= maxLimitY)
        {
            directionY = -1;
        }
        else if (transform.localPosition.y <= minLimitY)
        {
            directionY = 1;
        }

        //limite de X
        if(transform.localPosition.x >= maxLimitX)
        {
            directionX = -1;
        }
        else if(transform.localPosition.x <= minLimitX)
        {
            directionX = 1;
        }

        if (Time.time >= nextMoveTime)
        {
            float numX = Random.Range(-1, 2);
            float numY = Random.Range(-1, 2);
            moveSpeed = Random.Range(0.1f, 0.5f);
            directionX = numX;
            directionY = numY;

            nextMoveTime = Time.time + 1f / moveRate;
        }
    }

    
    void Attack()
    {
        if (inRange)
        {
            if (Time.time >= nextAttackTime)
            {
                anim.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    /*
    public void SpitFire()
    {
        AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);

        instanceBullet = Instantiate(bulletPrefab, fangsPos.position, Quaternion.identity);
        instanceBullet.GetComponent<SpitController>().direction = myDirecton;
    }
    */

    public void Fire()
    {
        targetOrientation = playerPos.position - fangsPos.position;
        Debug.DrawRay(transform.position, targetOrientation, Color.green);

        AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);
        instanceBullet = Instantiate(bulletPrefab, fangsPos.position, Quaternion.identity);
        instanceBullet.GetComponent<Rigidbody2D>().AddForce(targetOrientation * force * Time.deltaTime);
    }

    void RangeDetector()
    {
        inRange = Physics2D.OverlapCircle(fangsPos.position, range, layerPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(fangsPos.position, range);
    }
}
