using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkController : MonoBehaviour
{
    public float movespeed;
    float direction;
    public GameObject fleaPrefab;
    public GameObject fleaPos;
    GameObject fleaDroped;

    public bool inRange;
    public bool droped;
    public Collider2D triggerCol;
    public LayerMask layerPlayer;
    Transform playerPos;

    public Transform boundaryFather;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        droped = false;
        Flip();
        boundaryFather = GameObject.Find("Boundary").GetComponent<Transform>();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        RangeDetector();
        DropFleaman();
        transform.position = new Vector3(transform.position.x + (direction * movespeed * Time.deltaTime), transform.position.y, 0);
    }
    void RangeDetector()
    {
        inRange = Physics2D.IsTouchingLayers(triggerCol, layerPlayer);
    }

    void Flip()
    {
        //flip
        if (playerPos.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = -1;
        }
        else if (playerPos.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = 1;
        }
    }

    void DropFleaman()
    {
        if (inRange)
        {
            if (!fleaDroped && !droped)
            {
                fleaDroped = Instantiate(fleaPrefab, fleaPos.transform.position, Quaternion.identity);
                fleaPos.GetComponent<SpriteRenderer>().enabled = false;
                droped = true;
                fleaDroped.GetComponent<Transform>().transform.parent = boundaryFather.transform;
            }
        }
    }
}
