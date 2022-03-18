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
    public SpawnUniqueItems spwnItemU4;
    public SpawnUniqueItems spwnItemU5;
    public SpawnUniqueItems spwnItemU6;
    public SpawnUniqueItems spwnItemU7;
    public SpawnUniqueItems spwnItemU8;
    public SpawnUniqueItems spwnItemU9;
    public SpawnUniqueItems spwnItemU10;


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
        datosJugador.wasCaughtItem4 = spwnItemU4.wasCaught;
        datosJugador.wasCaughtItem5 = spwnItemU5.wasCaught;
        datosJugador.wasCaughtItem6 = spwnItemU6.wasCaught;
        datosJugador.wasCaughtItem7 = spwnItemU7.wasCaught;
        datosJugador.wasCaughtItem8 = spwnItemU8.wasCaught;
        datosJugador.wasCaughtItem9 = spwnItemU9.wasCaught;
        datosJugador.wasCaughtItem10 = spwnItemU10.wasCaught;
    }
    
    //incrementar este listado de items entre mas se agreguen al mapa
    public void ItemsMapOnLoadGame()
    {
        FindUniqueItems();

        spwnItemU1.wasCaught = datosJugador.wasCaughtItem1;
        spwnItemU2.wasCaught = datosJugador.wasCaughtItem2;
        spwnItemU3.wasCaught = datosJugador.wasCaughtItem3;
        spwnItemU4.wasCaught = datosJugador.wasCaughtItem4;
        spwnItemU5.wasCaught = datosJugador.wasCaughtItem5;
        spwnItemU6.wasCaught = datosJugador.wasCaughtItem6;
        spwnItemU7.wasCaught = datosJugador.wasCaughtItem7;
        spwnItemU8.wasCaught = datosJugador.wasCaughtItem8;
        spwnItemU9.wasCaught = datosJugador.wasCaughtItem9;
        spwnItemU10.wasCaught = datosJugador.wasCaughtItem10;
    }

    //incrementar este listado de items entre mas se agreguen al mapa
    public void ItemsMapOnStartGame()
    {
        FindUniqueItems();
        //se reinician los items del mapa
        spwnItemU1.wasCaught = false;
        spwnItemU2.wasCaught = false;
        spwnItemU3.wasCaught = false;
        spwnItemU4.wasCaught = false;
        spwnItemU5.wasCaught = false;
        spwnItemU6.wasCaught = false;
        spwnItemU7.wasCaught = false;
        spwnItemU8.wasCaught = false;
        spwnItemU9.wasCaught = false;
        spwnItemU10.wasCaught = false;

        CheckItems();
    }

    void FindUniqueItems()
    {
        spwnItemU1 = GameObject.Find("ItemSpawnerOnly1").GetComponent<SpawnUniqueItems>();
        spwnItemU2 = GameObject.Find("ItemSpawnerOnly2").GetComponent<SpawnUniqueItems>();
        spwnItemU3 = GameObject.Find("ItemSpawnerOnly3").GetComponent<SpawnUniqueItems>();
        spwnItemU4 = GameObject.Find("ItemSpawnerOnly4").GetComponent<SpawnUniqueItems>();
        spwnItemU5 = GameObject.Find("ItemSpawnerOnly5").GetComponent<SpawnUniqueItems>();
        spwnItemU6 = GameObject.Find("ItemSpawnerOnly6").GetComponent<SpawnUniqueItems>();
        spwnItemU7 = GameObject.Find("ItemSpawnerOnly7").GetComponent<SpawnUniqueItems>();
        spwnItemU8 = GameObject.Find("ItemSpawnerOnly8").GetComponent<SpawnUniqueItems>();
        spwnItemU9 = GameObject.Find("ItemSpawnerOnly9").GetComponent<SpawnUniqueItems>();
        spwnItemU10 = GameObject.Find("ItemSpawnerOnly10").GetComponent<SpawnUniqueItems>();
    }
}
