using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGiantNpcController : MonoBehaviour
{

    public float range;
    public bool inRange;
    public LayerMask layerPlayer;
    Transform playerPos;

    float nextMoveTime;
    public float moveRate;

    public float moveSpeed;
    Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("HeadPlayer").GetComponent<Transform>();
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        Detector();
        Movement();
    }
    
    void Detector()
    {
        inRange = Physics2D.OverlapCircle(transform.position, range, layerPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Movement()
    {
        if (inRange)
        {
            if (Time.time > nextMoveTime)
            {
                newPos = playerPos.position;

                nextMoveTime = Time.time + 1 / moveRate;
            }

            transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
        }

        
    }
}
