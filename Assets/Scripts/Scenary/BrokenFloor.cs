using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    public GameObject particle;
    private GameObject particleInstance;
    CeilingSpawnerMummies ceilingSpawner;


    // Start is called before the first frame update
    void Start()
    {
        
        ceilingSpawner = GameObject.FindObjectOfType<CeilingSpawnerMummies>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ceilingSpawner.interruptorOn)
        {
            float numA = Random.Range(-15, 1);
            float numB = Random.Range(0, 16);
            float numC = Random.Range(-14, 1);
            float numD = Random.Range(0, 15);

            float forceA = Random.Range(60, 120);
            float forceB = Random.Range(60, 120);
            float forceC = Random.Range(60, 120);
            float forceD = Random.Range(60, 120);

            particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
            particleInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            particleInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(numA * Time.deltaTime, forceA * Time.deltaTime), ForceMode2D.Impulse);

            GameObject particleInstanceB = Instantiate(particle, transform.position, Quaternion.identity);
            particleInstanceB.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            particleInstanceB.GetComponent<Rigidbody2D>().AddForce(new Vector2(numB * Time.deltaTime, forceB * Time.deltaTime), ForceMode2D.Impulse);

            GameObject particleInstanceC = Instantiate(particle, transform.position, Quaternion.identity);
            particleInstanceC.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            particleInstanceC.GetComponent<Rigidbody2D>().AddForce(new Vector2(numC * Time.deltaTime, forceC * Time.deltaTime), ForceMode2D.Impulse);

            GameObject particleInstanceD = Instantiate(particle, transform.position, Quaternion.identity);
            particleInstanceD.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            particleInstanceD.GetComponent<Rigidbody2D>().AddForce(new Vector2(numD * Time.deltaTime, forceD * Time.deltaTime), ForceMode2D.Impulse);

            Destroy(this.gameObject);
        }
    }
}
