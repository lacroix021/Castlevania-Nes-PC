using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDoppelganger : MonoBehaviour
{
    public float speed;
    [SerializeField] float direction;

    Rigidbody2D rb;

    public GameObject blood;

    bool impulse;

    [Header("Tiempo de vida")]
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        direction = GameObject.FindObjectOfType<DoppelgangerController>().direction;
        rb = GetComponent<Rigidbody2D>();
        impulse = true;

        if (direction == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

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
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y), ForceMode2D.Impulse);
            impulse = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Transform newPos = GameObject.Find("CeilingCheck").transform;
            Instantiate(blood, newPos.position, Quaternion.identity);
        }
    }
}
