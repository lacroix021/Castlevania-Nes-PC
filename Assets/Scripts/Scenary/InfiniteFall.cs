using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteFall : MonoBehaviour
{

    public Transform posInfA;
    public int cant;
    public float timer;

    public bool inside;

    public LayerMask layerPlayer;
    Collider2D myColl;

    // Start is called before the first frame update
    void Start()
    {
        cant = 0;
        myColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inside = Physics2D.IsTouchingLayers(myColl, layerPlayer);

        if (!inside)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                cant = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cant != 5)
        {
            collision.transform.position = new Vector3(collision.transform.position.x, posInfA.position.y, 0);
            cant += 1;
            timer = 0;
        }
    }
}
