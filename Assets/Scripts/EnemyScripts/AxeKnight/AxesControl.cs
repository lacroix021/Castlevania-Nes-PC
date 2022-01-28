using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxesControl : MonoBehaviour
{
    public enum typeAxe
    {
        axeFloat,
        axeGravity
    };
    public typeAxe TypeAxe;

    public float rotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));

        if(TypeAxe == typeAxe.axeFloat)
        {
            //poner ida y vuelta de las hachas
        }
        else if(TypeAxe == typeAxe.axeGravity)
        {
            //poner funcion para que se impulse hacia arriba y luego caiga el hacha
        }
    }
}
