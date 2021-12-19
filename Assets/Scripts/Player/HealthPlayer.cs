using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public float currentHealth;
    public bool isDead;
    public bool isInvulnerable;

    SimonController playerController;
    SpriteRenderer sprPlayer;

    public float forceKnockback;
    Renderer rend;
    Color c;

    GameManager gManager;

    DatosJugador datosJugador;

    public float currentDamageTime;
    public float damageTime;
    [SerializeField] float dañoADisminuir;
    public float vidaACurar;
    public bool damaged;
    public bool healing;

    float colPosX;


    

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<SimonController>();
        gManager = GameManager.gameManager;
        datosJugador = gManager.GetComponent<DatosJugador>();
        sprPlayer = GetComponent<SpriteRenderer>();
        /*********************/
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    private void Update()
    {
        RecibeDaño();
        CuraDaño();
    }

    public void HealthCheck()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, datosJugador.maxHealth);

        if (currentHealth <= 0)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.lifeLost);
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        DeathPlayer();
    }

    void DeathPlayer()
    {
        if (isDead)
        {
            playerController.anim.SetBool("Die", isDead);
            playerController.canMove = false;
            playerController.rb.velocity = new Vector2(0, playerController.rb.velocity.y);
            gManager.BlinkControl();
            StartCoroutine(ChangeRb());

            StartCoroutine(TimeRespawn());
        }
    }

    IEnumerator ChangeRb()
    {
        yield return new WaitUntil(() => playerController.isGrounded);
        playerController.rb.bodyType = RigidbodyType2D.Static;
        playerController.myCollider.enabled = false;
    }

    public void RespawnPlayer()
    {
        if(datosJugador.Saves != 0 && datosJugador.gold >= datosJugador.costRespawn)
        {
            currentHealth = datosJugador.maxHealth;
            transform.position = new Vector3(PlayerPrefs.GetFloat("LastPositionX"), PlayerPrefs.GetFloat("LastPositionY"), PlayerPrefs.GetFloat("LastPositionZ"));
            datosJugador.currentHearts = 0;
            datosJugador.typeSub = 4;
            datosJugador.haveSub = false;
            datosJugador.multiplierPow = 0;
            datosJugador.typeWhip = 0;
            isDead = false;
            playerController.anim.SetBool("Die", isDead);
            isInvulnerable = false;
            playerController.anim.SetBool("Hurting", false);
            playerController.canMove = true;
            sprPlayer.enabled = true;
            playerController.rb.bodyType = RigidbodyType2D.Dynamic;
            playerController.myCollider.enabled = true;
            Physics2D.IgnoreLayerCollision(9, 10, false);

            //pierde oro al revivir
            datosJugador.gold -= datosJugador.costRespawn;
            datosJugador.costRespawn *= 2;
            //
        }
    }

    IEnumerator TimeRespawn()
    {
        yield return new WaitForSeconds(4);
        RespawnPlayer();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy") && currentHealth > 0 && !isInvulnerable)
        {
            dañoADisminuir = col.collider.GetComponent<DamageTouch>().damageTouch;
            colPosX = col.transform.position.x;
            MakeHurtPlayer();
        }
    }
    
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy") && currentHealth > 0 && !isInvulnerable)
        {
            dañoADisminuir = col.collider.GetComponent<DamageTouch>().damageTouch;
            colPosX = col.transform.position.x;
            MakeHurtPlayer();
        }
    }
   
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && currentHealth > 0 && !isInvulnerable)
        {
            dañoADisminuir = col.GetComponent<DamageTouch>().damageTouch;
            colPosX = col.transform.position.x;
            MakeHurtPlayer();
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && currentHealth > 0 && !isInvulnerable)
        {
            dañoADisminuir = col.GetComponent<DamageTouch>().damageTouch;
            colPosX = col.transform.position.x;
            MakeHurtPlayer();
        }
    }

    void MakeHurtPlayer()
    {
        damaged = true;
        isInvulnerable = true;

        if (!playerController.isSlide)
        {
            playerController.canMove = false;
            playerController.anim.SetBool("Hurting", true);
            playerController.rb.velocity = Vector2.zero;

            if (colPosX > transform.position.x)
            {
                playerController.rb.AddForce(new Vector2(-forceKnockback * Time.fixedDeltaTime, 100 * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }
            else if (colPosX < transform.position.x)
            {
                playerController.rb.AddForce(new Vector2(forceKnockback * Time.fixedDeltaTime, 100 * Time.fixedDeltaTime), ForceMode2D.Impulse);
            }
        }

        AudioManager.instance.PlayAudio(AudioManager.instance.simonHurt);

        StartCoroutine(MoveAgain());
        StartCoroutine(GetInvulnerable());
        HealthCheck();
    }

    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(0.3f);

        if (currentHealth > 0)
        {
            playerController.canMove = true;
            playerController.anim.SetBool("Hurting", false);
        }
    }

    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        c.a = 0.5f;
        rend.material.color = c;
        yield return new WaitForSeconds(0.8f);
        c.a = 1f;
        rend.material.color = c;

        if (currentHealth > 0)
        {
            Physics2D.IgnoreLayerCollision(9, 10, false);
            isInvulnerable = false;
        }
    }

    //recibir daño de forma progresiva
    void RecibeDaño()
    {
        if (damaged)
        {
            currentDamageTime += Time.deltaTime;

            if (currentDamageTime > damageTime)
            {
                currentHealth--;
                dañoADisminuir--;

                currentDamageTime = 0;
            }
            HealthCheck();
        }

        if (dañoADisminuir == 0)
        {
            damaged = false;
        }
    }

    //curar daño de forma progresiva
    public void CuraDaño()
    {
        if (healing)
        {
            currentDamageTime += Time.deltaTime;

            if (currentDamageTime > damageTime)
            {
                currentHealth++;
                vidaACurar--;

                currentDamageTime = 0;
            }
            HealthCheck();
        }

        if (vidaACurar == 0)
        {
            healing = false;
        }
    }
}
