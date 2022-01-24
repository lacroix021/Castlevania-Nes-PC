using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubWeaponSystem : MonoBehaviour
{
    private float nextSubTime;
    float subRate;

    SimonController pController;
    HeartsSystem heartsSys;
    HealthPlayer health;

    DatosJugador datosJugador;
    GameManager gmanager;

    public float direction;

    public GameObject[] subWeapons;

    public Transform subPos;
    public Transform subPosB;
    public bool canCrush = false;
    float nextCrushTime;
    public float crushRate = 0.2f;
    ParticleSystem psAxe;

    /*
    type of subweapon
    0 = knife
    1 = Axe
    2 = Holy Water
    3 = Cross
    */

    List<ParticleCollisionEvent> eventCol = new List<ParticleCollisionEvent>(); //lista de eventos del PS

    // Start is called before the first frame update
    void Start()
    {
        pController = GetComponent<SimonController>();
        heartsSys = GetComponent<HeartsSystem>();
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        gmanager = GameManager.gameManager;
        health = GetComponent<HealthPlayer>();
        psAxe = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RateSubWeapons();
    }

    
    public void InputSubWep(InputAction.CallbackContext context)
    {
        if (!gmanager.GamePaused)
        {
            if (context.performed && datosJugador.haveSub && pController.canMove)
            {
                if (pController.isGrounded)
                {
                    pController.rb.velocity = new Vector2(0, 0);
                }
                //Dagas y cooldown
                if (datosJugador.typeSub == 0 && datosJugador.currentHearts >= 1 && pController.v < 0.1f)
                {
                    if (Time.time >= nextSubTime)
                    {
                        pController.anim.SetTrigger("Sub");
                        pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                        datosJugador.currentHearts -= 1;
                        nextSubTime = Time.time + 1f / subRate;
                    }
                }
                //achas y cooldown
                else if (datosJugador.typeSub == 1 && datosJugador.currentHearts >= 2 && pController.v < 0.1f)
                {
                    if (Time.time >= nextSubTime)
                    {
                        pController.anim.SetTrigger("Sub");
                        pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                        datosJugador.currentHearts -= 2;
                        nextSubTime = Time.time + 1f / subRate;
                    }
                }
                //agua bendita y cooldown
                else if (datosJugador.typeSub == 2 && datosJugador.currentHearts >= 2 && pController.v < 0.1f)
                {
                    if (Time.time >= nextSubTime)
                    {
                        pController.anim.SetTrigger("Sub");
                        pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                        datosJugador.currentHearts -= 2;
                        nextSubTime = Time.time + 1f / subRate;
                    }
                }
                //cruz y cooldown
                else if (datosJugador.typeSub == 3 && datosJugador.currentHearts >= 2 && pController.v < 0.1f)
                {
                    if (Time.time >= nextSubTime)
                    {
                        pController.anim.SetTrigger("Sub");
                        pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                        datosJugador.currentHearts -= 2;
                        nextSubTime = Time.time + 1f / subRate;
                    }
                }

                heartsSys.CheckHearts();
            }

            if (canCrush && context.performed && pController.v > 0 && datosJugador.haveSub && datosJugador.currentHearts >= 20 && pController.canMove)
            {
                if (Time.time >= nextCrushTime)
                {
                    health.isInvulnerable = true;
                    Physics2D.IgnoreLayerCollision(9, 10, true);
                    pController.rb.velocity = Vector2.zero;
                    pController.rb.AddForce(Vector2.up * 100 * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    StartCoroutine(MidAir());
                    pController.anim.SetTrigger("ItemCrush");
                    datosJugador.currentHearts -= 20;
                    pController.canMove = false;
                    StartCoroutine(PostItemCrush());
                    ParticleCrush();
                    nextCrushTime = Time.time + 1f / crushRate;
                } 
            }
        }
    }

    IEnumerator PostItemCrush()
    {
        yield return new WaitForSeconds(1.8f);
        pController.canMove = true;
        health.isInvulnerable = false;
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    
    IEnumerator MidAir()
    {
        yield return new WaitForSeconds(0.2f);
        pController.rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.7f);
        pController.rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void InstantiateSubWeapon()
    {
        direction = transform.localScale.x;

        if (datosJugador.multiplierPow == 0 || datosJugador.multiplierPow == 1)
        {
            Instantiate(subWeapons[datosJugador.typeSub], subPos.position, Quaternion.identity);
        }
        else if (datosJugador.multiplierPow == 2)
        {
            Instantiate(subWeapons[datosJugador.typeSub], subPos.position, Quaternion.identity);
            Instantiate(subWeapons[datosJugador.typeSub], subPosB.position, Quaternion.identity);
        }
    }

    void RateSubWeapons()
    {
        if(datosJugador.typeSub == 0)
        {
            if (datosJugador.multiplierPow == 0)
                subRate = 0.8f;
            else if (datosJugador.multiplierPow == 1)
                subRate = 1.4f;
            else if (datosJugador.multiplierPow == 2)
                subRate = 1.8f;
        }
        else if(datosJugador.typeSub == 1)
        {
            if (datosJugador.multiplierPow == 0)
                subRate = 0.7f;
            else if (datosJugador.multiplierPow == 1)
                subRate = 1.4f;
            else if (datosJugador.multiplierPow == 2)
                subRate = 1.6f;
        }
        else if(datosJugador.typeSub == 2)
        {
            if (datosJugador.multiplierPow == 0)
                subRate = 0.7f;
            else if (datosJugador.multiplierPow == 1)
                subRate = 1.1f;
            else if (datosJugador.multiplierPow == 2)
                subRate = 1.3f;
        }
        else if(datosJugador.typeSub == 3)
        {
            if (datosJugador.multiplierPow == 0)
                subRate = 0.4f;
            else if (datosJugador.multiplierPow == 1)
                subRate = 0.7f;
            else if (datosJugador.multiplierPow == 2)
                subRate = 0.9f;
        }
    }

    void ParticleCrush()
    {
        if(datosJugador.typeSub == 1)
        {
            psAxe.Play();
        }
    }

    private void OnParticleCollision(GameObject enemy)
    {
        int events = psAxe.GetCollisionEvents(enemy, eventCol); //tamaño de eventos

        for (int i = 0; i < events; i++)
        {
            print(enemy.name + " con collider");
            //quitarle vida al enemigo colisionado
            //ver si esto funciona con triggers ya que la particula no debe tocar objetos solo enemigos
        }
    }

    //on particle trigger no funciona pasandole valores, corregir!
    private void OnParticleTrigger( GameObject enemy)
    {
        int events = psAxe.GetCollisionEvents(enemy, eventCol); //tamaño de eventos

        for (int i = 0; i < events; i++)
        {
            print(enemy.name + " con trigger");
            //quitarle vida al enemigo colisionado
            //ver si esto funciona con triggers ya que la particula no debe tocar objetos solo enemigos
        }
    }
}
