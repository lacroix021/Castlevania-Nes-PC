using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : MonoBehaviour
{
    float z;
    

    // Update is called once per frame
    void Update()
    {
        z += 300;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
