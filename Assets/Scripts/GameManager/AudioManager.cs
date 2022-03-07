using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public float masterVol, effectsVol;

    public AudioMixer musicMixer, effectsMixer;

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
        lifeLost,
        batsFlying,
        slide,
        end,
        saveSound,
        teleport;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            effectsVol = -2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MasterVolume();
        EffectsVolume();
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void StopAudio(AudioSource audio)
    {
        audio.Stop();
    }

    public void MasterVolume()
    {
        musicMixer.SetFloat("masterVolume", masterVol);
    }
    public void EffectsVolume()
    {
        effectsMixer.SetFloat("effectsVolume", effectsVol);
    }
}
