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
    string credit1 = "Screenplay by \n Vram Stoker \n music by \n James banana";
    string credit2 = "The Cast \n Dracula \n Ramshy";
    string credit3 = "Death \n Belo Lugosi \n Frankenstein \n Boris Karloffice";
    string credit4 = "Mummyman \n Love Chaney Jr \n Medusa \n Barber Sherry";
    string credit5 = "Doppelganger, Legion \n Ramshy & SmithyGCN";
    string credit6 = "Vampire Bat \n Mix Schrecks \n HunchBack \n Love Chaney";
    string credit7 = "FishMan \n Green Stranger \n Armor \n Cafebar Read";
    string credit8 = "Skeleton \n Andre Moral \n Zombie \n Jone Candies";
    string credit9 = "And the hero \n Simon Belmondo";
    string credit10 = "You played the greatest role in this story.";
    string credit11 = "Thank you \n for playing.";
    //

    int num;
    float nextChangeName;
    public float changeNameRate;

    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(3f);
        names.SetActive(false);
    }
}
