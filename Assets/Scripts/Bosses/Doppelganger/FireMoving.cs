using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMoving : MonoBehaviour
{
    public float direction;
    public float speed;
    Rigidbody2D rb;

    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifeTime);

        if(rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.AddForce(new Vector2(direction * speed * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
        }
        //transform.position = new Vector3(transform.position.x + (direction * speed * Time.deltaTime), transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.bodyType == RigidbodyType2D.Kinematic)
        {
            if (direction == -1)
                transform.Translate(Vector2.left * 0.1f * Time.deltaTime);
            else if (direction == 1)
                transform.Translate(Vector2.right * 0.1f * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if(rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (coll.gameObject.CompareTag("Ground"))
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                this.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }
}
