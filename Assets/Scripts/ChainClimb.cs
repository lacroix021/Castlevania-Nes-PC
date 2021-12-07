using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChainClimb : MonoBehaviour
{

    Rigidbody2D rb;
    SimonController pController;
    Transform playerPos;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }



    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabChain") &&
            collision.GetComponentInParent<SimonController>().activating && 
            collision.GetComponentInParent<HealthPlayer>().currentHealth > 0)
        {
            pController.climbing = true;
            playerPos.position = new Vector3(transform.position.x, playerPos.position.y, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("GrabChain"))
        {
            pController.climbing = false;
        }
    }
}
