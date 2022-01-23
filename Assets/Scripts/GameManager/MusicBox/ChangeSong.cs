using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    public enum levels
    {
        level2,
        level3,
        level4
    };
    public levels Level;

    public AudioClip intro;
    public AudioClip loop;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource.clip = intro;
        audioSource.loop = false;
        audioSource.Play();
        StartCoroutine(StartLoop());
    }

    IEnumerator StartLoop()
    {
        if (Level == levels.level2)
        {
            yield return new WaitForSeconds(4.427f);
            audioSource.clip = loop;
            audioSource.loop = true;
            audioSource.Play();
        }
        
        if (Level == levels.level3)
        {
            yield return new WaitForSeconds(22.635f);
            audioSource.clip = loop;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (Level == levels.level4)
        {
            yield return new WaitForSeconds(23.350f);
            audioSource.clip = loop;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void OnRespawnMusic()
    {
        audioSource.clip = intro;
        audioSource.loop = false;
    }
}
