using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    public GameObject particleBlock;

    public Transform partA, partB, partC, partD;

    public bool haveLoot;

    GameObject part1;
    GameObject part2;
    GameObject part3;
    GameObject part4;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Weapon"))
        {
            ImpulseParts();

            if (haveLoot)
            {
                GetComponent<WallLoot>().InstantiateLoot();
            }

            Destroy(this.gameObject);
        }
    }

    void ImpulseParts()
    {

        if (!part1)
            part1 = Instantiate(particleBlock, partA.position, Quaternion.identity);
        if(!part2)
            part2 = Instantiate(particleBlock, partB.position, Quaternion.identity);
        if(!part3)
            part3 = Instantiate(particleBlock, partC.position, Quaternion.identity);
        if(!part4)
            part4 = Instantiate(particleBlock, partD.position, Quaternion.identity);

        part1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * 30 * Time.deltaTime, 70 * Time.deltaTime), ForceMode2D.Impulse);
        part2.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * 30 * Time.deltaTime, 70 * Time.deltaTime), ForceMode2D.Impulse);
        part3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * 30 * Time.deltaTime, 20 * Time.deltaTime), ForceMode2D.Impulse);
        part4.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * 30 * Time.deltaTime, 20 * Time.deltaTime), ForceMode2D.Impulse);
    }
}
