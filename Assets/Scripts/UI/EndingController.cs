using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    public Image castleImg;
    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posX, timer, xSen;

    /// </summary>

    public AudioSource castleSound;
    public AudioSource musicEnd;

    //
    public GameObject scoreObj;

    public Text numSavesTxt;
    public Text numGoldTxt;
    public Text numTimeTxt;

    // Start is called before the first frame update
    void Start()
    {
        posX = castleImg.transform.localPosition.x;
        StartCoroutine(StopDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        CastleDestroy();
    }

    void CastleDestroy()
    {
        //movimiento senoidal Castillo destruyendose
        timer = timer + (frecuency / 100);
        xSen = Mathf.Sin(timer);
        castleImg.transform.localPosition = new Vector3(posX + (xSen * cycleWidth), castleImg.transform.localPosition.y, transform.position.z);
    }

    IEnumerator StopDestroy()
    {
        yield return new WaitForSeconds(8);
        castleSound.Pause();
        musicEnd.Play();
        yield return new WaitForSeconds(52f);
        scoreObj.SetActive(true);
        FixValuesPlayer();
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(0);
    }

    void FixValuesPlayer()
    {
        numSavesTxt.text = PlayerPrefs.GetInt("Saves").ToString();
        numGoldTxt.text = PlayerPrefs.GetInt("GoldPlayer").ToString();
        numTimeTxt.text = PlayerPrefs.GetFloat("TimePlayed").ToString();
    }

    //pendiente agregar los nombres de los creditos
    //pendiente poner una transicion mas suave de el nivel de juego a el ending
    //poner un sistema de rango al finalizar el juego que dependa del tiempo y el numero de saves, tal vez del gold

}
