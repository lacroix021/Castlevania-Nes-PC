using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowKnife : MonoBehaviour
{
    public GameObject shadow;
    private float shadowTimeCD;
    public float shadowTime;
    SubWeaponSystem weaponSys;
    DatosJugador datosJugador;

    // Start is called before the first frame update
    void Start()
    {
        weaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        shadowTimeCD = shadowTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (datosJugador.multiplierPow == 2)
        {
            if (shadowTimeCD > 0)
            {
                shadowTimeCD -= Time.deltaTime;
            }
            else
            {
                InstantiateShadow();
                shadowTimeCD = shadowTime;
            }
        }
    }
    public void InstantiateShadow()
    {
        GameObject currentShadow = Instantiate(shadow, transform.position, Quaternion.identity);
        currentShadow.transform.localScale = transform.localScale;
        currentShadow.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
        currentShadow.GetComponent<SpriteRenderer>().color = new Vector4(10, 0, 10, 0.3f);
        Destroy(currentShadow, 0.568f);
    }
}
