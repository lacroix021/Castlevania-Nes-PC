using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour
{

    SimonController pController;
    // Start is called before the first frame update
    void Start()
    {
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            pController.onSlope = true;
        }
    }
}
