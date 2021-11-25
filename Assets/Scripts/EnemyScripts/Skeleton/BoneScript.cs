using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    float z;
    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        z += rotation * Time.deltaTime;

        transform.localRotation = Quaternion.EulerRotation(0, 0, z);
    }
}
