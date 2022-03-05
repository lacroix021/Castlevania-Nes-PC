using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitController : MonoBehaviour
{
    public float moveAttackX;    //0.9f base
    public float moveAttackY;    //0.9f base
    public float direction;


    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Flip();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * direction * moveAttackX * Time.deltaTime, ForceMode2D.Impulse);
        rb.AddForce(transform.up * moveAttackY * Time.deltaTime, ForceMode2D.Impulse);
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Destroy(this.gameObject);
        }
    }
    */

    void Flip()
    {
        if (direction == -1)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
