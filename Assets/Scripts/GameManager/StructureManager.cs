using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StructureManager : MonoBehaviour
{
    public BrokenWallManager wallBrkn1;
    public BrokenWallManager wallBrkn2;

    public bool wallBroken1;
    public bool wallBroken2;


    [Header("MUROS DEL MAPA SALVADOS")]
    public bool wallBroken1Sav;
    public bool wallBroken2Sav;

    /*partes del mapa agregar mas bools entre mas partes de mapa tengamos en la escena*/
    public BoundaryManager[] mapParts;

    /******************************************/
    [Header("PARTES DEL MAPA")]
    public bool mapPart0;
    public bool mapPart1;
    public bool mapPart2;
    public bool mapPart3;
    public bool mapPart4;
    public bool mapPart5;
    public bool mapPart6;
    public bool mapPart7;
    public bool mapPart8;
    public bool mapPart9;
    public bool mapPart10;
    public bool mapPart11;
    public bool mapPart12;
    public bool mapPart13;
    public bool mapPart14;

    //agregar mas partes de mapa a medida que se va creando mas
    [Header("PARTES DEL MAPA SALVADOS")]

    public bool mapPart0Sav;
    public bool mapPart1Sav;
    public bool mapPart2Sav;
    public bool mapPart3Sav;
    public bool mapPart4Sav;
    public bool mapPart5Sav;
    public bool mapPart6Sav;
    public bool mapPart7Sav;
    public bool mapPart8Sav;
    public bool mapPart9Sav;
    public bool mapPart10Sav;
    public bool mapPart11Sav;
    public bool mapPart12Sav;
    public bool mapPart13Sav;
    public bool mapPart14Sav;

    /******************************************/

    public void CheckWalls()
    {
        wallBroken1 = wallBrkn1.wasBroken;
        wallBroken2 = wallBrkn2.wasBroken;
    }

    public void BrokenWallsOnLoadGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de loadgame
        FindBrokenWalls();
        

        wallBrkn1.wasBroken = Convert.ToBoolean(PlayerPrefs.GetInt("WallBroken1"));
        wallBrkn2.wasBroken = Convert.ToBoolean(PlayerPrefs.GetInt("WallBroken2"));

        wallBroken1Sav = Convert.ToBoolean(PlayerPrefs.GetInt("WallBroken1"));
        wallBroken2Sav = Convert.ToBoolean(PlayerPrefs.GetInt("WallBroken2"));

        CheckWalls();
    }

    public void BrokenWallsOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindBrokenWalls();

        wallBrkn1.wasBroken = false;
        wallBrkn2.wasBroken = false;

        wallBroken1Sav = wallBrkn1.wasBroken;
        wallBroken2Sav = wallBrkn1.wasBroken;
        
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
        mapPart0 = mapParts[0].mapActive;
        mapPart1 = mapParts[1].mapActive;
        mapPart2 = mapParts[2].mapActive;
        mapPart3 = mapParts[3].mapActive;
        mapPart4 = mapParts[4].mapActive;
        mapPart5 = mapParts[5].mapActive;
        mapPart6 = mapParts[6].mapActive;
        mapPart7 = mapParts[7].mapActive;
        mapPart8 = mapParts[8].mapActive;
        mapPart9 = mapParts[9].mapActive;
        mapPart10 = mapParts[10].mapActive;
        mapPart11 = mapParts[11].mapActive;
        mapPart12 = mapParts[12].mapActive;
        mapPart13 = mapParts[13].mapActive;
        mapPart14 = mapParts[14].mapActive;
    }

    //agregar mas partes de mapa a medida que se va creando mas
    public void MapPartsStartGame()
    {
        FindMapParts();

        for (int i = 0; i < mapParts.Length; i++)
        {
            mapParts[i].mapActive = false;
            mapPart0Sav = mapParts[0].mapActive;
            mapPart1Sav = mapParts[1].mapActive;
            mapPart2Sav = mapParts[2].mapActive;
            mapPart3Sav = mapParts[3].mapActive;
            mapPart4Sav = mapParts[4].mapActive;
            mapPart5Sav = mapParts[5].mapActive;
            mapPart6Sav = mapParts[6].mapActive;
            mapPart7Sav = mapParts[7].mapActive;
            mapPart8Sav = mapParts[8].mapActive;
            mapPart9Sav = mapParts[9].mapActive;
            mapPart10Sav = mapParts[10].mapActive;
            mapPart11Sav = mapParts[11].mapActive;
            mapPart12Sav = mapParts[12].mapActive;
            mapPart13Sav = mapParts[13].mapActive;
            mapPart14Sav = mapParts[14].mapActive;
        }

        CheckMap();
    }

    //agregar mas partes de mapa a medida que se va creando mas para guardar la partida
    public void MapPartsLoadGame()
    {
        FindMapParts();

        mapParts[0].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap0"));
        mapParts[1].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap1"));
        mapParts[2].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap2"));
        mapParts[3].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap3"));
        mapParts[4].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap4"));
        mapParts[5].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap5"));
        mapParts[6].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap6"));
        mapParts[7].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap7"));
        mapParts[8].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap8"));
        mapParts[9].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap9"));
        mapParts[10].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap10"));
        mapParts[11].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap11"));
        mapParts[12].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap12"));
        mapParts[13].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap13"));
        mapParts[14].mapActive = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap14"));

        mapPart0Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap0"));
        mapPart1Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap1"));
        mapPart2Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap2"));
        mapPart3Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap3"));
        mapPart4Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap4"));
        mapPart5Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap5"));
        mapPart6Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap6"));
        mapPart7Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap7"));
        mapPart8Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap8"));
        mapPart9Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap9"));
        mapPart10Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap10"));
        mapPart11Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap11"));
        mapPart12Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap12"));
        mapPart13Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap13"));
        mapPart14Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap14"));

        CheckMap();
    }
}
