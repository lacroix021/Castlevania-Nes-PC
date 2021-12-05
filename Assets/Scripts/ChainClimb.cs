using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainClimb : MonoBehaviour
{

    Rigidbody2D rb;
    SimonController pController;
    Transform playerPos;

    float v;
    bool releaseChain;
    bool grabChain;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }




    public void FallOfChain(InputAction.CallbackContext context)
    {
        releaseChain = context.performed;
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

        /*
        if (releaseChain && pController.climbing)
        {
            pController.climbing = false;
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("GrabChain"))
        {
            pController.climbing = false;
        }
    }
}
