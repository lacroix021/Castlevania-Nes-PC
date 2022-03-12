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
}
