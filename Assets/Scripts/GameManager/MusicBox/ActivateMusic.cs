using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateMusic : MonoBehaviour
{
    public GameObject musicLevel;
    public GameObject bossMusic;
    public AudioSource completeMusic;
    
    private HealthPlayer healthPlayer;
    public bool battle;
    public bool complete;

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            healthPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();

            if(healthPlayer.currentHealth <= 0)
            {
                bossMusic.SetActive(false);
                musicLevel.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!complete)
            {
                if (!battle)
                {
                    if (collision.GetComponent<HealthPlayer>().currentHealth > 0)
                    {
                        musicLevel.SetActive(true);
                        bossMusic.SetActive(false);
                        completeMusic.enabled = false;
                    }
                    else
                    {
                        musicLevel.SetActive(false);
                        bossMusic.SetActive(false);
                        completeMusic.enabled = false;
                    }
                }
                else
                {
                    bossMusic.SetActive(true);
                    musicLevel.SetActive(false);
                    completeMusic.enabled = false;
                }
            }
            else
            {
                //corregir esta mierda por que la musica se aloco, hay que darle orden
                completeMusic.enabled = true;
                musicLevel.SetActive(false);
                bossMusic.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            musicLevel.SetActive(false);
            bossMusic.SetActive(false);
        }
    }
}
