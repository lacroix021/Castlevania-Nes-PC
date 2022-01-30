using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxesControl : MonoBehaviour
{
    public enum typeAxe
    {
        axeFloat,
        axeGravity
    };
    public typeAxe TypeAxe;

    public float rotation;

    Rigidbody2D rb;
    public float speedMove;
    public float direction;
    public float dirTemp;


    public GameObject blood;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        if (TypeAxe == typeAxe.axeFloat)
        {
            

            direction = dirTemp;
            StartCoroutine(ChangeDirection());
            Destroy(this.gameObject, 6f);
        }
        else if(TypeAxe == typeAxe.axeGravity)
        {
            Destroy(this.gameObject, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));
    }

    private void FixedUpdate()
    {
        if (TypeAxe == typeAxe.axeFloat)
        {
            //limitador de velocidad hacha
            if (rb.velocity.x >= 1)
                rb.velocity = new Vector2(1f, 0);
            else if (rb.velocity.x <= -1)
                rb.velocity = new Vector2(-1f, 0);

            AxePath();
        }
    }

    void AxePath()
    {
        rb.AddForce(new Vector2(direction * speedMove * Time.fixedDeltaTime, 0), ForceMode2D.Force);
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(1.5f);
        if (direction == -1)
            direction = 1;
        else if (direction == 1)
            direction = -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
