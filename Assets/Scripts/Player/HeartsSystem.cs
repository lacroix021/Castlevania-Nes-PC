using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsSystem : MonoBehaviour
{

    public int MaxHearts;
    

    DatosJugador datosJugador;

    private void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
    }

    public void CheckHearts()
    {
        datosJugador.currentHearts = Mathf.Clamp(datosJugador.currentHearts, 0, MaxHearts);
    } 
}
