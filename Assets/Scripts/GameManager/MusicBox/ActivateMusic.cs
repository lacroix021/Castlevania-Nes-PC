using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateMusic : MonoBehaviour
{
    public GameObject musicLevel;
    public GameObject bossMusic;
    
    private HealthPlayer healthPlayer;
    public bool battle;

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
            if (!battle)
            {
                if (collision.GetComponent<HealthPlayer>().currentHealth > 0)
                {
                    musicLevel.SetActive(true);
                    bossMusic.SetActive(false);
                }
                else
                {
                    musicLevel.SetActive(false);
                    bossMusic.SetActive(false);
                }
            }
            else
            {
                bossMusic.SetActive(true);
                musicLevel.SetActive(false);
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
