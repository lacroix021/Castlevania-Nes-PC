using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaFireBullet : MonoBehaviour
{
    public float speed;
    
    // Update is called once per frame
    void Update()
    {
        //se deja el vector en down por que el prefab tiene rotacion
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
