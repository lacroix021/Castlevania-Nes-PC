using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarCargar : MonoBehaviour
{
    public DatosJugador datosJugador;

    //este script debe ir en un objeto que se encuentre desde el inicio del juego 
    //ya que debe viajar entre todas las escenas para guardar y para cargar
    //como en el GameManager
    
    public static void GuardarPartida(MonoBehaviour informacion)
    {
        PlayerPrefs.SetString("DataPlayer", JsonUtility.ToJson(informacion));
        //Debug.Log(JsonUtility.ToJson(informacion));
    }

    public static void CargarPartida(MonoBehaviour informacion)
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("DataPlayer"), informacion);
    }

    //esta funcion debe ir en el objeto que realizara el guardado de partida
    public void GuardarInformacion()
    {
        GuardarPartida(datosJugador);
    }

    //esta funcion debe ir en el boton de loadgame en el menu principal
    public void CargarInformacion()
    {
        CargarPartida(datosJugador);
    }
}
