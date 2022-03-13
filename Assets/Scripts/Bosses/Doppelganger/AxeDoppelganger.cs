using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDoppelganger : MonoBehaviour
{
    public float rotation = 500f;
    public float forceX;
    public float forceY;
    [SerializeField] float direction;
    
    //
    Rigidbody2D rb;
    public GameObject blood;

    bool impulse;

    [Header("Tiempo de vida")]
    public float lifeTime;


    private void Start()
    {
        direction = GameObject.FindObjectOfType<DoppelgangerController>().direction;
        rb = GetComponent<Rigidbody2D>();
        impulse = true;
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //rotacion
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));
        Impulse();
    }

    void Impulse()
    {
        if (impulse)
        {
            forceY = Random.Range(90, 170);
            forceX = Random.Range(50, 120);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(direction * forceX * Time.fixedDeltaTime, forceY * Time.fixedDeltaTime), ForceMode2D.Impulse);
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
