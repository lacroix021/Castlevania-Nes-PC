using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float moveSpeed;
    public float dirSnake;
    Rigidbody2D rb;

    bool wall;
    public LayerMask layerWall;
    public BoxCollider2D bColl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    private void FixedUpdate()
    {
        WallCheck();
        rb.velocity = new Vector2(dirSnake * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    void WallCheck()
    {
        wall = Physics2D.IsTouchingLayers(bColl, layerWall);

        if (wall)
        {
            if(dirSnake == 1)
            {
                dirSnake = -1;
            }
            else if(dirSnake == -1)
            {
                dirSnake = 1;
            }
        }

        if (dirSnake == 1)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dirSnake == -1)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
