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

    //
    string credit0 = "Directed by \n Ramshy";
    string credit1 = "Screenplay by \n Vram Stoker \n Music by \n James banana \nYoutube - MikaeruRashon \nYoutube - Wyatt";
    string credit2 = "The Cast \n Dracula, Doppelganger \nRamshy - Christopher Bee\nDeath - Belo Lugosi \n Frankenstein - Boris Karloffice";
    string credit3 = "Mummyman - Love Chaney Jr \n Medusa - Barber Sherry \nLegion - Ramshy & SmithyGCN";
    string credit4 = "Vampire Bat - Mix Schrecks \n HunchBack - Love Chaney \nFishMan - Green Stranger \n Armor - Cafebar Read";
    string credit5 = "Skeleton - Andre Moral \n Zombie - Jone Candies";
    string credit6 = "Thanks for the support \nGerman Bautista \nDennis Castlesampa \nFrancisco Dworaczuk \nFreddy Ponce";
    string credit7 = "Thanks for the support \nChristian Paris \nGuerrero Martinez \nComunity CastlevaniaLatino";
    string credit8 = "Thanks for the support \nMy Wife & my Son \nI love you for eternity";
    string credit9 = "And the hero \n Simon Belmondo";
    string credit10 = "You played the greatest role in this story.";
    string credit11 = "Thank you \n for playing.";
    //

    int num;

    public float timing;
    public float timeChange;

    public Image flearider;

    public float newScaleX;
    public float newScaleY;
    public float newPosX;
    public float newPosY;
    public float multiplier;
    public float timeFlea;

    // Start is called before the first frame update
    void Start()
    {
        timeFlea = 0;
        timing = 0;
        num = 0;
        posX = castleImg.transform.localPosition.x;
        StartCoroutine(StopDestroy());

        

        credits[0] = credit0;
        credits[1] = credit1;
        credits[2] = credit2;
        credits[3] = credit3;
        credits[4] = credit4;
        credits[5] = credit5;
        credits[6] = credit6;
        credits[7] = credit7;
        credits[8] = credit8;
        credits[9] = credit9;
        credits[10] = credit10;
        credits[11] = credit11;

    }
    //corregir creditos de forma optima, e incluir a algunos subscriptores;

    // Update is called once per frame
    void Update()
    {
        CastleDestroy();
        Credits();

        TrayectoryFleaRider();
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
        yield return new WaitForSeconds(musicEnd.clip.length);
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
        minutesTimeTxt.text = PlayerPrefs.GetFloat("Minutes")<10? "0" + PlayerPrefs.GetFloat("Minutes").ToString("F0") + " :" : PlayerPrefs.GetFloat("Minutes").ToString("F0") + " :";
        hoursTimeTxt.text = PlayerPrefs.GetFloat("Hours")<10? "0" + PlayerPrefs.GetFloat("Hours").ToString("F0") + " :" : PlayerPrefs.GetFloat("Hours").ToString("F0") + " :";
    }

    //pendiente agregar los nombres de los creditos
    //pendiente poner una transicion mas suave de el nivel de juego a el ending
    //poner un sistema de rango al finalizar el juego que dependa del tiempo y el numero de saves, tal vez del gold


    void Credits()
    {
        timing += Time.deltaTime * 1;

        if(timing >= timeChange)
        {
            num++;
            timing = 0;
        }

        switch (num)
        {
            case 0:
                names.GetComponent<Text>().text = credits[0];
                break;
            case 1:
                names.GetComponent<Text>().text = credits[1];
                break;
            case 2:
                names.GetComponent<Text>().text = credits[2];
                break;
            case 3:
                names.GetComponent<Text>().text = credits[3];
                break;
            case 4:
                names.GetComponent<Text>().text = credits[4];
                break;
            case 5:
                names.GetComponent<Text>().text = credits[5];
                break;
            case 6:
                names.GetComponent<Text>().text = credits[6];
                break;
            case 7:
                names.GetComponent<Text>().text = credits[7];
                break;
            case 8:
                names.GetComponent<Text>().text = credits[8];
                break;
            case 9:
                names.GetComponent<Text>().text = credits[9];
                break;
            case 10:
                names.GetComponent<Text>().text = credits[10];
                break;
            case 11:
                names.GetComponent<Text>().text = credits[11];
                break;
            case 12:
                names.SetActive(false);
                break;
        }

    }

    IEnumerator Fades()
    {
        names.SetActive(true);
        yield return new WaitForSeconds(timeChange);
        names.SetActive(false);
    }

    void TrayectoryFleaRider()
    {
        newScaleX += Time.deltaTime * multiplier;
        newScaleY += Time.deltaTime * multiplier;
        flearider.transform.localScale = new Vector3(transform.localScale.x + newScaleX, transform.localScale.y + newScaleY, transform.localScale.z);

        if(timeFlea < 7)
        {
            flearider.transform.localPosition = new Vector3(flearider.transform.localPosition.x - 0.5f, flearider.transform.localPosition.y - 0.3f, 0);
        }

        timeFlea += Time.deltaTime * 1;

        if(timeFlea >= 7 && timeFlea <= 24)
        {
            multiplier = 1.3f;
            flearider.transform.eulerAngles = new Vector3(0, 180, 0);
            flearider.transform.localPosition = new Vector3(flearider.transform.localPosition.x + 0.25f, flearider.transform.localPosition.y + 0.2f, 0);
        }

        if(timeFlea >= 10 && timeFlea <= 24)
        {
            flearider.transform.localPosition = new Vector3(flearider.transform.localPosition.x + 0.5f, flearider.transform.localPosition.y + 0.2f, 0);
        }

        if(timeFlea >= 25)
        {
            flearider.enabled = false;
            multiplier = 0;
        }
    }
}
