using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaHeadController : MonoBehaviour
{
    public float moveSpeed;
    float direction;
    Transform player;

    Rigidbody2D rb;
    Animator anim;

    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float timer, ySen;

    /// </summary>

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        /*valores aleatorios al spawneo de medusa head para mas dificultad*/
        moveSpeed = Random.Range(30, 51);
        cycleWidth = Random.Range(30, 81);
        frecuency = Random.Range(6, 16);
        ////////////////////////////////////////////////////////////
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        if (transform.position.x > player.position.x)
        {
            direction = -1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < player.position.x)
        {
            direction = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Update()
    {
        anim.SetFloat("VelY", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        MoveMedusaHead();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }


    void MoveMedusaHead()
    {
        //movimiento senoidal
        timer = timer + (frecuency / 100);
        ySen = Mathf.Sin(timer);
        
        //movimiento horizontal y senoidal aplicado al RB
        rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime,  (ySen * cycleWidth) * Time.fixedDeltaTime);
        StartCoroutine(DieMedusaHead());
    }

    IEnumerator DieMedusaHead()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
