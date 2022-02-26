using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossMapManager : MonoBehaviour
{
    DatosJugador datosJugador;

    public BossSpawner bossBat;
    public MedusaStatueController bossMedusa;
    public CeilingSpawnerMummies ceilingMummies;
    public BossSpawner frankenSpawner;
    public BossSpawner deathSpawner;

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
        datosJugador.bossMummyA = ceilingMummies.isDeadA;
        datosJugador.bossMummyB = ceilingMummies.isDeadB;
        datosJugador.floorBrokenLevel3 = ceilingMummies.interruptorOn;  //SE DEBE MIGRAR AL STRUCTURE MANAGER
        datosJugador.wallBrokenLevel3 = ceilingMummies.wallBroken;  //SE DEBE MIGRAR AL STRUCTURE MANAGER
        datosJugador.bossFranken = frankenSpawner.isDead;
        datosJugador.bossDeath = deathSpawner.isDead;
    }

    public void BossMapOnLoadGame()
    {
        //primero localizamos a los Boss del mapa para ahi si pasarle caracteristicas de loadgame
        FindBossSpawner();
        bossBat.isDead = datosJugador.bossBatDefeated;
        bossMedusa.medusaDefeated = datosJugador.bossMedusaDefeated;
        ceilingMummies.isDeadA = datosJugador.bossMummyA;
        ceilingMummies.isDeadB = datosJugador.bossMummyB;
        ceilingMummies.interruptorOn = datosJugador.floorBrokenLevel3; //SE DEBE MIGRAR AL STRUCTURE MANAGER
        ceilingMummies.wallBroken = datosJugador.wallBrokenLevel3;  //SE DEBE MIGRAR AL STRUCTURE MANAGER
        frankenSpawner.isDead = datosJugador.bossFranken;
        deathSpawner.isDead = datosJugador.bossDeath;
    }

    public void BossMapOnStartGame()
    {
        //primero localizamos a los items del mapa que son unicos para ahi si pasarle caracteristicas de newgame
        FindBossSpawner();
        bossBat.isDead = false;
        bossMedusa.medusaDefeated = false;
        ceilingMummies.isDeadA = false;
        ceilingMummies.isDeadB = false;
        ceilingMummies.interruptorOn = false;     //evento de piso roto en nivel3 SE DEBE MIGRAR AL STRUCTURE MANAGER
        ceilingMummies.wallBroken = false;     //evento de pared rota en nivel3 SE DEBE MIGRAR AL STRUCTURE MANAGER}
        frankenSpawner.isDead = false;
        deathSpawner.isDead = false;

        CheckBoss();
    }


    void FindBossSpawner()
    {
        bossBat = GameObject.Find("BossBatSpawner").GetComponent<BossSpawner>();
        bossMedusa = GameObject.Find("MedusaStatue").GetComponent<MedusaStatueController>();
        ceilingMummies = GameObject.Find("CeilingMummies").GetComponent<CeilingSpawnerMummies>();
        frankenSpawner = GameObject.Find("BossFrankenSpawner").GetComponent<BossSpawner>();
        deathSpawner = GameObject.Find("BossDeathSpawner").GetComponent<BossSpawner>();
    }
}
