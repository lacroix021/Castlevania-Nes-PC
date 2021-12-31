using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrank : MonoBehaviour
{
    public float rotation;
    float newRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newRotation = newRotation + (rotation * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.z + newRotation);
    }
}
