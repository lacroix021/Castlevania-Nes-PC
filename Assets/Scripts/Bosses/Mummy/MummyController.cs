using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public bool mummyA;
    public bool mummyB;

    public int typeMove;

    public float moveSpeed;
    public float direction;
    float nextMoveTime;
    public float posCheckRate;


    public GameObject blockPrefab;
    public GameObject ribbonPrefab;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spr;
    Transform posCeilling;
    Transform posPlayer;

    public Transform ribbonThrower;


    public bool drop;
    public int limitBlocks;

    float nextRibbonTime;
    public float ribbonRate;
    public int limitRibbons;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        posCeilling = GameObject.Find("CeilingMummies").GetComponentInParent<Transform>();
        posPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        TypeMove();
        DropBlocks();

        Checkers();

        //configurar metodo para que se active el drop de las puas del techo
        if (typeMove == 0)
        {
            drop = true;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Checkers()
    {
        //direction
        if(posPlayer.position.x < transform.position.x)
        {
            direction = -1;
        }
        else if (posPlayer.position.x > transform.position.x)
        {
            direction = 1;
        }
        //flip
        if(direction == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(direction == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Movement()
    {
        rb.MovePosition(new Vector2(transform.position.x + (direction * moveSpeed* Time.deltaTime), transform.position.y));
    }

    void TypeMove()
    {
        if (Time.time >= nextMoveTime)
        {
            typeMove = Random.Range(0, 3);
            nextMoveTime = Time.time + 1f / posCheckRate;
        }

        if (typeMove == 0)
        {
            moveSpeed = 0;  //atacando
            anim.SetBool("Walk", false);
            StartCoroutine(ChangeColor());
        }
        else if (typeMove != 0)
        {
            moveSpeed = 0.3f;    //moviendose
            anim.SetBool("Walk", true);
        }
    }
    IEnumerator ChangeColor()
    {
        spr.color = Color.cyan;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.white;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.cyan;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.white;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.cyan;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.white;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.cyan;
        yield return new WaitForSeconds(0.03f);
        spr.color = Color.white;
    }

    void DropBlocks()
    {
        if (mummyA)
        {
            BlockMummy[] blocks = GameObject.FindObjectsOfType<BlockMummy>();
            if (drop)
            {
                if (blocks.Length <= limitBlocks)
                {
                    float posX = Random.Range(38.64f, 41.998f);
                    Vector3 newPos = new Vector3(posX, posCeilling.position.y, 0);
                    Instantiate(blockPrefab, newPos, Quaternion.identity);
                }
            }
            if (blocks.Length == limitBlocks)
            {
                drop = false;
            }
        }


        if (mummyB)
        {
            RibbonController[] ribbons = GameObject.FindObjectsOfType<RibbonController>();
            if (drop)
            {
                if(ribbons.Length <= limitRibbons)
                {
                    if (Time.time >= nextRibbonTime)
                    {
                        string posY = Random.Range(0.06f, 0.37f).ToString("F2");
                        
                        ribbonThrower.localPosition = new Vector3(ribbonThrower.localPosition.x, float.Parse(posY), 0);
                        GameObject istance = Instantiate(ribbonPrefab, ribbonThrower.position, Quaternion.identity);
                        istance.GetComponent<RibbonController>().direction = direction;
                        nextRibbonTime = Time.time + 1f / ribbonRate;
                    }
                }
                if(ribbons.Length == limitRibbons)
                {
                    drop = false;
                }
            }
        }
    }
}
