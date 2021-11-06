using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ItemMapManager : MonoBehaviour
{

    public SpawnUniqueItems spwnItemU1;
    public SpawnUniqueItems spwnItemU2;

    public bool lifeMax1Taken;
    public bool lifeMax2Taken;

    [Header("ITEMS DEL MAPA SALVADOS")]
    public bool lifeMax1Sav;
    public bool lifeMax2Sav;

    
    public void CheckItems()
    {
        lifeMax1Taken = spwnItemU1.wasCaught;
        lifeMax2Taken = spwnItemU2.wasCaught;
    }
    
    //incrementar este listado de items entre mas se agreguen al mapa
    public void ItemsMapOnLoadGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de loadgame
        FindUniqueItems();

        spwnItemU1.wasCaught = Convert.ToBoolean(PlayerPrefs.GetInt("LifeMax1"));
        spwnItemU2.wasCaught = Convert.ToBoolean(PlayerPrefs.GetInt("LifeMax2"));

        lifeMax1Sav = Convert.ToBoolean(PlayerPrefs.GetInt("LifeMax1"));
        lifeMax2Sav = Convert.ToBoolean(PlayerPrefs.GetInt("LifeMax2"));
        CheckItems();
    }

    //incrementar este listado de items entre mas se agreguen al mapa
    public void ItemsMapOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindUniqueItems();

        spwnItemU1.wasCaught = false;
        spwnItemU2.wasCaught = false;

        lifeMax1Sav = spwnItemU1.wasCaught;
        lifeMax2Sav = spwnItemU2.wasCaught;
        CheckItems();
    }

    void FindUniqueItems()
    {
        spwnItemU1 = GameObject.Find("ItemSpawnerOnly1").GetComponent<SpawnUniqueItems>();
        spwnItemU2 = GameObject.Find("ItemSpawnerOnly2").GetComponent<SpawnUniqueItems>();
    }
}
