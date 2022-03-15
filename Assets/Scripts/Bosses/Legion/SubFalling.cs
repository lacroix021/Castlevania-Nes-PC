using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubFalling : MonoBehaviour
{
    public GameObject destroyEffect;
    public float rotation = 500f;
    Rigidbody2D rb;

    public float limitVelocityY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));
         

    }

    private void FixedUpdate()
    {
        if (rb.velocity.y <= limitVelocityY)
        {
            rb.velocity = new Vector2(rb.velocity.x, limitVelocityY);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Ground") || coll.gameObject.CompareTag("Platform") || coll.gameObject.CompareTag("Player"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
