using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool transporting;

    EdgeCollider2D thisColl;
    BoxCollider2D otherColl;
    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posX, timer, xSen;

    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        thisColl = GetComponent<EdgeCollider2D>();

        posX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.gamePaused)
        {
            //movimiento senoidal
            timer = timer + (frecuency / 100);
            xSen = Mathf.Sin(timer);
            transform.position = new Vector3(posX + (xSen * cycleWidth), transform.position.y, transform.position.z);
        }
    }

    void Transporting()
    {
        transporting = Physics2D.IsTouching(thisColl, otherColl);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            otherColl = collision.collider.GetComponent<BoxCollider2D>();
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
