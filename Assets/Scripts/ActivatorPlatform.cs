using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorPlatform : MonoBehaviour
{
    public GameObject Platform;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetAxisRaw("Vertical") < 0)
        {
            Platform.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Platform.SetActive(true);
        }
    }
}
