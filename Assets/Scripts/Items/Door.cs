using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool needKey;
    [Tooltip("debe estar marcado needKey para poner un valor de que llave se necesita para abrir la puerta 0 = none, 1 = bluekey, 2 = cianKey, 3 = redKey, 4 = yellowKey, 5 = pinkKey, 6 = greenKey")]
    public int requiredKey;
    // 0 = none
    // 1 = bluekey
    // 2 = cianKey
    // 3 = redKey
    // 4 = yellowKey
    // 5 = pinkKey
    // 6 = greenKey

    Animator anim;

    AudioSource aSource;

    DatosJugador datosJugador;

    GameManager gManager;

    private bool keyNeeded;

    

    private void Start()
    {
        gManager = GameManager.gameManager;
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        datosJugador = gManager.GetComponent<DatosJugador>();

    }


    void KeyDetector()
    {
        switch (requiredKey)
        {
            case 0:
                keyNeeded = false;
                break;
            case 1:
                keyNeeded = datosJugador.blueKey;
                gManager.doorMessajeTxt.text = "Need Blue key";
                break;
            case 2:
                keyNeeded = datosJugador.cianKey;
                gManager.doorMessajeTxt.text = "Need Cian key";
                break;
            case 3:
                keyNeeded = datosJugador.redKey;
                gManager.doorMessajeTxt.text = "Need Red key";
                break;
            case 4:
                keyNeeded = datosJugador.yellowKey;
                gManager.doorMessajeTxt.text = "Need Yellow key";
                break;
            case 5:
                keyNeeded = datosJugador.pinkKey;
                gManager.doorMessajeTxt.text = "Need Pink key";
                break;
            case 6:
                keyNeeded = datosJugador.greenKey;
                gManager.doorMessajeTxt.text = "Need Green key";
                break;
        }
    }

    //agregar un metodo para que las puertas tengan una condicion de llave y que sea reutilizable sin importar la llave

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && needKey)
        {
            KeyDetector();

            if (keyNeeded)
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(1.5f, 1, 1);
                }
                else if (collision.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-1.5f, 1, 1);
                }

                anim.SetTrigger("OpenDoor");
                aSource.Play();
            }
            else
            {
                //sincronizar con el gamemanager para que muestre en pantalla que el jugador no tiene la llave que necesita
                gManager.barDoorMessaje.SetActive(true);
            }
        }

        else if (collision.gameObject.CompareTag("Player") && !needKey)
        {
            if (collision.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1.5f, 1, 1);
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1.5f, 1, 1);
            }

            anim.SetTrigger("OpenDoor");
            aSource.Play();
        }
    }
}
