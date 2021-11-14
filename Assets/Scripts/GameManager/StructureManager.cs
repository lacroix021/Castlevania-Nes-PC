﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class StructureManager : MonoBehaviour
{
    DatosJugador datosJugador;

    public BrokenWallManager wallBrkn1;
    public BrokenWallManager wallBrkn2;
    
    public BoundaryManager[] mapParts;
    public Text percentMapTxt;
    //public float percentMap;
    public float percentPerRoom;
    public int roomsActive;
    public int roomsTotal;

    //a medida que ponga mas paredes rompibles o mas secciones de mapa, se deben agregar a este archivo en sus respectivos campos
    private void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ContarRooms();
        }
    }

    public void CheckWalls()
    {
        datosJugador.wallBroken1 = wallBrkn1.wasBroken;
        datosJugador.wallBroken2 = wallBrkn2.wasBroken;
    }

    public void BrokenWallsOnLoadGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de loadgame
        FindBrokenWalls();

        wallBrkn1.wasBroken = datosJugador.wallBroken1;
        wallBrkn2.wasBroken = datosJugador.wallBroken2;
    }

    public void BrokenWallsOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindBrokenWalls();

        wallBrkn1.wasBroken = false;
        wallBrkn2.wasBroken = false;
        
        CheckWalls();
    }

    void FindBrokenWalls()
    {
        wallBrkn1 = GameObject.Find("BrokenWallManager").GetComponent<BrokenWallManager>();
        wallBrkn2 = GameObject.Find("BrokenWallManager2").GetComponent<BrokenWallManager>();
    }

    void FindMapParts()
    {
       mapParts = GameObject.FindObjectsOfType<BoundaryManager>();
    }

    //agregar mas partes de mapa a medida que se va creando mas
    public void CheckMap()
    {
        datosJugador.mapPart0 = mapParts[0].mapActive;
        datosJugador.mapPart1 = mapParts[1].mapActive;
        datosJugador.mapPart2 = mapParts[2].mapActive;
        datosJugador.mapPart3 = mapParts[3].mapActive;
        datosJugador.mapPart4 = mapParts[4].mapActive;
        datosJugador.mapPart5 = mapParts[5].mapActive;
        datosJugador.mapPart6 = mapParts[6].mapActive;
        datosJugador.mapPart7 = mapParts[7].mapActive;
        datosJugador.mapPart8 = mapParts[8].mapActive;
        datosJugador.mapPart9 = mapParts[9].mapActive;
        datosJugador.mapPart10 = mapParts[10].mapActive;
        datosJugador.mapPart11 = mapParts[11].mapActive;
        datosJugador.mapPart12 = mapParts[12].mapActive;
        datosJugador.mapPart13 = mapParts[13].mapActive;
        datosJugador.mapPart14 = mapParts[14].mapActive;
    }

    public void MapPartsLoadGame()
    {
        FindMapParts();

        mapParts[0].mapActive = datosJugador.mapPart0;
        mapParts[1].mapActive = datosJugador.mapPart1;
        mapParts[2].mapActive = datosJugador.mapPart2;
        mapParts[3].mapActive = datosJugador.mapPart3;
        mapParts[4].mapActive = datosJugador.mapPart4;
        mapParts[5].mapActive = datosJugador.mapPart5;
        mapParts[6].mapActive = datosJugador.mapPart6;
        mapParts[7].mapActive = datosJugador.mapPart7;
        mapParts[8].mapActive = datosJugador.mapPart8;
        mapParts[9].mapActive = datosJugador.mapPart9;
        mapParts[10].mapActive = datosJugador.mapPart10;
        mapParts[11].mapActive = datosJugador.mapPart11;
        mapParts[12].mapActive = datosJugador.mapPart12;
        mapParts[13].mapActive = datosJugador.mapPart13;
        mapParts[14].mapActive = datosJugador.mapPart14;
    }

    public void MapPartsStartGame()
    {
        FindMapParts();

        for (int i = 0; i < mapParts.Length; i++)
        {
            mapParts[i].mapActive = false;
            
        }

        CheckMap();
    }

    void ContarRooms()
    {
        GameObject[] maps = GameObject.FindGameObjectsWithTag("map");
        roomsActive = maps.Length;

        //la operacion es dividir 100 por la cantidad de cuartos total
        //ese valor es lo que puse a multiplicar por cada roomActivo y asi su totalidad da 100%
        //hay que añadir todos los decimales para ser exactos por eso puse 100.0f
        roomsTotal = mapParts.Length;
        percentPerRoom = 100.0f / roomsTotal;
        datosJugador.percentMap = roomsActive * percentPerRoom;
        percentMapTxt.text = datosJugador.percentMap.ToString("F0");
    }
    
    //agregar mas partes de mapa a medida que se va creando mas para guardar la partida
    
}
