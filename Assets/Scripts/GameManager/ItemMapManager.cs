using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ItemMapManager : MonoBehaviour
{
    DatosJugador datosJugador;

    public SpawnUniqueItems spwnItemU1;
    public SpawnUniqueItems spwnItemU2;
    public SpawnUniqueItems spwnItemU3;


    //a medida que se agregan mas items unicos nuevos, se deben poner tambien en este archivo y en el DatosJugador
    //agregar en CheckItems(), ItemsMapOnLoadGame(), ItemsMapOnStartGame(), FindUniqueItems()
    private void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
    }

    public void CheckItems()
    {
        datosJugador.wasCaughtItem1 = spwnItemU1.wasCaught;
        datosJugador.wasCaughtItem2 = spwnItemU2.wasCaught;
        datosJugador.wasCaughtItem3 = spwnItemU3.wasCaught;
    }
    
    //incrementar este listado de items entre mas se agreguen al mapa
    public void ItemsMapOnLoadGame()
    {
        FindUniqueItems();

        spwnItemU1.wasCaught = datosJugador.wasCaughtItem1;
        spwnItemU2.wasCaught = datosJugador.wasCaughtItem2;
        spwnItemU3.wasCaught = datosJugador.wasCaughtItem3;
    }

    //incrementar este listado de items entre mas se agreguen al mapa
    public void ItemsMapOnStartGame()
    {
        FindUniqueItems();
        //se reinician los items del mapa
        spwnItemU1.wasCaught = false;
        spwnItemU2.wasCaught = false;
        spwnItemU3.wasCaught = false;

        CheckItems();
    }

    void FindUniqueItems()
    {
        spwnItemU1 = GameObject.Find("ItemSpawnerOnly1").GetComponent<SpawnUniqueItems>();
        spwnItemU2 = GameObject.Find("ItemSpawnerOnly2").GetComponent<SpawnUniqueItems>();
        spwnItemU3 = GameObject.Find("ItemSpawnerOnly3").GetComponent<SpawnUniqueItems>();
    }
}
