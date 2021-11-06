using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public int EscenaActual;
    public bool startGame;
    public bool loadGame;
    

    [Header("POSICION INICIAL JUGADOR Y OBJETOS")]
    public GameObject playerPrefab;
    private Transform startPositionPlayer;
    GameObject instancePlayer;

    SimonController simonController;
    HealthPlayer playerHealth;
    HeartsSystem playerHearts;
    SubWeaponSystem weaponSystem;
    public bool haveSubW;
    TypeWhip whipMode;
    KeyChain keyChain;

    ItemMapManager itemMapManager;
    BossMapManager bossManager;
    EventManager eventManager;
    StructureManager structureManager;
    

    public Text healthText;
    public Text HeartsText;

    public Text weaponText;
    public Text goldText;
    public int gold;
    public int costRespawn = 1000;

    public GameObject panelPause;
    public bool GamePaused;
    

    [Header("COMPLEMENTOS")]
    public AudioClip SoundTouchHeart;
    public AudioClip soundGrabItem;
    public AudioClip soundGrabGold;
    public AudioClip soundGrabLifeMax;
    public AudioSource audioSource;

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

    [Header("SAVED DATA")]
    public int Saves;
    public float playerMaxHealthSav;
    public float playerPosXSav;
    public float playerPosYSav;
    public int playerGoldSav;
    public int costRespawnSav;
    public int playerHeartsSav;
    public int playerTypeSubSav;
    public int playerMultiplierPowSav;
    public bool playerHaveSub;
    public int playerTypeWhipSav;
    public bool BlueKeySav;
    public bool CianKeySav;
    public bool RedKeySav;
    public bool YellowKeySav;
    public bool PinkKeySav;
    public bool GreenKeySav;

    [Header("DATOS DEL JUGADOR")]    
    public float currentMaxHealth;
    public Vector2 posPlayer;
    public int currentHearts;
    public int currentTypeSub;
    public int currentMultiplierPow;
    public int currentTypeWhip;
    public bool currentBKey;
    public bool currentCKey;
    public bool currentRKey;
    public bool currentYKey;
    public bool currentPKey;
    public bool currentGKey;


    public static GameManager gameManager;

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
        if (level == 1 && startGame)
        {
            StartGame();
            startGameFadeOut.SetActive(true);
        }
        else if (level == 1 && loadGame)
        {
            LoadGame();
            startGameFadeOut.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemMapManager = GetComponentInChildren<ItemMapManager>();
        bossManager = GetComponentInChildren<BossMapManager>();
        eventManager = GetComponentInChildren<EventManager>();
        structureManager = GetComponentInChildren<StructureManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
        EscenaActual = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BGUIPlayer.SetActive(true);
            

            //esta linea es solo para encerrar el valor del oro en 0 y 999999999 para que no exceda ese limite
            gold = Mathf.Clamp(gold, 0, 999999999);

            healthText.text = playerHealth.currentHealth.ToString("F0");
            HeartsText.text = playerHearts.currentHearts.ToString("F0");
            CurrentDataPlayer();
            InputKeysMenu();
            Paused();
            WeaponCheck();
            ScorePoints();
            KeyCheck();
        }
        else if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            BGUIPlayer.SetActive(false);
        }
    }
    
    
    public void BackMainMenuButton()
    {
        gameOverFadeIn.SetActive(true);
        gameOverScreen.SetActive(false);
        StartCoroutine(ChangeMainMenu());
    }

    public void QuitButton()
    {

        SceneManager.LoadScene(0);
        panelPause.SetActive(false);
        startGame = false;
        GamePaused = false;
    }

    IEnumerator ChangeMainMenu()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(0);
    }

    void InputKeysMenu()
    {
        if (Input.GetButtonDown("Start") && playerHealth.currentHealth > 0)
        {
            if (panelPause.activeSelf == false)
            {
                panelPause.SetActive(true);
                GamePaused = true;
            }
            else
            {
                panelPause.SetActive(false);
                GamePaused = false;
            }
        }
    }

    void WeaponCheck()
    {
        if(whipMode.typeWhip == 0)
        {
            weaponText.text = "Leather Whip";
        }
        else if(whipMode.typeWhip == 1)
        {
            weaponText.text = "Chain Whip";
        }
        else if (whipMode.typeWhip == 2)
        {
            weaponText.text = "Vampire Killer";
        }
        //imagen del banner de estado
        if(weaponSystem.typeSub == 0)
        {
            imgSub.sprite = SubSprKnife;
            imgSub.enabled = true;
        }            
        else if(weaponSystem.typeSub == 1)
        {
            imgSub.sprite = SubSprAxe;
            imgSub.enabled = true;
        }            
        else if (weaponSystem.typeSub == 2)
        {
            imgSub.sprite = SubSprHolyWater;
            imgSub.enabled = true;
        }
        else if (weaponSystem.typeSub == 3)
        {
            imgSub.sprite = SubSprCross;
            imgSub.enabled = true;
        }
        else
        {
            imgSub.sprite = null;
            imgSub.enabled = false;
        }

        if(weaponSystem.multiplierPow == 0)
        {
            imgPow.sprite = null;
            imgPow.enabled = false;
        }
        else if(weaponSystem.multiplierPow == 1)
        {
            imgPow.sprite = x2Pow;
            imgPow.enabled = true;
        }
        else if (weaponSystem.multiplierPow == 2)
        {
            imgPow.sprite = x3Pow;
            imgPow.enabled = true;
        }
    }

    void KeyCheck()
    {
        if (keyChain.blueKey)
        {
            imgBlueKey.SetActive(true);
            imgBlueKey.GetComponent<Image>().sprite = blueKeySpr;
        }

         if (keyChain.cianKey)
        {
            imgCianKey.SetActive(true);
            imgCianKey.GetComponent<Image>().sprite = cianKeySpr;
        }

        if (keyChain.redKey)
        {
            imgRedKey.SetActive(true);
            imgRedKey.GetComponent<Image>().sprite = redKeySpr;
        }

        if (keyChain.yellowKey)
        {
            imgYellowKey.SetActive(true);
            imgYellowKey.GetComponent<Image>().sprite = yellowKeySpr;
        }

        if (keyChain.pinkKey)
        {
            imgPinkKey.SetActive(true);
            imgPinkKey.GetComponent<Image>().sprite = pinkKeySpr;
        }

        if (keyChain.greenKey)
        {
            imgGreenKey.SetActive(true);
            imgGreenKey.GetComponent<Image>().sprite = greenKeySpr;
        }
    }

    void ScorePoints()
    {
        goldText.text = gold.ToString();
    }

    void Paused()
    {
        if (GamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void CurrentDataPlayer()
    {
        currentMaxHealth = playerHealth.maxHealth;
        posPlayer = instancePlayer.transform.position;
        currentHearts = playerHearts.currentHearts;
        currentTypeSub = weaponSystem.typeSub;
        currentMultiplierPow = weaponSystem.multiplierPow;
        currentTypeWhip = whipMode.typeWhip;
        currentBKey = keyChain.blueKey;
        currentCKey = keyChain.cianKey;
        currentRKey = keyChain.redKey;
        currentYKey = keyChain.yellowKey;
        currentPKey = keyChain.pinkKey;
        currentGKey = keyChain.greenKey;
        haveSubW = weaponSystem.haveSub;
    }

    public void BlinkControl()
    {
        StartCoroutine(TimeBlink());
    }

    IEnumerator TimeBlink()
    {
        yield return new WaitForSeconds(3);
        if(Saves != 0 && gold >= costRespawn)
        {
            blink.SetActive(true);
        }
        else
        {
            //lanzar pantalla de gameover para ir al menu inicial
            gameOverScreen.SetActive(true);
        }
    }
    

    public void LoadGame()
    {
        //Carga de datos temporales
        Saves = PlayerPrefs.GetInt("Saves");
        playerMaxHealthSav = PlayerPrefs.GetFloat("PlayerMaxHealth");
        playerPosXSav = PlayerPrefs.GetFloat("PlayerPosX");
        playerPosYSav = PlayerPrefs.GetFloat("PlayerPosY");
        playerGoldSav = PlayerPrefs.GetInt("PlayerGold");
        costRespawnSav = PlayerPrefs.GetInt("CostRespawn");
        playerHeartsSav = PlayerPrefs.GetInt("PlayerHearts");

        if (!instancePlayer)
        {
            instancePlayer = Instantiate(playerPrefab, new Vector3(playerPosXSav, playerPosYSav, 0), Quaternion.identity);
            instancePlayer.name = playerPrefab.name;

            CargarComponentesInicio();
        }

        playerTypeSubSav = weaponSystem.typeSub = PlayerPrefs.GetInt("PlayerTypeSub");
        playerMultiplierPowSav = weaponSystem.multiplierPow = PlayerPrefs.GetInt("PlayerMultiplierPow");
        playerHaveSub = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerHaveSub"));
        playerTypeWhipSav = whipMode.typeWhip = PlayerPrefs.GetInt("PlayerTypeWhip");

        itemMapManager.ItemsMapOnLoadGame();
        bossManager.BossMapOnLoadGame();
        eventManager.EventOnLoadGame();
        structureManager.BrokenWallsOnLoadGame();
        structureManager.MapPartsLoadGame();

        //carga de datos de llaves temporales
        BlueKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerBKey"));
        CianKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerCKey"));
        RedKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerRKey"));
        YellowKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerYKey"));
        PinkKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerPKey"));
        GreenKeySav = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerGKey"));

        //Carga STATS BASE (vida maxima, posicion, oro, corazones, subArma, tipo de latigo)
        playerHealth.maxHealth = PlayerPrefs.GetFloat("PlayerMaxHealth");
        playerHealth.currentHealth = PlayerPrefs.GetFloat("PlayerMaxHealth");
        simonController.canMove = true;
        gold = PlayerPrefs.GetInt("PlayerGold");
        costRespawn = PlayerPrefs.GetInt("CostRespawn");
        playerHearts.currentHearts = PlayerPrefs.GetInt("PlayerHearts");
        weaponSystem.typeSub = PlayerPrefs.GetInt("PlayerTypeSub");
        weaponSystem.haveSub = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerHaveSub"));
        whipMode.typeWhip = PlayerPrefs.GetInt("PlayerTypeWhip");

        //carga estado de llaves
        keyChain.blueKey = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerBKey"));
        keyChain.cianKey = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerCKey"));
        keyChain.redKey = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerRKey"));
        keyChain.yellowKey = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerYKey"));
        keyChain.pinkKey = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerPKey"));
        keyChain.greenKey = Convert.ToBoolean(PlayerPrefs.GetInt("PlayerGKey"));
    }

    
    public void StartGame()
    {
        startPositionPlayer = GameObject.Find("StartSpawn").GetComponent<Transform>();

        if (!instancePlayer)
        {
            instancePlayer = Instantiate(playerPrefab, startPositionPlayer.position, Quaternion.identity);
            instancePlayer.name = playerPrefab.name;

            CargarComponentesInicio();
            
            simonController.canMove = true;
            costRespawn = 1000;
            playerHealth.maxHealth = 20;
            playerHealth.currentHealth = playerHealth.maxHealth;
            playerHearts.currentHearts = 6;
            weaponSystem.typeSub = 4;
            weaponSystem.multiplierPow = 0;
            weaponSystem.haveSub = false;
            gold = 0;
            keyChain.blueKey = false;
            keyChain.cianKey = false;
            keyChain.redKey = false;
            keyChain.yellowKey = false;
            keyChain.pinkKey = false;
            keyChain.greenKey = false;
        }

        //items unicos del mapa restablecidos por defecto
        itemMapManager.ItemsMapOnStartGame();
        bossManager.BossMapOnStartGame();
        eventManager.EventOnStartGame();
        structureManager.BrokenWallsOnStartGame();
        structureManager.MapPartsStartGame();
    }

    void CargarComponentesInicio()
    {
        /*********como si fuera el start de C# *********/
        instancePlayer = GameObject.FindGameObjectWithTag("Player");
        simonController = instancePlayer.GetComponent<SimonController>();
        playerHealth = instancePlayer.GetComponent<HealthPlayer>();
        playerHearts = instancePlayer.GetComponent<HeartsSystem>();
        weaponSystem = instancePlayer.GetComponent<SubWeaponSystem>();

        whipMode = instancePlayer.GetComponentInChildren<TypeWhip>();
        keyChain = instancePlayer.GetComponentInChildren<KeyChain>();
        /************************/
    }
}
