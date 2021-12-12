using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject bubblePrefab;
    GameObject instancedBubbleL;
    GameObject instancedBubbleR;
    GameObject instancedBubbleM;

    BoxCollider2D colliderWater;
    /*Enemigos a spawnear desde el agua*/
    [Header("ENEMYIES TO SPAWN")]
    public GameObject mermanPrefab;
    public float spawnRate;
    private float nextSpawnTime;
    public GameObject boundaryFather;

    [Header("SONIDOS")]
    AudioSource aSource;
    public AudioClip inWater;
    public AudioClip outWater;

    [Header("CONTADORES")]
    public int limitMermans;
    [SerializeField] int numMermans;
    [SerializeField] MermanController[] cantMermans;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        colliderWater = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InstanceCreature();
        cantMermans = GameObject.FindObjectsOfType<MermanController>();
        numMermans = cantMermans.Length;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aSource.clip = outWater;
            aSource.loop = false;
            aSource.Play();

            instancedBubbleL = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleL.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 * Time.deltaTime, 40 * Time.deltaTime), ForceMode2D.Impulse);
            instancedBubbleL.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            instancedBubbleR = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleR.GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * Time.deltaTime, 40 * Time.deltaTime), ForceMode2D.Impulse);
            instancedBubbleR.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            instancedBubbleM = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleM.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 40 * Time.deltaTime), ForceMode2D.Impulse);

            //jugador muere
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            aSource.clip = outWater;
            aSource.loop = false;
            aSource.Play();

            instancedBubbleL = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleL.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 * Time.deltaTime, 40 * Time.deltaTime), ForceMode2D.Impulse);
            instancedBubbleL.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            instancedBubbleR = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleR.GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * Time.deltaTime, 40 * Time.deltaTime), ForceMode2D.Impulse);
            instancedBubbleR.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            instancedBubbleM = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleM.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 40 * Time.deltaTime), ForceMode2D.Impulse);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            aSource.clip = outWater;
            aSource.loop = false;
            aSource.Play();

            instancedBubbleL = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleL.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 * Time.deltaTime, 40 * Time.deltaTime), ForceMode2D.Impulse);
            instancedBubbleL.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            instancedBubbleR = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleR.GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * Time.deltaTime, 40 * Time.deltaTime), ForceMode2D.Impulse);
            instancedBubbleR.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            instancedBubbleM = Instantiate(bubblePrefab, other.transform.position, Quaternion.identity);
            instancedBubbleM.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 40 * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    void InstanceCreature()
    {
        if(numMermans <= limitMermans -1)
        {
            if (Time.time >= nextSpawnTime)
            {
                float posX = Random.Range(colliderWater.bounds.min.x, colliderWater.bounds.max.x + 0.1f);
                GameObject instance = Instantiate(mermanPrefab, new Vector3(posX, transform.position.y, 0), Quaternion.identity);
                instance.transform.parent = boundaryFather.GetComponent<Transform>();
                instance.name = mermanPrefab.name;
                nextSpawnTime = Time.time + 1f / spawnRate;
            }
        }
    }
}
