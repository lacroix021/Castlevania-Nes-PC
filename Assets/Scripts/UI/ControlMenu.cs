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

    public GameObject DeletePanel;
    public Button noButton;

    public GameObject gamePadInput;
    public Button pcInputButton;

    public GameObject pcInput;
    public Button gamePadButton;
    

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

    IEnumerator TimerChargeMap2()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
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
        StartCoroutine(TimerChargeMap2());
    }

    public void ClearData()
    {
        DeletePanel.SetActive(true);
        noButton.Select();
    }

    public void YesButton()
    {
        PlayerPrefs.DeleteAll();
        datosJugador.Saves = 0;
        DeletePanel.SetActive(false);
        buttonStartGame.Select();
    }

    public void NoButton()
    {
        DeletePanel.SetActive(false);
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

    public void InputButton()
    {
        gamePadInput.SetActive(true);
        pcInput.SetActive(false);
        pcInputButton.Select();
    }

    public void PCInputButton()
    {
        pcInput.SetActive(true);
        gamePadInput.SetActive(false);
        gamePadButton.Select();
    }
    public void BackButtonInputPages()
    {
        pcInput.SetActive(false);
        gamePadInput.SetActive(false);
        buttonStartGame.Select();
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
    public void ButtonBetaTesters()
    {
        Application.OpenURL("https://www.facebook.com/groups/betatestersramshycorp");
    }
    public void ButtonPaypal()
    {
        Application.OpenURL("https://paypal.me/RamshyCorp");
    }
    
    public void ButtonPatreon()
    {
        Application.OpenURL("https://www.patreon.com/ramshycorp");
    }
    
}
