using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBulletController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
