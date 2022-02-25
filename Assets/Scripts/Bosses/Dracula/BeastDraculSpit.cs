using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastDraculSpit : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D thisColl;
    bool touchingGround;
    public LayerMask thisGround;
    // Start is called before the first frame update
    void Start()
    {
        thisColl = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        touchingGround = Physics2D.IsTouchingLayers(thisColl, thisGround);

        if (touchingGround)
        {
            //thisColl.enabled = false;
            //rb.bodyType = RigidbodyType2D.Kinematic;
            Destroy(this.gameObject, 1);
        }
    }
}
