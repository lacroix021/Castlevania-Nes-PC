using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    public Transform emisorPos;

    public Collider2D myColl;

    public GameObject bodyAPrefab;
    public GameObject bodyBPrefab;

    float nextTimeAttack;
    public float attackRate;

    public float timeAttack;

    [Header("VALORES DE IMPULSO")]
    public float secondsAttack;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [Header("DETECTORES")]
    public GameObject PartA;
    public GameObject PartB;
    public GameObject PartC;
    public GameObject PartD;


    Rigidbody2D rb;
    HealthCore healthCore;

    public bool isBounce;

    public float forceMinX;
    public float forceMaxX;
    public float forceMinY;
    public float forceMaxY;

    public float tiempo;
    public float tiempoRebotando;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthCore = GetComponent<HealthCore>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(tiempo > 0)
        {
            tiempo -= 1 * Time.deltaTime;
        }

        //limite de tiempo
        if(tiempo <= 0)
        {
            tiempo = 0;
        }

        //control de ataques
        if (!PartA && !PartB && !PartC && !PartD)
        {
            if(tiempo == 0 && healthCore.currentHealth > 0)
            {
                isBounce = true;
            }
        }
        else
        {
            Attacks();
        }

        DetectorParts();
    }

    private void FixedUpdate()
    {
        Bounces();
    }

    void Attacks()
    {
        if (timeAttack > 0)
        {
            //random impulse
            float forceX = Random.Range(minX, maxX);
            float forceY = Random.Range(minY, maxY);
            //emisor de un tipo de cuerpo
            GameObject partA = Instantiate(bodyAPrefab, emisorPos.position, Quaternion.identity);
            partA.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            partA.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX * Time.deltaTime, forceY * Time.deltaTime), ForceMode2D.Force);
            //emisor de el otro tipo de cuerpo
            GameObject partB = Instantiate(bodyBPrefab, emisorPos.position, Quaternion.identity);
            partB.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            partB.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX * Time.deltaTime, forceY * Time.deltaTime), ForceMode2D.Force);

            timeAttack -= 1 * Time.deltaTime;
        }

        //para que no se pase de 0 a valores negativos
        if (timeAttack <= 0)
        {
            timeAttack = 0;
        }

        //para restablecer los segundos que dura cada ataque
        if (Time.time > nextTimeAttack)
        {
            timeAttack = secondsAttack;

            nextTimeAttack = Time.time + 1 / attackRate;
        }
    }

    void DetectorParts()
    {
        if (!PartA && !PartB && !PartC && !PartD)
        {
            myColl.enabled = true;
        }
        else
        {
            myColl.enabled = false;
        }
    }

    void Bounces()
    {
        if (isBounce)
        {
            tiempo = tiempoRebotando;
            rb.bodyType = RigidbodyType2D.Dynamic;
            myColl.isTrigger = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            float forceX = Random.Range(forceMinX, forceMaxX);
            float forceY = Random.Range(-forceMinY, forceMaxY);
            StartCoroutine(WaitToForce(forceX, forceY));
            isBounce = false;
        }

        //limites de velocidad
        if (rb.velocity.x >= 2.1f)
            rb.velocity = new Vector2(2, rb.velocity.y);

        if (rb.velocity.x <= -2.1f)
            rb.velocity = new Vector2(-2, rb.velocity.y);

        if (rb.velocity.y >= 2.1f)
            rb.velocity = new Vector2(rb.velocity.x, 2);

        if (rb.velocity.y <= -2.1f)
            rb.velocity = new Vector2(rb.velocity.x, -2);
    }

    IEnumerator WaitToForce(float xVal, float yVal)
    {
        yield return new WaitForSeconds(3);
        rb.gravityScale = 0.4f;
        rb.AddForce(new Vector2(xVal * Time.fixedDeltaTime, yVal * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }
}
