using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HozSpawner : MonoBehaviour
{
    public float minPosX;
    public float maxPosX;
    public float minPosY;
    public float maxPosY;

    float nextMoveTime;
    public float moveRate;

    public GameObject hozPrefab;

    DeathController deathController;

    float nextAttackTime;
    public float attackRate;
    public float force;
    Transform playerPos;
    public Vector2 targetOrientation;
    public float seconds;

    // Start is called before the first frame update
    void Start()
    {
        deathController = GetComponentInParent<DeathController>();
        playerPos = GameObject.FindGameObjectWithTag("HeadPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        NewAttack();
        

        if (Time.time >= nextMoveTime)
        {
            transform.localPosition = new Vector3(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY), 0);

            nextMoveTime = Time.time + 1f / moveRate;
        }
    }

    void NewAttack()
    {
        if (deathController.visible && deathController.inRange)
        {
            targetOrientation = playerPos.position - transform.position;

            Debug.DrawRay(transform.position, targetOrientation, Color.green);

            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(StepAttack());

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    IEnumerator StepAttack()
    {
        yield return new WaitForSeconds(seconds);
        GameObject bulletInst = Instantiate(hozPrefab, transform.position, Quaternion.identity);
        bulletInst.GetComponent<Rigidbody2D>().AddForce(targetOrientation * force * Time.deltaTime);
    }
}
