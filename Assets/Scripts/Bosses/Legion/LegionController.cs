using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionController : MonoBehaviour
{

    public GameObject PartA;
    public GameObject PartB;
    public GameObject PartC;
    public GameObject PartD;

    public Transform startPoint;

    public float moveSpeed;

    public bool starting;


    /// <summary>
    //funcion senoidal
    public float cycleWidthX, frecuencyX;
    float posX, timerX, xSen;


    public float cycleWidthY, frecuencyY;
    float posY, timerY, ySen;
    /// </summary>

    private void Start()
    {
        starting = true;
        startPoint = GameObject.Find("StartPointLegion").transform;
    }

    private void OnDisable()
    {
        //si se sale del boundary se destruye el boss y no debe contar como muerte para que respawnee cuando se active el boundary nuevamente
        Destroy(this.gameObject);
    }


    private void Update()
    {
        MoveStartPoint();
        if(!starting)
            DetectorParts();
    }

    void DetectorParts()
    {
        if (PartA || PartB || PartC || PartD)
        {
            MovementSen();
        }
    }

    void MoveStartPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPoint.position, moveSpeed * Time.deltaTime);

        if(transform.position == startPoint.position && starting)
        {
            //fijar posicion inicial para movimiento senoidal
            posX = transform.position.x;
            posY = transform.position.y;
            starting = false;
        }
    }

    void MovementSen()
    {
        //movimiento senoidal
        timerX = timerX + (frecuencyX / 100);
        xSen = Mathf.Sin(timerX);

        timerY = timerY + (frecuencyY / 100);
        ySen = Mathf.Sin(timerY);

        transform.position = new Vector3(posX + (xSen * cycleWidthX), posY + (ySen * cycleWidthY), transform.position.z);
    }
}
