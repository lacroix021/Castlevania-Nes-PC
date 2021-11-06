using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public GameObject buttonLoadGame;
    public GameObject buttonClearGame;
    
    GameManager gManager;

   

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.gameManager;

    }


    private void Update()
    {
        

        if (PlayerPrefs.GetInt("Saves") == 0)
        {
            buttonLoadGame.SetActive(false);
            buttonClearGame.SetActive(false);
        }
        else
        {
            buttonLoadGame.SetActive(true);
            buttonClearGame.SetActive(true);
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
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
