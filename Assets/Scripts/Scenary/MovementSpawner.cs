using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpawner : MonoBehaviour
{
    //funcion senoidal
    public float cycleWidth, frecuency;
    float posY, timer, ySen;

    // Start is called before the first frame update
    void Start()
    {
        posY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        //movimiento senoidal
        timer = timer + (frecuency / 100);
        ySen = Mathf.Sin(timer);
        transform.localPosition = new Vector3(transform.localPosition.x, posY + (ySen * cycleWidth), transform.localPosition.z);
    }
}
