using System.Collections;
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
    public BrokenWallManager wallBrkn3;
    public BrokenWallManager wallBrkn4;
    
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
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            ContarRooms();
        }
    }

    public void CheckWalls()
    {
        datosJugador.wallBroken1 = wallBrkn1.wasBroken;
        datosJugador.wallBroken2 = wallBrkn2.wasBroken;
        datosJugador.wallBroken3 = wallBrkn3.wasBroken;
        datosJugador.wallBroken4 = wallBrkn4.wasBroken;
    }

    public void BrokenWallsOnLoadGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de loadgame
        FindBrokenWalls();

        wallBrkn1.wasBroken = datosJugador.wallBroken1;
        wallBrkn2.wasBroken = datosJugador.wallBroken2;
        wallBrkn3.wasBroken = datosJugador.wallBroken3;
        wallBrkn4.wasBroken = datosJugador.wallBroken4;
    }

    public void BrokenWallsOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindBrokenWalls();

        wallBrkn1.wasBroken = false;
        wallBrkn2.wasBroken = false;
        wallBrkn3.wasBroken = false;
        wallBrkn4.wasBroken = false;
        
        CheckWalls();
    }

    void FindBrokenWalls()
    {
        wallBrkn1 = GameObject.Find("BrokenWallManager").GetComponent<BrokenWallManager>();
        wallBrkn2 = GameObject.Find("BrokenWallManager2").GetComponent<BrokenWallManager>();
        wallBrkn3 = GameObject.Find("BrokenWallManager3").GetComponent<BrokenWallManager>();
        wallBrkn4 = GameObject.Find("BrokenWallManager4").GetComponent<BrokenWallManager>();
    }

    void FindMapParts()
    {
       mapParts = GameObject.FindObjectsOfType<BoundaryManager>();
    }

    //agregar mas partes de mapa a medida que se va creando mas
    public void CheckMap()
    {
        if (!GameManager.gameManager.debugMode)
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
            datosJugador.mapPart11B = mapParts[12].mapActive;   //zona extra
            datosJugador.mapPart12 = mapParts[13].mapActive;
            datosJugador.mapPart13 = mapParts[14].mapActive;
            datosJugador.mapPart14 = mapParts[15].mapActive;
            datosJugador.mapPart15 = mapParts[16].mapActive;
            datosJugador.mapPart16 = mapParts[17].mapActive;
            datosJugador.mapPart16B = mapParts[18].mapActive;   //zona extra
            datosJugador.mapPart17 = mapParts[19].mapActive;
            datosJugador.mapPart18 = mapParts[20].mapActive;
            datosJugador.mapPart19 = mapParts[21].mapActive;
            datosJugador.mapPart20 = mapParts[22].mapActive;
            datosJugador.mapPart21 = mapParts[23].mapActive;
            datosJugador.mapPart22 = mapParts[24].mapActive;
            datosJugador.mapPart23 = mapParts[25].mapActive;
            datosJugador.mapPart24 = mapParts[26].mapActive;    //precipicio saveroom
            datosJugador.mapPart25 = mapParts[27].mapActive;    
            datosJugador.mapPart26 = mapParts[28].mapActive;    
            datosJugador.mapPart27 = mapParts[29].mapActive;    
            datosJugador.mapPart28 = mapParts[30].mapActive;    //saveRoom Level5
            datosJugador.mapPart29 = mapParts[31].mapActive;    
            datosJugador.mapPart30 = mapParts[32].mapActive;    
            datosJugador.mapPart31 = mapParts[33].mapActive;    
            datosJugador.mapPart32 = mapParts[34].mapActive;    
            datosJugador.mapPart33 = mapParts[35].mapActive;    
            datosJugador.mapPart34 = mapParts[36].mapActive;    
            datosJugador.mapPart35 = mapParts[37].mapActive;    //saveRoom level6
            datosJugador.mapPart36 = mapParts[38].mapActive;    
            datosJugador.mapPart37 = mapParts[39].mapActive;    
            datosJugador.mapPart38 = mapParts[40].mapActive;    
            datosJugador.mapPart39 = mapParts[41].mapActive;    
            datosJugador.mapPart40 = mapParts[42].mapActive;    
            datosJugador.mapPart38B = mapParts[43].mapActive;   //saveRoom PreDracula 
        }
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
        mapParts[12].mapActive = datosJugador.mapPart11B;   //zona extra
        mapParts[13].mapActive = datosJugador.mapPart12;
        mapParts[14].mapActive = datosJugador.mapPart13;
        mapParts[15].mapActive = datosJugador.mapPart14;
        mapParts[16].mapActive = datosJugador.mapPart15;
        mapParts[17].mapActive = datosJugador.mapPart16;
        mapParts[18].mapActive = datosJugador.mapPart16B;   //zona extra
        mapParts[19].mapActive = datosJugador.mapPart17;
        mapParts[20].mapActive = datosJugador.mapPart18;
        mapParts[21].mapActive = datosJugador.mapPart19;
        mapParts[22].mapActive = datosJugador.mapPart20;
        mapParts[23].mapActive = datosJugador.mapPart21;
        mapParts[24].mapActive = datosJugador.mapPart22;
        mapParts[25].mapActive = datosJugador.mapPart23;
        mapParts[26].mapActive = datosJugador.mapPart24;    //precipicio saveroom
        mapParts[27].mapActive = datosJugador.mapPart25;    
        mapParts[28].mapActive = datosJugador.mapPart26;    
        mapParts[29].mapActive = datosJugador.mapPart27;    
        mapParts[30].mapActive = datosJugador.mapPart28;    //saveRoom Level5
        mapParts[31].mapActive = datosJugador.mapPart29;    
        mapParts[32].mapActive = datosJugador.mapPart30;    
        mapParts[33].mapActive = datosJugador.mapPart31;    
        mapParts[34].mapActive = datosJugador.mapPart32;    
        mapParts[35].mapActive = datosJugador.mapPart33;    
        mapParts[36].mapActive = datosJugador.mapPart34;    
        mapParts[37].mapActive = datosJugador.mapPart35;    //saveRoom level6
        mapParts[38].mapActive = datosJugador.mapPart36;    
        mapParts[39].mapActive = datosJugador.mapPart37;    
        mapParts[40].mapActive = datosJugador.mapPart38;    
        mapParts[41].mapActive = datosJugador.mapPart39;    
        mapParts[42].mapActive = datosJugador.mapPart40;    
        mapParts[43].mapActive = datosJugador.mapPart38B;   //saveRoom Predracula 
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
