using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonController : MonoBehaviour
{
    public float moveSpeed;
    public float direction;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector2(transform.localPosition.x + (direction * moveSpeed * Time.deltaTime), transform.localPosition.y));

        if (direction == 1)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction == -1)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
