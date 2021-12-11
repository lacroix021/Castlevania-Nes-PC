using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public Button buttonStartGame;
    public GameObject buttonLoadGame;
    public GameObject buttonClearGame;
    public Text percentMap;

    GameManager gManager;
    DatosJugador datosJugador;

    public GameObject helpPanel;
    public Button buttonBackHelpPanel;

    // Start is called before the first frame update
    void Start()
    {
        buttonStartGame.Select();
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

    IEnumerator TimerChargeMap()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
    public void StartGameButton()
    {
        gManager.startGame = true;
        gManager.loadGame = false;
        StartCoroutine(TimerChargeMap());
    }

    public void LoadGameButton()
    {
        gManager.startGame = false;
        gManager.loadGame = true;
        StartCoroutine(TimerChargeMap());
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        datosJugador.Saves = 0;
        buttonStartGame.Select();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void HelpButton()
    {
        helpPanel.SetActive(true);
        buttonBackHelpPanel.Select();
    }

    public void BackButtonHelpPannel()
    {
        helpPanel.SetActive(false);
        buttonStartGame.Select();
    }

    public void ButtonYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UC399HFBX_kKapEWh6jQ8GaA");
    }
    public void ButtonFacebook()
    {
        Application.OpenURL("https://www.facebook.com/RamshyCorp");
    }
    public void ButtonPaypal()
    {
        Application.OpenURL("https://www.paypal.com/paypalme/lacroixiscariot");
    }
}
