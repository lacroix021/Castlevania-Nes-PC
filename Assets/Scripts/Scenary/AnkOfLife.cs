using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnkOfLife : MonoBehaviour
{
    public ParticleSystem particleA;
    public ParticleSystem particleB;

    GameManager gManager;

    /// <summary>
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posY, timer, ySen;
    /// </summary>

    float timeSaveRate;
    public float saveRate;

    public Collider2D collPlayer;
    public bool isActive;
    public string nameTp;

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.gameManager;
        posY = transform.position.y;
    }

    private void FixedUpdate()
    {
        MovementAnk();
    }

    void MovementAnk()
    {
        //movimiento senoidal
        timer = timer + (frecuency / 100);
        ySen = Mathf.Sin(timer);
        transform.position = new Vector3(transform.position.x, posY + (ySen * cycleWidth), transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && coll.GetComponent<SimonController>().activating && !gManager.gamePaused)
        {
            isActive = true;
            gManager.collPlayer = coll;
            gManager.particleA = particleA;
            gManager.particleB = particleB;
            gManager.nameTeleporter.text = nameTp;

            //activar menu de guardado o teletransporte
            gManager.teleportMenu = true;
            gManager.GetComponentInChildren<StructureManager>().CheckWalls();
        }
    }
}
