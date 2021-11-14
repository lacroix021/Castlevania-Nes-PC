using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossMapManager : MonoBehaviour
{
    DatosJugador datosJugador;

    public BossSpawner bossBat;
    public MedusaStatueController bossMedusa;

    //a medida que se agregan mas boss al juego, tambien agregar sus respectivos estados en este archivo
    //se deben agregar al CheckBoss(), BossMapOnLoadGame(), BossMapOnStartGame(), FindBossSpawner()
    private void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
    }

    public void CheckBoss()
    {
        datosJugador.bossBatDefeated = bossBat.isDead;
        datosJugador.bossMedusaDefeated = bossMedusa.medusaDefeated;
    }

    public void BossMapOnLoadGame()
    {
        //primero localizamos a los Boss del mapa para ahi si pasarle caracteristicas de loadgame
        FindBossSpawner();
        bossBat.isDead = datosJugador.bossBatDefeated;
        bossMedusa.medusaDefeated = datosJugador.bossMedusaDefeated;
    }

    public void BossMapOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindBossSpawner();
        bossBat.isDead = false;
        bossMedusa.medusaDefeated = false;

        CheckBoss();
    }


    void FindBossSpawner()
    {
        bossBat = GameObject.Find("BossSpawner1").GetComponent<BossSpawner>();
        bossMedusa = GameObject.Find("MedusaStatue").GetComponent<MedusaStatueController>();
    }
}
