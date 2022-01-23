using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgorFireEvil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayAudio(AudioManager.instance.holyWater);
        AudioManager.instance.PlayAudio(AudioManager.instance.burnHoly);
    }
}
