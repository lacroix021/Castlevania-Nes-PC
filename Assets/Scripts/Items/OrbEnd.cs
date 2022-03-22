using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbEnd : MonoBehaviour
{
    SpriteRenderer spr;
    Rigidbody2D rb;
    Collider2D myColl;
    GuardarCargar guardarCargar;
    DatosJugador datosJugador;
    GameManager gManager;

    bool touched;
    public LayerMask layerPlayer;
    public Collider2D coll;


    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        myColl = GetComponent<Collider2D>();
        guardarCargar = GameManager.gameManager.GetComponent<GuardarCargar>();
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        gManager = GameManager.gameManager;
    }

    private void Update()
    {
        TakenController();
    }

    void TakenController()
    {
        touched = Physics2D.IsTouchingLayers(coll, layerPlayer);

        if (touched)
        {
            spr.enabled = false;
            coll.gameObject.layer = 19;
            GameObject.Find("Stage7Music").GetComponent<ActivateMusic>().completeMusic.GetComponent<AudioSource>().Play();
            LastSave();
            StartCoroutine(WaitEnd());
        }
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(5.277f);
        SceneManager.LoadScene(3);
        Destroy(this.gameObject);
    }

    void LastSave()
    {
        guardarCargar.GuardarInformacion();
        PlayerPrefs.SetFloat("Seconds", gManager.seconds);
        PlayerPrefs.SetFloat("Minutes", gManager.minutes);
        PlayerPrefs.SetFloat("Hours", gManager.hours);

        PlayerPrefs.SetInt("GoldPlayer", datosJugador.gold);
        PlayerPrefs.SetInt("Saves", datosJugador.Saves);
        PlayerPrefs.SetFloat("MapPercent", datosJugador.percentMap);
    }
}
