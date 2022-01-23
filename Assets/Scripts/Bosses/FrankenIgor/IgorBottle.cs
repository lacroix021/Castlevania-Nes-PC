using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgorBottle : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spr;
    BoxCollider2D bottleCol;

    bool isGrounded;
    bool hitPlayer;
    BoxCollider2D boxCollision;
    public LayerMask theGround;
    public LayerMask layerPlayer;

    public GameObject prefabFire;
    GameObject fireInstanced;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bottleCol = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        boxCollision = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }


    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(boxCollision, theGround);
        hitPlayer = Physics2D.IsTouchingLayers(boxCollision, layerPlayer);

        if (isGrounded || hitPlayer)
        {
            rb.bodyType = RigidbodyType2D.Static;
            spr.enabled = false;
            bottleCol.enabled = false;

            fireInstanced = Instantiate(prefabFire, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
