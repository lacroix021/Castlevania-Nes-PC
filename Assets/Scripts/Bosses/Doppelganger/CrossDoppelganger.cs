using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossDoppelganger : MonoBehaviour
{
    public float rotation = 500f;

    public float speed;

    [SerializeField] float direction;

    Rigidbody2D rb;
    public GameObject blood;


    [Header("Tiempo de vida")]
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        direction = GameObject.FindObjectOfType<DoppelgangerController>().direction;
        rb = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject, lifeTime);

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //rotacion
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));

        StartCoroutine(ReturnCross());
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Transform newPos = GameObject.Find("CeilingCheck").transform;
            Instantiate(blood, newPos.position, Quaternion.identity);
        }
    }

    IEnumerator ReturnCross()
    {
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(new Vector2((direction * -1) * speed * Time.fixedDeltaTime, rb.velocity.y), ForceMode2D.Force);
    }
}
