using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    DatosJugador datosJugador;

    public PitEvent pitEvent;

    //a medida que se agregan mas eventos al juego, tambien agregar sus respectivos estados en este archivo
    //se deben agregar al CheckEvents(), EventOnLoadGame(), EventOnStartGame(), FindEvents()
    private void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
    }

    public void CheckEvents()
    {
        datosJugador.pitActive = pitEvent.pitEnable;
    }

    public void EventOnLoadGame()
    {
        //primero localizamos los eventos del mapa para ahi si pasarle caracteristicas de loadgame
        FindEvents();
        pitEvent.pitEnable = datosJugador.pitActive;
        
    }

    public void EventOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindEvents();
        pitEvent.pitEnable = false;
        
        CheckEvents();
    }

    void FindEvents()
    {
        pitEvent = GameObject.Find("EventPit").GetComponent<PitEvent>();
    }
}
