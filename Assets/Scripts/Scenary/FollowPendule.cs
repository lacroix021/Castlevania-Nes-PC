using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPendule : MonoBehaviour
{
    public Transform pendulePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = pendulePos.position;
    }
}
