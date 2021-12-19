using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    public bool level2;
    public bool level3;

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
        if (level2)
        {
            yield return new WaitForSeconds(4.427f);
            audioSource.clip = loop;
            audioSource.loop = true;
            audioSource.Play();
        }
        
        if (level3)
        {
            yield return new WaitForSeconds(22.635f);
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
