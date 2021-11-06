using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMessageDoor : MonoBehaviour
{
    public GameObject barDoorMessaje;

    

    public void DisableBannerMessageDoor()
    {
        barDoorMessaje.SetActive(false);
    }
}
