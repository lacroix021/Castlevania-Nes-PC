using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorStair : MonoBehaviour
{
    public GameObject stair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && Input.GetAxisRaw("Vertical") > 0 || coll.CompareTag("Player") && Input.GetAxisRaw("Vertical") < 0)
        {
            stair.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            stair.SetActive(false);
        }
    }
}
