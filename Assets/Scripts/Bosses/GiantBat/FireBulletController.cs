using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletController : MonoBehaviour
{
    GiantBatController batController;

    private float moveAttack = 0.9f;
    private float posX;
    private float posY;
    // Start is called before the first frame update
    void Start()
    {
        batController = GameObject.Find("GiantBat").GetComponent<GiantBatController>();
        posX = batController.posX;
        posY = batController.posY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, posY, 0), moveAttack * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Destroy(this.gameObject);
        }
    }
}
