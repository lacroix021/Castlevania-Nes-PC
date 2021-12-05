using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivatorsUpDown : MonoBehaviour
{
    public bool up;
    public bool down;
    public GameObject stair;
   


    private void OnTriggerStay2D(Collider2D coll)
    {
        //para subir
        if (coll.CompareTag("Player") && coll.GetComponent<SimonController>().v > 0.8f)
        {
            if (up)
                stair.SetActive(true);
        }

        //para bajar
        if (coll.CompareTag("Player") && coll.GetComponent<SimonController>().v < -0.8)
        {
            if (down)
                stair.SetActive(true);
        }
    }
}
