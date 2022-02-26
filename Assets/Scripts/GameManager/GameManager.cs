﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public CursorLockMode cursorState;
    public bool debugMode;
    

    public int EscenaActual;
    public bool startGame;
    public bool loadGame;
    

    [Header("POSICION INICIAL JUGADOR Y OBJETOS")]
    //public GameObject playerPrefab;
    private Transform startPositionPlayer;
    public GameObject instancePlayer;

    SimonController simonController;
    HealthPlayer playerHealth;

    ItemMapManager itemMapManager;
    BossMapManager bossManager;
    EventManager eventManager;
    StructureManager structureManager;

    /**/
    GuardarCargar guardarCargar;
    DatosJugador datosJugador;
    /**/


    public Text healthText;
    public Text HeartsText;

    public Text weaponText;
    public Text goldText;
    

    public GameObject panelPause;
    public bool GamePaused;

    [Header("SPRITES COMPLEMENTO")]
    public Image imgSub;
    public Sprite SubSprKnife;
    public Sprite SubSprAxe;
    public Sprite SubSprHolyWater;
    public Sprite SubSprCross;

    public Image imgPow;
    public Sprite x2Pow, x3Pow;

    public GameObject imgBlueKey;
    public GameObject imgCianKey;
    public GameObject imgRedKey;
    public GameObject imgYellowKey;
    public GameObject imgPinkKey;
    public GameObject imgGreenKey;

    public Sprite blueKeySpr;
    public Sprite cianKeySpr;
    public Sprite redKeySpr;
    public Sprite yellowKeySpr;
    public Sprite pinkKeySpr;
    public Sprite greenKeySpr;

    [Header("OBJETOS DE UI")]
    public GameObject BGUIPlayer;
    public GameObject barDoorMessaje;
    public Text doorMessajeTxt;
    public GameObject blink;
    public GameObject gameOverScreen;
    public GameObject startGameFadeOut;
    public GameObject gameOverFadeIn;

    public static GameManager gameManager;
    
    public Button buttonQuit;
    public Button buttonNav;
    public Button buttonBackMenu;

    public bool navigationMode;


    public ActivateMusic[] musicMap;

    private void Awake()
    {
        if(GameManager.gameManager == null)
        {
            GameManager.gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2 && startGame)
        {
            StartGame();
            startGameFadeOut.SetActive(true);
        }
        else if (level == 2 && loadGame)
        {
            LoadGame();
            startGameFadeOut.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = cursorState;
        itemMapManager = GetComponentInChildren<ItemMapManager>();
        bossManager = GetComponentInChildren<BossMapManager>();
        eventManager = GetComponentInChildren<EventManager>();
        structureManager = GetComponentInChildren<StructureManager>();
        guardarCargar = GetComponent<GuardarCargar>();
        datosJugador = GetComponent<DatosJugador>();
    }

    public void PauseGame()
    {
        if (playerHealth.currentHealth > 0)
        {
            if (panelPause.activeSelf == false)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.pause);
                AudioManager.instance.masterVol = -20;
                AudioManager.instance.effectsVol = -20;
                Time.timeScale = 0;
                panelPause.SetActive(true);
                GamePaused = true;
                /////////////////////////////////////////////////////////
                buttonNav.Select();

                GameObject[] audios = GameObject.FindGameObjectsWithTag("MusicBox");
                foreach (GameObject a in audios)
                {
                    a.GetComponent<AudioSource>().Pause();
                }
            }
            else
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.pause);
                AudioManager.instance.masterVol = 0;
                AudioManager.instance.effectsVol = -2;
                Time.timeScale = 1;
                panelPause.SetActive(false);
                GamePaused = false;
                navigationMode = false;
                buttonQuit.enabled = true;

                GameObject[] audios = GameObject.FindGameObjectsWithTag("MusicBox");
                foreach (GameObject a in audios)
                {
                    a.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;

        
        EscenaActual = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            

            BGUIPlayer.SetActive(true);

            //esta linea es solo para encerrar el valor del oro en 0 y 999999999 para que no exceda ese limite
            datosJugador.gold = Mathf.Clamp(datosJugador.gold, 0, 999999999);

            healthText.text = playerHealth.currentHealth.ToString("F0");
            HeartsText.text = datosJugador.currentHearts.ToString("F0");
            //CurrentDataPlayer();
            WeaponCheck();
            ScorePoints();
            KeyCheck();
        }
        else if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            BGUIPlayer.SetActive(false);
        }
    }
    
    
    public void BackMainMenuButton()
    {
        AudioManager.instance.masterVol = 0;
        AudioManager.instance.effectsVol = -2;
        gameOverFadeIn.SetActive(true);
        gameOverScreen.SetActive(false);
        StartCoroutine(ChangeMainMenu());
    }

    public void QuitButton()
    {
        Time.timeScale = 1;
        AudioManager.instance.masterVol = 0;
        AudioManager.instance.effectsVol = -2;
        GamePaused = false;
        panelPause.SetActive(false);
        SceneManager.LoadScene(0);
        startGame = false;
    }

    public void Navigation()
    {
        if (!navigationMode)
        {
            navigationMode = true;
            buttonQuit.enabled = false;
        }
        else
        {
            navigationMode = false;
            buttonQuit.enabled = true;
        }
    }

    IEnumerator ChangeMainMenu()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(0);
    }
    

    void WeaponCheck()
    {
        if(datosJugador.typeWhip == 0)
        {
            weaponText.text = "Leather Whip";
        }
        else if(datosJugador.typeWhip == 1)
        {
            weaponText.text = "Chain Whip";
        }
        else if (datosJugador.typeWhip == 2)
        {
            weaponText.text = "Vampire Killer";
        }
        //imagen del banner de estado
        if(datosJugador.typeSub == 0)
        {
            imgSub.sprite = SubSprKnife;
            imgSub.enabled = true;
        }            
        else if(datosJugador.typeSub == 1)
        {
            imgSub.sprite = SubSprAxe;
            imgSub.enabled = true;
        }            
        else if (datosJugador.typeSub == 2)
        {
            imgSub.sprite = SubSprHolyWater;
            imgSub.enabled = true;
        }
        else if (datosJugador.typeSub == 3)
        {
            imgSub.sprite = SubSprCross;
            imgSub.enabled = true;
        }
        else
        {
            imgSub.sprite = null;
            imgSub.enabled = false;
        }
        //imagen del multiplicador
        if(datosJugador.multiplierPow == 0)
        {
            imgPow.sprite = null;
            imgPow.enabled = false;
        }
        else if(datosJugador.multiplierPow == 1)
        {
            imgPow.sprite = x2Pow;
            imgPow.enabled = true;
        }
        else if (datosJugador.multiplierPow == 2)
        {
            imgPow.sprite = x3Pow;
            imgPow.enabled = true;
        }
    }

    void KeyCheck()
    {
        if (datosJugador.blueKey)
        {
            imgBlueKey.SetActive(true);
            imgBlueKey.GetComponent<Image>().sprite = blueKeySpr;
        }
        else
        {
            imgBlueKey.SetActive(false);
            imgBlueKey.GetComponent<Image>().sprite = null;
        }

         if (datosJugador.cianKey)
        {
            imgCianKey.SetActive(true);
            imgCianKey.GetComponent<Image>().sprite = cianKeySpr;
        }
        else
        {
            imgCianKey.SetActive(false);
            imgCianKey.GetComponent<Image>().sprite = null;
        }

        if (datosJugador.redKey)
        {
            imgRedKey.SetActive(true);
            imgRedKey.GetComponent<Image>().sprite = redKeySpr;
        }
        else
        {
            imgRedKey.SetActive(false);
            imgRedKey.GetComponent<Image>().sprite = null;
        }

        if (datosJugador.yellowKey)
        {
            imgYellowKey.SetActive(true);
            imgYellowKey.GetComponent<Image>().sprite = yellowKeySpr;
        }
        else
        {
            imgYellowKey.SetActive(false);
            imgYellowKey.GetComponent<Image>().sprite = null;
        }

        if (datosJugador.pinkKey)
        {
            imgPinkKey.SetActive(true);
            imgPinkKey.GetComponent<Image>().sprite = pinkKeySpr;
        }
        else
        {
            imgPinkKey.SetActive(false);
            imgPinkKey.GetComponent<Image>().sprite = null;
        }

        if (datosJugador.greenKey)
        {
            imgGreenKey.SetActive(true);
            imgGreenKey.GetComponent<Image>().sprite = greenKeySpr;
        }
        else
        {
            imgGreenKey.SetActive(false);
            imgGreenKey.GetComponent<Image>().sprite = null;
        }
    }
    
    void ScorePoints()
    {
        goldText.text = datosJugador.gold.ToString();
    }

    /*
    void CurrentDataPlayer()
    {
        datosJugador.posPlayer = instancePlayer.transform.position;
    }
    */

    public void BlinkControl()
    {
        StartCoroutine(TimeBlink());
    }

    IEnumerator TimeBlink()
    {
        yield return new WaitForSeconds(3);

        if(datosJugador.Saves != 0 && datosJugador.gold >= datosJugador.costRespawn)
        {
            blink.SetActive(true);
            RespawnMusic();
        }
        else
        {
            //lanzar pantalla de gameover para ir al menu inicial
            gameOverScreen.SetActive(true);
            buttonBackMenu.Select();
        }
    }
    

    public void LoadGame()
    {
        guardarCargar.CargarInformacion();

        if (!instancePlayer)
        {
            //instancePlayer = Instantiate(playerPrefab, datosJugador.posPlayer, Quaternion.identity);
            //instancePlayer.name = playerPrefab.name;
            Vector2 loadPos = new Vector2(PlayerPrefs.GetFloat("LastPositionX"), PlayerPrefs.GetFloat("LastPositionY"));
            instancePlayer = GameObject.FindGameObjectWithTag("Player");
            instancePlayer.transform.position = loadPos;
            instancePlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            CargarComponentesInicio();
        }

        itemMapManager.ItemsMapOnLoadGame();
        bossManager.BossMapOnLoadGame();
        eventManager.EventOnLoadGame();
        structureManager.BrokenWallsOnLoadGame();
        structureManager.MapPartsLoadGame();
        
        playerHealth.currentHealth = datosJugador.maxHealth;
        simonController.canMove = true;
    }

    
    public void StartGame()
    {
        startPositionPlayer = GameObject.Find("StartSpawn").GetComponent<Transform>();

        if (!instancePlayer)
        {
            //instancePlayer = Instantiate(playerPrefab, startPositionPlayer.position, Quaternion.identity);
            //instancePlayer.name = playerPrefab.name;
            instancePlayer = GameObject.FindGameObjectWithTag("Player");
            instancePlayer.transform.position = startPositionPlayer.position;
            instancePlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            CargarComponentesInicio();
            
            simonController.canMove = true;
            datosJugador.costRespawn = 600;
            datosJugador.maxHealth = 20;
            playerHealth.currentHealth = datosJugador.maxHealth;
            datosJugador.currentHearts = 6;
            datosJugador.typeSub = 4;
            datosJugador.typeWhip = 0;
            datosJugador.multiplierPow = 0;
            datosJugador.haveSub = false;
            datosJugador.gold = 0;
            datosJugador.Saves = 0;
        }

        //items unicos del mapa restablecidos por defecto
        datosJugador.blueKey = false;
        datosJugador.cianKey = false;
        datosJugador.redKey = false;
        datosJugador.pinkKey = false;
        datosJugador.yellowKey = false;
        datosJugador.greenKey = false;

        itemMapManager.ItemsMapOnStartGame();
        bossManager.BossMapOnStartGame();
        eventManager.EventOnStartGame();
        structureManager.BrokenWallsOnStartGame();
        structureManager.MapPartsStartGame();
    }

    void CargarComponentesInicio()
    {
        /*********como si fuera el start de C# *********/
        //instancePlayer = GameObject.FindGameObjectWithTag("Player");
        simonController = instancePlayer.GetComponent<SimonController>();
        playerHealth = instancePlayer.GetComponent<HealthPlayer>();
        
        /************************/
    }

    public void RespawnMusic()
    {
        //restablecer musica a la normalidad al respawnear
        musicMap = GameObject.FindObjectsOfType<ActivateMusic>();

        for (int i = 0; i < musicMap.Length; i++)
        {
            musicMap[i].GetComponent<ActivateMusic>().battle = false;

            if (musicMap[i].GetComponentInChildren<ChangeSong>())
            {
                musicMap[i].GetComponentInChildren<ChangeSong>().OnRespawnMusic();
            }
        }
    }
}
