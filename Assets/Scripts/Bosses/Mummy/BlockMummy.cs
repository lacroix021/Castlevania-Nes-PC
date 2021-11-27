using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMummy : MonoBehaviour
{
    public bool block;
    public bool fragment;
    public LayerMask theGround;


    bool grounded;
    Collider2D coll;
    public GameObject fragmentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (block)
        {
            grounded = Physics2D.IsTouchingLayers(coll, theGround);
            

            if (grounded)
            {
                Fragments();

                Destroy(this.gameObject);
            }
        }

        if (fragment)
        {
            Destroy(this.gameObject, 0.7f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (block)
        {
            if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
            {
                Fragments();

                Destroy(this.gameObject);
            }
        }
    }

    void Fragments()
    {
        GameObject fragmentA = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
        fragmentA.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30 * Time.deltaTime, 20 * Time.deltaTime), ForceMode2D.Impulse);
        GameObject fragmentB = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
        fragmentB.GetComponent<Rigidbody2D>().AddForce(new Vector2(30 * Time.deltaTime, 20 * Time.deltaTime), ForceMode2D.Impulse);
        GameObject fragmentC = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
        fragmentC.GetComponent<Rigidbody2D>().AddForce(new Vector2(-40 * Time.deltaTime, 20 * Time.deltaTime), ForceMode2D.Impulse);
    }
}
