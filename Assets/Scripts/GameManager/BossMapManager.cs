using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossMapManager : MonoBehaviour
{
    public BossSpawner bossBat;
    public MedusaStatueController bossMedusa;

    public bool bossBatDefeated;
    public bool bossMedusaDefeated;

    [Header("BOSS DEL MAPA SALVADOS")]
    public bool bossBatSav;
    public bool bossMedusaSav;


    public void CheckBoss()
    {
        bossBatDefeated = bossBat.isDead;
        bossMedusaDefeated = bossMedusa.medusaDefeated;
    }

    public void BossMapOnLoadGame()
    {
        //primero localizamos a los Boss del mapa para ahi si pasarle caracteristicas de loadgame
        FindBossSpawner();
        bossBat.isDead = Convert.ToBoolean(PlayerPrefs.GetInt("BossBat"));
        bossMedusa.medusaDefeated = Convert.ToBoolean(PlayerPrefs.GetInt("BossMedusa"));

        bossBatSav = Convert.ToBoolean(PlayerPrefs.GetInt("BossBat"));
        bossMedusaSav = Convert.ToBoolean(PlayerPrefs.GetInt("BossMedusa"));

        CheckBoss();
    }

    public void BossMapOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindBossSpawner();
        bossBat.isDead = false;
        bossBatSav = bossBat.isDead;

        bossMedusa.medusaDefeated = false;
        bossMedusaSav = bossMedusa.medusaDefeated;

        CheckBoss();
    }


    void FindBossSpawner()
    {
        bossBat = GameObject.Find("BossSpawner1").GetComponent<BossSpawner>();
        bossMedusa = GameObject.Find("MedusaStatue").GetComponent<MedusaStatueController>();
    }
}
