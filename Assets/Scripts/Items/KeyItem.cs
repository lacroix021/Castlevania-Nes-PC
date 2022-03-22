using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    
    public enum typeKey
    {
        blueKey,
        cianKey,
        redKey,
        yellowKey,
        pinkKey,
        greenKey
    };

    [Header("TIPO DE LLAVE")]
    public typeKey TypeKey;

    GameManager gManager;
    DatosJugador datosJugador;

    bool touched;
    public LayerMask layerPlayer;
    public Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.gameManager;

        datosJugador = gManager.GetComponent<DatosJugador>();
    }

    private void Update()
    {
        TakeController();
    }

    void TakeController()
    {
        touched = Physics2D.IsTouchingLayers(coll, layerPlayer);

        if (touched)
        {
            AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);

            switch (TypeKey)
            {
                case typeKey.blueKey:
                    datosJugador.blueKey = true;
                    break;
                case typeKey.cianKey:
                    datosJugador.cianKey = true;
                    break;
                case typeKey.redKey:
                    datosJugador.redKey = true;
                    break;
                case typeKey.yellowKey:
                    datosJugador.yellowKey = true;
                    break;
                case typeKey.pinkKey:
                    datosJugador.pinkKey = true;
                    break;
                case typeKey.greenKey:
                    datosJugador.greenKey = true;
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
