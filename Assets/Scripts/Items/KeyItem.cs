using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    
    public enum tipeKey
    {
        blueKey,
        cianKey,
        redKey,
        yellowKey,
        pinkKey,
        greenKey
    };
    [Header("TIPO DE LLAVE")]
    public tipeKey TipeKey;

    GameManager gManager;
    DatosJugador datosJugador;

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.gameManager;

        datosJugador = gManager.GetComponent<DatosJugador>();
    }

    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gManager.audioSource.clip = gManager.soundGrabItem;
            gManager.audioSource.Play();
            gManager.audioSource.loop = false;

            if (TipeKey == tipeKey.blueKey)
            {
                //añadir al llavero
                datosJugador.blueKey = true;
                Destroy(this.gameObject);
            }
            else if (TipeKey == tipeKey.cianKey)
            {
                //añadir al llavero
                datosJugador.cianKey = true;
                Destroy(this.gameObject);
            }
            else if (TipeKey == tipeKey.redKey)
            {
                //añadir al llavero
                datosJugador.redKey = true;
                Destroy(this.gameObject);
            }
            else if (TipeKey == tipeKey.yellowKey)
            {
                //añadir al llavero
                datosJugador.yellowKey = true;
                Destroy(this.gameObject);
            }
            else if (TipeKey == tipeKey.pinkKey)
            {
                //añadir al llavero
                datosJugador.pinkKey = true;
                Destroy(this.gameObject);
            }
            else if (TipeKey == tipeKey.greenKey)
            {
                //añadir al llavero
                datosJugador.greenKey = true;
                Destroy(this.gameObject);
            }
        }
    }
}
