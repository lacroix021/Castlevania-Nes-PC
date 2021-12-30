using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    Camera cameraMap;
    Rigidbody2D rb;

    float vMap;
    float h, v;

    Transform player;
    GameManager gManager;


    SimonController playerController;

    // Start is called before the first frame update
    

    private void OnEnable()
    {
        cameraMap = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        gManager = GameManager.gameManager;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        MovementCameraMap();

        vMap = playerController.vRStick;
        h = playerController.h;
        v = playerController.v;
    }

    void MovementCameraMap()
    {
        if (transform.localPosition.x <= -666.1349f)
            transform.localPosition = new Vector3(-666.1349f, transform.localPosition.y, transform.localPosition.z);
        else if (transform.localPosition.x >= -517.1349f)
            transform.localPosition = new Vector3(-517.1349f, transform.localPosition.y, transform.localPosition.z);
        
        if (transform.localPosition.y <= -370f)
            transform.localPosition = new Vector3(transform.localPosition.x, -370f, transform.localPosition.z);
        else if (transform.localPosition.y >= -328f)
            transform.localPosition = new Vector3(transform.localPosition.x, -328f, transform.localPosition.z);

        if (gManager.navigationMode)
        {
            if (h > 0.5f)
            {
                transform.position = new Vector3(transform.position.x - h, transform.position.y, -10);
            }
            else if (h < -0.5f)
            {
                transform.position = new Vector3(transform.position.x - h, transform.position.y, -10);
            }
            else if (v > 0.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - v, -10);
            }
            else if (v < -0.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - v, -10);
            }

            //distancia de mapa
            cameraMap.orthographicSize = Mathf.Clamp(cameraMap.orthographicSize, 15, 35);


            if (vMap > 0)
            {
                cameraMap.orthographicSize -= vMap;
            }
            else if (vMap < 0)
            {
                cameraMap.orthographicSize -= vMap;
            }
        }
    }
}
