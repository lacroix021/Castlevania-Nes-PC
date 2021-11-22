using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour
{
    public bool playerAbove;
    Collider2D thisCollider;
    public LayerMask layerPlayer;
    public float timesUp;

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        playerAbove = Physics2D.IsTouchingLayers(thisCollider, layerPlayer);
        
        if (this.gameObject.activeSelf== true)
        {
            if (!playerAbove)
            {
                timesUp += Time.deltaTime;

                if (timesUp > 0.3f)
                {
                    this.gameObject.SetActive(false);
                    timesUp = 0;
                }
            }
            else
            {
                timesUp = 0;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            playerAbove = false;
            timesUp = 0;
            this.gameObject.SetActive(false);
        }
    }
}
