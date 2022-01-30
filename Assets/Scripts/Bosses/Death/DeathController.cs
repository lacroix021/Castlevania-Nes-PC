using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public int typeMove;
    Rigidbody2D rb;

    //X -1.32f, 1.32f
    //Y -0.56f, 0.56f

    [SerializeField] float posX;
    [SerializeField] float posY;

    float nextRandomPosTime;
    public float randomPosRate;
    Transform playerPos;
    SpriteRenderer spr;
    Collider2D myColl;
    public float t;
    public bool visible;

    public Transform boundaryFather;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;

        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spr = GetComponent<SpriteRenderer>();
        myColl = GetComponent<Collider2D>();
        boundaryFather = GameObject.Find("Boundary").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Invisible();

        t = Mathf.Clamp(t, 0, 1);
        spr.color = new Vector4(1, 1, 1, t);

        //control de visibilidad y colision
        if (t >= 1)
        {
            visible = true;
            
        }
        else if(t >= 0.5f)
        {
            myColl.enabled = true;
        }
        else
        {
            visible = false;
            myColl.enabled = false;
        }

        //control tipo de movimiento
        if(typeMove == 0)
        {
            t -= Time.deltaTime;
        }
        else if(typeMove == 1)
        {
            t += Time.deltaTime;
        }
    }

    void RandomPos()
    {
        if (Time.time >= nextRandomPosTime)
        {
            string tempPosX = Random.Range(-1.32f, 1.33f).ToString("F2");
            string tempPosY = Random.Range(-0.56f, 0.57f).ToString("F2");

            posX = float.Parse(tempPosX);
            posY = float.Parse(tempPosY);

            nextRandomPosTime = Time.time + 1f / randomPosRate;
        }
    }

    void Invisible()
    {
        //mientras esta invisible cambia de posicion
        if (t <= 0)
        {
            RandomPos();
            transform.localPosition = new Vector3(posX, posY, 0);
        }
    }

    void Attack()
    {

    }


    void Flip()
    {
        if (playerPos.position.x - 0.4f > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (playerPos.position.x + 0.4f < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
