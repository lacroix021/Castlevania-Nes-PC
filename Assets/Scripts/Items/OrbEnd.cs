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

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        myColl = GetComponent<Collider2D>();
        guardarCargar = GameManager.gameManager.GetComponent<GuardarCargar>();
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        gManager = GameManager.gameManager;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            spr.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            myColl.enabled = false;
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
