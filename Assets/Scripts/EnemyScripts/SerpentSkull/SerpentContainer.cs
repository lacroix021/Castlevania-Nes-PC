using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentContainer : MonoBehaviour
{
    public GameObject head;
    
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (!head)
            Destroy(this.gameObject);
    }
}
