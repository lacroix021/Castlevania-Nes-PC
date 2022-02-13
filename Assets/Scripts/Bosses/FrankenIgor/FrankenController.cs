using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankenController : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedReverse;

    Rigidbody2D rb;
    Animator anim;
    Transform playerPos;

    public float distanceFromPlayer;
    public float visionRange;
    public float visionRangeReverse;
    public float RangeAttack;
    public bool moving;

    public GameObject igorSprite;
    public GameObject igorBoss;
    public GameObject igorBossInstance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animations();
    }

    void Movement()
    {
        if(playerPos.position.x < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if(playerPos.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);

        distanceFromPlayer = Vector2.Distance(playerPos.position, rb.position);
        if(distanceFromPlayer < visionRange && distanceFromPlayer > visionRangeReverse && distanceFromPlayer > RangeAttack)
        {
            //si esta el jugador en rango
            GameObject.Find("Stage4BMusic").GetComponent<ActivateMusic>().battle = true;
            Vector2 nuevaPos = Vector2.MoveTowards(rb.position, new Vector2(playerPos.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            rb.MovePosition(nuevaPos);
            moving = true;
        }
        else if(distanceFromPlayer < visionRange && distanceFromPlayer > visionRangeReverse && distanceFromPlayer <= RangeAttack)
        {
            //si el jugador esta en rango de ataque
            igorSprite.SetActive(false);

            if (!igorBossInstance)
            {
                igorBossInstance = Instantiate(igorBoss, igorSprite.transform.position, Quaternion.identity);
            }

            Vector2 nuevaPos = Vector2.MoveTowards(rb.position, new Vector2(playerPos.position.x, transform.position.y), 0 * Time.deltaTime);
            rb.MovePosition(nuevaPos);
            moving = false;
        }
        else if(distanceFromPlayer < visionRangeReverse)
        {
            //si el jugador esta muy cerca, retrocede
            Vector2 nuevaPos = Vector2.MoveTowards(rb.position, new Vector2(playerPos.position.x, transform.position.y), moveSpeedReverse * Time.deltaTime);
            rb.MovePosition(nuevaPos);
            moving = true;
        }
        else
        {
            //si el jugador no esta en rango
            if(GetComponent<HealthBoss>().currentHealth < GetComponent<HealthBoss>().maxHealth)
            {
                GetComponent<HealthBoss>().currentHealth += 1 * Time.deltaTime;
            }
            else
            {
                GetComponent<HealthBoss>().currentHealth = GetComponent<HealthBoss>().maxHealth;
            }
            
            moving = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRangeReverse);
    }

    void Animations()
    {
        anim.SetBool("Moving", moving);
    }
}
