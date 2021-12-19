using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject shadow;
    private float shadowTimeCD;
    public float shadowTime;
    float z = 0;

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

        if(datosJugador.multiplierPow == 2)
        {
            currentShadow.GetComponent<SpriteRenderer>().color = new Vector4(10, 0, 0, 0.6f);
        }
        else
        {
            currentShadow.GetComponent<SpriteRenderer>().color = new Vector4(10, 0, 10, 0.3f);
        }
    }
}
