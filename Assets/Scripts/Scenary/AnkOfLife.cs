using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnkOfLife : MonoBehaviour
{
    public ParticleSystem particleA;
    public ParticleSystem particleB;

    GameManager gManager;
    HealthPlayer playerHealth;

    AudioSource aSource;

    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posY, timer, ySen;
    /// </summary>

    float timeSaveRate;
    public float saveRate;


    /*probando nuevo metodo de guardado*/
    GuardarCargar guardarCargar;
    DatosJugador datosJugador;

    /**********************************/

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        gManager = GameManager.gameManager;
        datosJugador = gManager.GetComponent<DatosJugador>();
        posY = transform.position.y;
        aSource = GetComponent<AudioSource>();

        /**/
        guardarCargar = gManager.GetComponent<GuardarCargar>();
        /**/
    }

    private void FixedUpdate()
    {
        MovementAnk();
    }

    void MovementAnk()
    {
        //movimiento senoidal
        timer = timer + (frecuency / 100);
        ySen = Mathf.Sin(timer);
        transform.position = new Vector3(transform.position.x, posY + (ySen * cycleWidth), transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && Input.GetAxisRaw("Vertical") > 0)
        {
            if(Time.time >= timeSaveRate)
            {
                particleA.Play();
                particleB.Play();
                //funcion de guardado
                SaveGame();
                datosJugador.Saves += 1;
                PlayerPrefs.SetInt("Saves", datosJugador.Saves);
                PlayerPrefs.SetFloat("MapPercent", datosJugador.percentMap);
                PlayerPrefs.SetFloat("LastPositionX", datosJugador.posPlayer.x);
                PlayerPrefs.SetFloat("LastPositionY", datosJugador.posPlayer.y);
                PlayerPrefs.SetFloat("LastPositionZ", datosJugador.posPlayer.z);
                timeSaveRate = Time.time + 1 / saveRate;
                /**/
                guardarCargar.GuardarInformacion();
                /**/
            }
        }
    }

    void SaveGame()
    {
        aSource.loop = false;
        aSource.Play();

        //cura al jugador al guardar
        playerHealth.healing = true;
        playerHealth.vidaACurar = datosJugador.maxHealth;
    }
}
