using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HozController : MonoBehaviour
{
    public float rotation;
    public GameObject blood;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(rotation * Time.time, new Vector3(0, 0, 1));
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Transform newPos = GameObject.Find("CeilingCheck").transform;
            Instantiate(blood, newPos.position, Quaternion.identity);
        }
    }
}
