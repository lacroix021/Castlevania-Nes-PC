using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoppelGangerSpawner : MonoBehaviour
{
    public Collider2D activatorCol;
    public LayerMask layerPlayer;

    public GameObject doppelPrefab;
    GameObject doppelBoss;

    public bool active;
    public bool defeated;

    public Transform boundaryFather;

    // Update is called once per frame
    void Update()
    {
        active = Physics2D.IsTouchingLayers(activatorCol, layerPlayer);

        if (active && !doppelBoss && !defeated)
        {
            GameObject.Find("StageExtraA").GetComponent<ActivateMusic>().battle = true;
            doppelBoss = Instantiate(doppelPrefab, transform.position, Quaternion.identity);
            doppelBoss.transform.parent = boundaryFather.transform;
            
        }

        if (defeated)
        {
            GameObject.Find("StageExtraA").GetComponent<ActivateMusic>().battle = false;
        }
    }
}
