using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitFireController : MonoBehaviour
{
    public GameObject firePrefab;
    public float speedFire;
    public Transform throwA, throwB;

    float nextFireTime;
    public float fireRate;

    public bool fire;
    public bool inRange;
    public LayerMask layerPlayer;
    public BoxCollider2D collisionRadius;

    Transform playerPos;

    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Time.time >= nextFireTime)
            {
                fire = true;
                Fire();

                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        RangeDetect();
    }

    void Fire()
    {
        if (fire)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            spr.color = Color.red;

            StartCoroutine(Attack());
            
            fire = false;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.8f);

        if (playerPos.position.x > transform.position.x)
        {
            GameObject fireInstance = Instantiate(firePrefab, throwB.position, Quaternion.identity);
            fireInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * speedFire * Time.fixedDeltaTime, 0);
            yield return new WaitForSeconds(0.5f);
            GameObject fireInstanceB = Instantiate(firePrefab, throwB.position, Quaternion.identity);
            fireInstanceB.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * speedFire * Time.fixedDeltaTime, 0);

        }
        else if (playerPos.position.x < transform.position.x)
        {
            GameObject fireInstance = Instantiate(firePrefab, throwA.position, Quaternion.identity);
            fireInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speedFire * Time.fixedDeltaTime, 0);
            yield return new WaitForSeconds(0.5f);
            GameObject fireInstanceB = Instantiate(firePrefab, throwA.position, Quaternion.identity);
            fireInstanceB.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speedFire * Time.fixedDeltaTime, 0);
        }

        spr.color = Color.white;
    }

    void RangeDetect()
    {
        inRange = Physics2D.IsTouchingLayers(collisionRadius, layerPlayer);
    }
}
