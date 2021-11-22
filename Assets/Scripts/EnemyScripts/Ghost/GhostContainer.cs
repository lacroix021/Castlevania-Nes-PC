using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostContainer : MonoBehaviour
{
    GhostController ghostSon;
    // Start is called before the first frame update
    void Start()
    {
        ghostSon = GetComponentInChildren<GhostController>();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!ghostSon || ghostSon == null)
        {
            Destroy(this.gameObject);
        }
    }
}
