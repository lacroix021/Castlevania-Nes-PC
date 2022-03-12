using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleDoppelganger : MonoBehaviour
{
    public float forceX;
    public float forceY;
    [SerializeField] float direction;

    //
    Rigidbody2D rb;
    public GameObject fire;
    bool impulse;

    [Header("Tiempo de vida")]
    public float lifeTime;


    private void Start()
    {
        direction = GameObject.FindObjectOfType<DoppelgangerController>().direction;
        rb = GetComponent<Rigidbody2D>();
        impulse = true;
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Impulse();
    }

    void Impulse()
    {
        if (impulse)
        {
            forceY = Random.Range(30, 50);
            forceX = Random.Range(60, 150);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(direction * forceX * Time.fixedDeltaTime, forceY * Time.deltaTime), ForceMode2D.Impulse);
            impulse = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GameObject fireResult = Instantiate(fire, new Vector3(transform.position.x,transform.position.y -0.05f,0), Quaternion.identity);
            fireResult.GetComponent<FireMoving>().direction = direction;
            Destroy(this.gameObject);
        }

        if (coll.gameObject.CompareTag("Ground"))
        {
            GameObject fireResult = Instantiate(fire, new Vector3(transform.position.x, transform.position.y - 0.05f, 0), Quaternion.identity);
            fireResult.GetComponent<FireMoving>().direction = direction;
            fireResult.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            fireResult.GetComponent<Collider2D>().isTrigger = true;
            Destroy(this.gameObject);
        }
    }
}
