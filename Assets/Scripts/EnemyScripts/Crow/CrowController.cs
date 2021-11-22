using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    public float moveSpeed;
    Transform playerPos;

    public bool inRange;
    public bool attack;
    public float radius;
    public LayerMask layerPlayer;

    Animator anim;

    float nextPosCheckTime;
    public float posCheckRate;

    public Vector3 positionAttack;
     

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRadius();
        Attack();
        Movement();
    }

    void CheckRadius()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, layerPlayer);
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Attack()
    {
        if (inRange)
        {
            attack = true;
            anim.SetTrigger("Fly");
        }

        if (attack)
        {
            playerPos = GameObject.FindGameObjectWithTag("HeadPlayer").GetComponent<Transform>();

            if (Time.time >= nextPosCheckTime)
            {
                positionAttack = playerPos.position;

                //flip y direccion de movimiento
                if (playerPos.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (playerPos.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                
                nextPosCheckTime = Time.time + 1f / posCheckRate;
            }
        }
    }

    void Movement()
    {
        if (attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionAttack, moveSpeed * Time.deltaTime);
        }
    }
}
