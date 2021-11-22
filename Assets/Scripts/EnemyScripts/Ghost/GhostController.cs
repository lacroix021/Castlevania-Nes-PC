using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float moveSpeed;

    public float radius;
    public LayerMask playerLayer;
    public bool inRange;

    //
    float nextPosTime;
    public float posRate;

    Vector3 target;
    string posX;
    string posY;
    //

    public bool patrolling;

    Transform posPlayer;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PatrolValues();
        CheckRange();
        Movement();
    }

    void CheckRange()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (inRange)
        {
            patrolling = false;
            FadeIn();
        }
        else
        {
            patrolling = true;
            FadeOut();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Movement()
    {
        if (patrolling)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            posPlayer = GameObject.FindGameObjectWithTag("HeadPlayer").GetComponent<Transform>();            
            transform.position = Vector3.MoveTowards(transform.position, posPlayer.position, moveSpeed * Time.deltaTime);

            if(posPlayer.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (posPlayer.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }

    void PatrolValues()
    {
        if (patrolling)
        {
            if (Time.time >= nextPosTime)
            {
                posX = Random.Range(-2.0f, 2.1f).ToString("F1");
                posY = Random.Range(-1.0f, 1.1f).ToString("F1");
                target = new Vector3(float.Parse(posX), float.Parse(posY), 0);

                nextPosTime = Time.time + 1f / posRate;
            }
        }
        
    }

    private void FadeIn()
    {
        Color c = spr.material.color;
        c.a += 0.05f;
        //para que aparezca pero no supere el limite del canal alpha
        if (c.a >= 1)
        {
            c.a = 1;
        }

        spr.material.color = c;
    }

    private void FadeOut()
    {
        Color c = spr.material.color;
        c.a -= 0.05f;
        // para que desaparezca pero no supere el limite del alpha en negativo
        if (c.a <= 0)
        {
            c.a = 0;
        }

        spr.material.color = c;
    }
}
