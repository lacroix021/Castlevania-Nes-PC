using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPenduleStatic : MonoBehaviour
{
    public bool transporting;

    Collider2D thisColl;
    Collider2D otherColl;

    // Start is called before the first frame update
    void Start()
    {
        thisColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Transporting()
    {
        transporting = Physics2D.IsTouching(thisColl, otherColl);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            otherColl = collision.collider.GetComponent<Collider2D>();
            Transporting();

            if (transporting)
            {
                collision.gameObject.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            transporting = false;
            collision.gameObject.transform.parent = null;
        }
    }
}
