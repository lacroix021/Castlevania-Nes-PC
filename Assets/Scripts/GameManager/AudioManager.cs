using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer music, effects;

    public AudioSource 
        attack, 
        hit, 
        breakWall, 
        bulletFire, 
        burnHoly, 
        door, 
        grabGold, 
        grabItem, 
        grabHearts, 
        inWater, 
        lifeMax, 
        makeDamageBoss, 
        outWater, 
        pause, 
        simonHurt,
        dagger, 
        holyWater,         
        stage1Loop,
        lifeLost,
        bossMusicLoop;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void StopAudio(AudioSource audio)
    {
        audio.Stop();
    }
}
