using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    public enum levels
    {
        level2,
        level3,
        level4,
        level5,
        level7,
        extraA,
        extraB
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
        yield return new WaitForSeconds(intro.length);
        audioSource.clip = loop;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void OnRespawnMusic()
    {
        if(Level != levels.level7)
        {
            audioSource.clip = intro;
            audioSource.loop = false;
        }
        else
        {
            audioSource.clip = intro;
            audioSource.loop = true;
        }
    }
}
