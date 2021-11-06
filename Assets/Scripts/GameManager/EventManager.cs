using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public PitEvent pitEvent;

    public bool pitActive;

    [Header("EVENTOS DEL MAPA SALVADOS")]
    public bool pitActiveSav;


    public void CheckEvents()
    {
        pitActive = pitEvent.pitEnable;
    }

    public void EventOnLoadGame()
    {
        //primero localizamos los eventos del mapa para ahi si pasarle caracteristicas de loadgame
        FindEvents();
        pitEvent.pitEnable = Convert.ToBoolean(PlayerPrefs.GetInt("PitEvent"));
        pitActiveSav = Convert.ToBoolean(PlayerPrefs.GetInt("PitEvent"));
        CheckEvents();
    }

    public void EventOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindEvents();
        pitEvent.pitEnable = false;
        pitActiveSav = pitEvent.pitEnable;
        CheckEvents();
    }

    void FindEvents()
    {
        pitEvent = GameObject.Find("EventPit").GetComponent<PitEvent>();
    }
}
