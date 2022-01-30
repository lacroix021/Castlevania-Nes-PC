using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeKnightController : MonoBehaviour
{
    [Header("TIPO DE MOVIMIENTO")]
    public int typeMove;

    [Header("VARIABLES MODIFICABLES")]
    public float moveSpeed;
    float direction;
    float numDirection;
    public float forceThrow;
    public float forceUp;

    public GameObject axeFloatPrefab;
    GameObject axeFlInstance;
    public GameObject axeGravityPrefab;
    GameObject axeGrInstance;

    public Transform axePosition;
    public GameObject shield;

    Rigidbody2D rb;
    Animator anim;

    //timers
    float nextMoveTime;
    public float moveRate;

    float nextDirectionTime;
    public float directionRate;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("VelX", Mathf.Abs(rb.velocity.x));

        if (Time.time >= nextMoveTime)
        {
            typeMove = Random.Range(0, 2);
            
            nextMoveTime = Time.time + 1f / moveRate;
        }

        if (Time.time >= nextDirectionTime)
        {
            numDirection = Mathf.Abs(Random.Range(0, 10));
            nextDirectionTime = Time.time + 1f / directionRate;
        }

        
        if (typeMove == 0)
        {
            int num = Random.Range(0, 6);
            if(num <= 2)
                TrowAxeFloat();
            else if(num >= 3)
                DropAxeGravity();
        }
        else if(typeMove == 1)
        {
            
            Movement();
        }
    }

    void TrowAxeFloat()
    {
        StartCoroutine(Lanza());
    }

    IEnumerator Lanza()
    {
        yield return new WaitForSeconds(0.8f);
        if (!axeFlInstance)
        {
            shield.SetActive(false);
            axeFlInstance = Instantiate(axeFloatPrefab, axePosition.position, Quaternion.identity);
            axeFlInstance.GetComponent<AxesControl>().dirTemp = direction;
            StartCoroutine(RestoreMove());
        }
        else
        {
            typeMove = 1;
        }
    }

    IEnumerator RestoreMove()
    {
        yield return new WaitForSeconds(0.8f);
        typeMove = 1;
        shield.SetActive(true);
    }

    void Movement()
    {
        if (numDirection <= 4)
            direction = -1;
        else if (numDirection >= 5)
            direction = 1;


        //flip
        if (direction == -1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction == 1)
            transform.localScale = new Vector3(-1, 1, 1);

        rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y); 
    }

    void DropAxeGravity()
    {
        if (!axeGrInstance)
        {
            forceThrow = Random.Range(50, 80);
            forceUp = Random.Range(50, 150);

            shield.SetActive(false);
            axeGrInstance = Instantiate(axeGravityPrefab, axePosition.position, Quaternion.identity);
            axeGrInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * forceThrow * Time.fixedDeltaTime, forceUp * Time.fixedDeltaTime), ForceMode2D.Impulse);
            StartCoroutine(RestoreMove());
        }
    }
}
