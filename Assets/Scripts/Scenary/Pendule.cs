using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendule : MonoBehaviour
{
    
    public Vector3 vectorA = new Vector3(0, 0, -1);
    public Vector3 vectorB = new Vector3(1, 0, 0);

    public GameObject nodePendule;
    public float speed = 0.5f;
    public float distance = 1.3f;
    public float angle = 30;

    float a;
    Vector3 pos;
    

    public bool transporting;

    Collider2D thisColl;
    Collider2D otherColl;
    
    

    private void Start()
    {
        
        thisColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        a = 90 +
            Mathf.Sin(Time.time * Mathf.PI * speed)
            * angle;

        pos = Quaternion.AngleAxis(a, vectorA) *
            vectorB;

        pos *= distance;
        pos += nodePendule.transform.position;

        transform.position = pos;
        
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
