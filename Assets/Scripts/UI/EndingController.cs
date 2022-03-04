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
    public Text secondsTimeTxt;
    public Text minutesTimeTxt;
    public Text hoursTimeTxt;

    //
    public Animator blackscreenAnim;

    //Credits
    [Header("CREDITS")]
    public GameObject names;
    public string[] credits;
    public int num;
    float nextChangeName;
    public float changeNameRate;

    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        posX = castleImg.transform.localPosition.x;
        StartCoroutine(StopDestroy());
        
    }

    // Update is called once per frame
    void Update()
    {
        CastleDestroy();
        Credits();
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
        blackscreenAnim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    void FixValuesPlayer()
    {
        numSavesTxt.text = PlayerPrefs.GetInt("Saves").ToString();
        numGoldTxt.text = PlayerPrefs.GetInt("GoldPlayer").ToString();
        secondsTimeTxt.text = PlayerPrefs.GetFloat("Seconds")<10? "0" + PlayerPrefs.GetFloat("Seconds").ToString("F0"): PlayerPrefs.GetFloat("Seconds").ToString("F0");
        minutesTimeTxt.text = PlayerPrefs.GetFloat("Minutes")<10? "0" + PlayerPrefs.GetFloat("Minutes").ToString("F0"): PlayerPrefs.GetFloat("Minutes").ToString("F0");
        hoursTimeTxt.text = PlayerPrefs.GetFloat("Hours")<10? "0" + PlayerPrefs.GetFloat("Hours").ToString("F0"): PlayerPrefs.GetFloat("Hours").ToString("F0");
    }

    //pendiente agregar los nombres de los creditos
    //pendiente poner una transicion mas suave de el nivel de juego a el ending
    //poner un sistema de rango al finalizar el juego que dependa del tiempo y el numero de saves, tal vez del gold


    void Credits()
    {
        if(Time.time >= nextChangeName)
        {
            switch (num)
            {
                case 0:
                    names.GetComponent<Text>().text = credits[0];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 1:
                    names.GetComponent<Text>().text = credits[1];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 2:
                    names.GetComponent<Text>().text = credits[2];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 3:
                    names.GetComponent<Text>().text = credits[3];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 4:
                    names.GetComponent<Text>().text = credits[4];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 5:
                    names.GetComponent<Text>().text = credits[5];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 6:
                    names.GetComponent<Text>().text = credits[6];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 7:
                    names.GetComponent<Text>().text = credits[7];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 8:
                    names.GetComponent<Text>().text = credits[8];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 9:
                    names.GetComponent<Text>().text = credits[9];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 10:
                    names.GetComponent<Text>().text = credits[10];
                    num += 1;
                    StartCoroutine(Fades());
                    break;
                case 11:
                    names.SetActive(false);
                    break;
            }

            

            nextChangeName = Time.time + 1 / changeNameRate;
        }

    }

    IEnumerator Fades()
    {
        names.SetActive(true);
        yield return new WaitForSeconds(5f);
        names.SetActive(false);
    }
}
