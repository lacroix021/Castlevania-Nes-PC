using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public GameObject buttonLoadGame;
    public GameObject buttonClearGame;
    public Text percentMap;

    GameManager gManager;
    DatosJugador datosJugador;
   

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.gameManager;
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        datosJugador.Saves = PlayerPrefs.GetInt("Saves");
    }


    private void Update()
    {
        if (datosJugador.Saves == 0)
        {
            buttonLoadGame.SetActive(false);
            buttonClearGame.SetActive(false);
        }
        else
        {
            buttonLoadGame.SetActive(true);
            buttonClearGame.SetActive(true);
            percentMap.text = PlayerPrefs.GetFloat("MapPercent").ToString("F0") + "%";
        }
    }
    public void StartGameButton()
    {
        gManager.startGame = true;
        gManager.loadGame = false;
        SceneManager.LoadScene(1);
    }

    public void LoadGameButton()
    {
        gManager.startGame = false;
        gManager.loadGame = true;
        SceneManager.LoadScene(1);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        datosJugador.Saves = 0;
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
