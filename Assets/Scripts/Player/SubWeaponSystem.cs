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

    DatosJugador datosJugador;
    GameManager gmanager;

    public float direction;

    public GameObject[] subWeapons;

    public Transform subPos;
    public Transform subPosB;

    /*
    type of subweapon
    0 = knife
    1 = Axe
    2 = Holy Water
    3 = Cross
    */

    // Start is called before the first frame update
    void Start()
    {
        pController = GetComponent<SimonController>();
        heartsSys = GetComponent<HeartsSystem>();
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        gmanager = GameManager.gameManager;
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
            if (context.performed && datosJugador.haveSub)
            {
                if (pController.isGrounded)
                {
                    pController.rb.velocity = new Vector2(0, 0);
                }
                //Dagas y cooldown
                if (datosJugador.typeSub == 0 && datosJugador.currentHearts >= 1)
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
                else if (datosJugador.typeSub == 1 && datosJugador.currentHearts >= 2)
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
                else if (datosJugador.typeSub == 2 && datosJugador.currentHearts >= 2)
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
                else if (datosJugador.typeSub == 3 && datosJugador.currentHearts >= 2)
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
        }
    }

    

    void InstantiateSubWeapon()
    {
        direction = transform.localScale.x;

        if(datosJugador.multiplierPow == 0 || datosJugador.multiplierPow == 1)
        {
            Instantiate(subWeapons[datosJugador.typeSub], subPos.position, Quaternion.identity);
        }
        else if(datosJugador.multiplierPow == 2)
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
}
