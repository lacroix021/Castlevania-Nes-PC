using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    public float rotation = 500f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));
    }
}
