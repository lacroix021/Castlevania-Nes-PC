using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnkOfLife : MonoBehaviour
{
    public ParticleSystem particleA;
    public ParticleSystem particleB;

    GameManager gManager;
    HealthPlayer playerHealth;

    ItemMapManager itemMapManager;
    BossMapManager bossManager;
    EventManager eventManager;
    StructureManager structureManager;

    AudioSource aSource;

    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posY, timer, ySen;
    /// </summary>

    float timeSaveRate;
    public float saveRate;

    // Start is called before the first frame update
    void Start()
    {
        
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        gManager = GameManager.gameManager;
        posY = transform.position.y;
        itemMapManager = gManager.GetComponentInChildren<ItemMapManager>();
        bossManager = gManager.GetComponentInChildren<BossMapManager>();
        eventManager = gManager.GetComponentInChildren<EventManager>();
        structureManager = gManager.GetComponentInChildren<StructureManager>();
        aSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        MovementAnk();
    }

    void MovementAnk()
    {
        //movimiento senoidal
        timer = timer + (frecuency / 100);
        ySen = Mathf.Sin(timer);
        transform.position = new Vector3(transform.position.x, posY + (ySen * cycleWidth), transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && Input.GetAxisRaw("Vertical") > 0)
        {
            if(Time.time >= timeSaveRate)
            {
                particleA.Play();
                particleB.Play();
                //funcion de guardado
                SaveGame();
                gManager.Saves += 1;
                PlayerPrefs.SetInt("Saves", gManager.Saves); //cargar esto al darle loadgame en el menu principal
                timeSaveRate = Time.time + 1 / saveRate;
            }
        }
    }

    void SaveGame()
    {
        aSource.loop = false;
        aSource.Play();

        //cura al jugador al guardar
        playerHealth.healing = true;
        playerHealth.vidaACurar = playerHealth.maxHealth;

        //------------------------------------------------------
        //STATS BASE (vida maxima, posicion, oro, corazones, subArma, tipo de latigo)
        //datos del jugador salvados
        PlayerPrefs.SetFloat("PlayerMaxHealth", gManager.currentMaxHealth);
        PlayerPrefs.SetFloat("PlayerPosX", gManager.posPlayer.x);
        PlayerPrefs.SetFloat("PlayerPosY", gManager.posPlayer.y);
        PlayerPrefs.SetInt("PlayerGold", gManager.gold);
        PlayerPrefs.SetInt("CostRespawn", gManager.costRespawn);
        PlayerPrefs.SetInt("PlayerHearts", gManager.currentHearts);
        PlayerPrefs.SetInt("PlayerTypeSub", gManager.currentTypeSub);
        PlayerPrefs.SetInt("PlayerMultiplierPow", gManager.currentMultiplierPow);
        PlayerPrefs.SetInt("PlayerHaveSub", Convert.ToInt32(gManager.haveSubW));
        PlayerPrefs.SetInt("PlayerTypeWhip", gManager.currentTypeWhip);

        //datos del jugador pasados a variable para su posterior carga al hacer respawn
        gManager.playerMaxHealthSav = PlayerPrefs.GetFloat("PlayerMaxHealth");
        gManager.playerPosXSav = PlayerPrefs.GetFloat("PlayerPosX");
        gManager.playerPosYSav = PlayerPrefs.GetFloat("PlayerPosY");
        gManager.playerGoldSav = PlayerPrefs.GetInt("PlayerGold");
        gManager.costRespawnSav = PlayerPrefs.GetInt("CostRespawn");
        gManager.playerHeartsSav = PlayerPrefs.GetInt("PlayerHearts");
        gManager.playerTypeSubSav = PlayerPrefs.GetInt("PlayerTypeSub");
        gManager.playerMultiplierPowSav = PlayerPrefs.GetInt("PlayerMultiplierPow");
        gManager.playerTypeWhipSav = PlayerPrefs.GetInt("PlayerTypeWhip");
        //-------------------------------------------------------


        //--------------------------------------------------------
        //LLAVES (dropeos de Boss)
        //datos de llaves guardadas
        PlayerPrefs.SetInt("PlayerBKey", Convert.ToInt32(gManager.currentBKey));
        PlayerPrefs.SetInt("PlayerCKey", Convert.ToInt32(gManager.currentCKey));
        PlayerPrefs.SetInt("PlayerRKey", Convert.ToInt32(gManager.currentRKey));
        PlayerPrefs.SetInt("PlayerYKey", Convert.ToInt32(gManager.currentYKey));
        PlayerPrefs.SetInt("PlayerPKey", Convert.ToInt32(gManager.currentPKey));
        PlayerPrefs.SetInt("PlayerGKey", Convert.ToInt32(gManager.currentGKey));
        
        //datos de llaves pasados a variable pasa su posterior carga al hacer respawn
        gManager.BlueKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerBKey"));
        gManager.CianKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerCKey"));
        gManager.RedKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerRKey"));
        gManager.YellowKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerYKey"));
        gManager.PinkKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerPKey"));
        gManager.GreenKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerGKey"));
        //---------------------------------------------------------



        //------------------------------------------------------------
        //ITEMS (Lifemax o llaves en el mapa)
        //estado de objetos del mapa guardados para saber si hacen respawn
        PlayerPrefs.SetInt("LifeMax1", Convert.ToInt32(itemMapManager.lifeMax1Taken));
        PlayerPrefs.SetInt("LifeMax2", Convert.ToInt32(itemMapManager.lifeMax2Taken));

        //estado de objetos unicos del mapa pasado a variable para su posterior carga al hacer respawn
        itemMapManager.lifeMax1Sav = Convert.ToBoolean(PlayerPrefs.GetInt("LifeMax1"));
        itemMapManager.lifeMax2Sav = Convert.ToBoolean(PlayerPrefs.GetInt("LifeMax2"));

        //------------------------------------------------------------


        //------------------------------------------------------------
        //BOSS
        //estado de los boss para saber cuando deben respawnear
        PlayerPrefs.SetInt("BossBat", Convert.ToInt32(bossManager.bossBatDefeated));
        PlayerPrefs.SetInt("BossMedusa", Convert.ToInt32(bossManager.bossMedusaDefeated));

        //estado de boss del mapa pasado a variable para su posterior carga
        bossManager.bossBatSav = Convert.ToBoolean(PlayerPrefs.GetInt("BossBat"));
        bossManager.bossMedusaSav = Convert.ToBoolean(PlayerPrefs.GetInt("BossMedusa"));

        //------------------------------------------------------------


        //------------------------------------------------------------
        //EVENTOS
        //estado de los eventos para saber como cargarlos
        PlayerPrefs.SetInt("PitEvent", Convert.ToInt32(eventManager.pitActive));

        //estado de los eventos del mapa pasado a variable para su posterior carga
        eventManager.pitActiveSav = Convert.ToBoolean(PlayerPrefs.GetInt("PitEvent"));

        //------------------------------------------------------------


        //------------------------------------------------------------
        //BROKEN WALLS
        //estado de muros que se rompen en el mapa
        PlayerPrefs.SetInt("WallBroken1", Convert.ToInt32(structureManager.wallBroken1));
        PlayerPrefs.SetInt("WallBroken2", Convert.ToInt32(structureManager.wallBroken2));

        //estado de los muros pasado a variable para su posterior carga
        structureManager.wallBroken1Sav = Convert.ToBoolean(PlayerPrefs.GetInt("WallBroken1"));
        structureManager.wallBroken2Sav = Convert.ToBoolean(PlayerPrefs.GetInt("WallBroken2"));
        //------------------------------------------------------------


        //------------------------------------------------------------
        //MAP PARTS
        //estado del mapa
        PlayerPrefs.SetInt("PartMap0", Convert.ToInt32(structureManager.mapPart0));
        PlayerPrefs.SetInt("PartMap1", Convert.ToInt32(structureManager.mapPart1));
        PlayerPrefs.SetInt("PartMap2", Convert.ToInt32(structureManager.mapPart2));
        PlayerPrefs.SetInt("PartMap3", Convert.ToInt32(structureManager.mapPart3));
        PlayerPrefs.SetInt("PartMap4", Convert.ToInt32(structureManager.mapPart4));
        PlayerPrefs.SetInt("PartMap5", Convert.ToInt32(structureManager.mapPart5));
        PlayerPrefs.SetInt("PartMap6", Convert.ToInt32(structureManager.mapPart6));
        PlayerPrefs.SetInt("PartMap7", Convert.ToInt32(structureManager.mapPart7));
        PlayerPrefs.SetInt("PartMap8", Convert.ToInt32(structureManager.mapPart8));
        PlayerPrefs.SetInt("PartMap9", Convert.ToInt32(structureManager.mapPart9));
        PlayerPrefs.SetInt("PartMap10", Convert.ToInt32(structureManager.mapPart10));
        PlayerPrefs.SetInt("PartMap11", Convert.ToInt32(structureManager.mapPart11));
        PlayerPrefs.SetInt("PartMap12", Convert.ToInt32(structureManager.mapPart12));
        PlayerPrefs.SetInt("PartMap13", Convert.ToInt32(structureManager.mapPart13));
        PlayerPrefs.SetInt("PartMap14", Convert.ToInt32(structureManager.mapPart14));

        //estado de las partes del mapa pasado a variable para su posterior carga
        structureManager.mapPart0Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap0"));
        structureManager.mapPart1Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap1"));
        structureManager.mapPart2Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap2"));
        structureManager.mapPart3Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap3"));
        structureManager.mapPart4Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap4"));
        structureManager.mapPart5Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap5"));
        structureManager.mapPart6Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap6"));
        structureManager.mapPart7Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap7"));
        structureManager.mapPart8Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap8"));
        structureManager.mapPart9Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap9"));
        structureManager.mapPart10Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap10"));
        structureManager.mapPart11Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap11"));
        structureManager.mapPart12Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap12"));
        structureManager.mapPart13Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap13"));
        structureManager.mapPart14Sav = Convert.ToBoolean(PlayerPrefs.GetInt("PartMap14"));

        //---------------------------------------------------------------
    }
}
