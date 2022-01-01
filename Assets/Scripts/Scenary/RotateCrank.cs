using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrank : MonoBehaviour
{
    public float rotation;
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(Time.time * rotation, new Vector3(0, 0, 1));
    }
}
