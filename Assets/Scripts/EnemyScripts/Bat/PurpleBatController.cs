using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBatController : MonoBehaviour
{
    public float impulse;
    public float moveSpeed;
    public float radius;
    [SerializeField] Vector3 posPlayer;
    public LayerMask layerPlayer;
    Rigidbody2D rb;
    Animator anim;
    float direction;
    bool inRange;
    bool attack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RangeChecker();
    }

    private void FixedUpdate()
    {
        AttackBat();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    void RangeChecker()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, layerPlayer);

        if (inRange)
        {
            attack = true;
            posPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            //flip
            if(posPlayer.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                direction = 1;
            }
            else if(posPlayer.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                direction = -1;
            }

            radius = 0;
        }
    }

    void AttackBat()
    {
        if (attack)
        {
            anim.SetTrigger("Attack");
            transform.position = Vector3.MoveTowards(transform.position, posPlayer, moveSpeed * Time.deltaTime);

            if(transform.position == posPlayer)
            {
                rb.velocity = new Vector2(direction * impulse * Time.deltaTime, rb.velocity.y);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (inRange)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
