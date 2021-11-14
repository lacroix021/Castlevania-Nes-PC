using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterController : MonoBehaviour
{
    public float speedBottleX;
    public float speedBottleY;
    private float dirBottle;

    //SoundsSimon soundSimon;
    SubWeaponSystem weaponSys;
    DatosJugador datosJugador;
    Rigidbody2D rb;
    SpriteRenderer spr;
    BoxCollider2D bottleCol;

    private bool isGrounded;
    private BoxCollider2D boxCollision;
    public LayerMask theGround;

    public GameObject prefabFire;
    private GameObject fireInstanced;

    // Start is called before the first frame update
    void Start()
    {
        bottleCol = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        weaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        dirBottle = weaponSys.direction;
        transform.localScale = new Vector3(dirBottle/2, 0.5f, 0.5f);
        boxCollision = GetComponent<BoxCollider2D>();

        if (dirBottle == 1)
            transform.localRotation = Quaternion.Euler(0, 0, -29.16f);
        else if (dirBottle == -1)
            transform.localRotation = Quaternion.Euler(0, 0, 29.16f);

        ImpulseBottle();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void ImpulseBottle()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2((dirBottle * -1) * speedBottleX * Time.fixedDeltaTime, speedBottleY * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }


    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(boxCollision, theGround);

        if (isGrounded)
        {
            rb.bodyType = RigidbodyType2D.Static;
            spr.enabled = false;
            bottleCol.enabled = false;

            if (dirBottle == -1)
            {
                fireInstanced = Instantiate(prefabFire, transform.position, Quaternion.identity);
                StartCoroutine(TimeSpawnFire());
            }
            else if(dirBottle == 1)
            {
                fireInstanced = Instantiate(prefabFire, transform.position, Quaternion.identity);
                StartCoroutine(TimeSpawnFireB());
            }
        }
    }

    IEnumerator TimeSpawnFire()
    {
        if (datosJugador.multiplierPow == 1 || datosJugador.multiplierPow == 2)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject fireInstancedB = Instantiate(prefabFire, new Vector2(transform.position.x + 0.15f, transform.position.y + 0.01f), Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    IEnumerator TimeSpawnFireB()
    {
        if (datosJugador.multiplierPow == 1 || datosJugador.multiplierPow == 2)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject fireInstancedB = Instantiate(prefabFire, new Vector2(transform.position.x - 0.15f, transform.position.y + 0.01f), Quaternion.identity);
        }
            
        Destroy(this.gameObject);
    }
}
