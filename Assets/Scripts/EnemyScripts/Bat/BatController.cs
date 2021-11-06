using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public float moveSpeed;
    float direction;
    Transform player;

    Rigidbody2D rb;
    Animator anim;

    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posY, timer, ySen;

    /// </summary>

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;

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

    private void FixedUpdate()
    {
        MovementBat();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }


    void MovementBat()
    {
        //movimiento senoidal
        timer = timer + (frecuency / 100);
        ySen = Mathf.Sin(timer);
        transform.position = new Vector3(transform.position.x, posY + (ySen * cycleWidth), transform.position.z);
        //movimiento horizontal
        rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);
        StartCoroutine(DieBat());
    }

    IEnumerator DieBat()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
