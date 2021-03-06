using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject shadow;
    private float shadowTimeCD;
    public float shadowTime;
    float z = 0;
    public Color shadowColorAxe;
    public Color shadowColorAxePow;
    public Color shadowColorCross;
    DatosJugador datosJugador;
    // Start is called before the first frame update
    void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        shadowTimeCD = shadowTime;
    }

    // Update is called once per frame
    void Update()
    {
        z += 100;

        if(shadowTimeCD > 0)
        {
            shadowTimeCD -= Time.deltaTime;
        }
        else
        {
            InstantiateShadow();
            shadowTimeCD = shadowTime;
        }
    }
    public void InstantiateShadow()
    {
        GameObject currentShadow = Instantiate(shadow, transform.position, Quaternion.Euler(0, 0, z));
        currentShadow.transform.localScale = transform.localScale;
        currentShadow.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
        Destroy(currentShadow, 0.568f);

        if(datosJugador.multiplierPow == 2 && datosJugador.typeSub == 1)
        {
            currentShadow.GetComponent<SpriteRenderer>().color = shadowColorAxePow;
        }
        else
        {
            currentShadow.GetComponent<SpriteRenderer>().color = shadowColorAxe;
        }

        if(datosJugador.typeSub == 3)
        {
            currentShadow.GetComponent<SpriteRenderer>().color = shadowColorCross;
        }
    }
}
