using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public int typeMove;
    Rigidbody2D rb;

    //X -1.32f, 1.32f
    //Y -0.56f, 0.56f

    [SerializeField] float posX;
    [SerializeField] float posY;

    float nextRandomPosTime;
    public float randomPosRate;
    Transform playerPos;
    SpriteRenderer spr;
    Collider2D myColl;
    public float t;
    public bool visible;

    public Transform boundaryFather;

    float nextTypeMoveTime;
    public float typeMoveRate;

    //attack
    public GameObject hozPrefab;

    public Transform hozEmiterA;
    public Transform hozEmiterB;
    public Transform hozEmiterC;
    public Transform hozEmiterD;
    public Transform hozEmiterE;
    public Transform hozEmiterF;
    public Transform hozEmiterG;
    public Transform hozEmiterH;


    public Transform directionA;
    public Transform directionB;
    public Transform directionC;
    public Transform directionD;
    public Transform directionE;
    public Transform directionF;
    public Transform directionG;
    public Transform directionH;


    Vector3 targetOrientationA;
    Vector3 targetOrientationB;
    Vector3 targetOrientationC;
    Vector3 targetOrientationD;
    Vector3 targetOrientationE;
    Vector3 targetOrientationF;
    Vector3 targetOrientationG;
    Vector3 targetOrientationH;
    

    float nextAttackTime;
    public float attackRate;
    public float force;


    //range detector
    public bool inRange;
    public LayerMask layerPlayer;
    public float radius;
    HealthBoss bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;

        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spr = GetComponent<SpriteRenderer>();
        myColl = GetComponent<Collider2D>();
        boundaryFather = GameObject.Find("Boundary").transform;
        bossHealth = GetComponent<HealthBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Invisible();
        ControlVisibility();
        TypeMovement();

        if (inRange)
        {
            Attack();
        }
        
        RangeDetector();
    }

    void RandomPos()
    {
        if (Time.time >= nextRandomPosTime)
        {
            string tempPosX = Random.Range(-1.32f, 1.33f).ToString("F2");
            string tempPosY = Random.Range(-0.56f, 0.57f).ToString("F2");

            posX = float.Parse(tempPosX);
            posY = float.Parse(tempPosY);

            nextRandomPosTime = Time.time + 1f / randomPosRate;
        }
    }

    void Invisible()
    {
        //mientras esta invisible cambia de posicion
        if (t <= 0)
        {
            RandomPos();
            transform.localPosition = new Vector3(posX, posY, 0);
        }
    }

    void Attack()
    {
        if (visible)
        {
            targetOrientationA = directionA.position - hozEmiterA.position;
            targetOrientationB = directionB.position - hozEmiterB.position;
            targetOrientationC = directionC.position - hozEmiterC.position;
            targetOrientationD = directionD.position - hozEmiterD.position;
            targetOrientationE = directionE.position - hozEmiterE.position;
            targetOrientationF = directionF.position - hozEmiterF.position;
            targetOrientationG = directionG.position - hozEmiterG.position;
            targetOrientationH = directionH.position - hozEmiterH.position;

            

            if (Time.time >= nextAttackTime)
            {
                GameObject bulletInstA = Instantiate(hozPrefab, hozEmiterA.position, Quaternion.identity);
                bulletInstA.GetComponent<Rigidbody2D>().AddForce(targetOrientationA * force * Time.deltaTime);

                GameObject bulletInstB = Instantiate(hozPrefab, hozEmiterB.position, Quaternion.identity);
                bulletInstB.GetComponent<Rigidbody2D>().AddForce(targetOrientationB * force * Time.deltaTime);

                GameObject bulletInstC = Instantiate(hozPrefab, hozEmiterC.position, Quaternion.identity);
                bulletInstC.GetComponent<Rigidbody2D>().AddForce(targetOrientationC * force * Time.deltaTime);

                GameObject bulletInstD = Instantiate(hozPrefab, hozEmiterD.position, Quaternion.identity);
                bulletInstD.GetComponent<Rigidbody2D>().AddForce(targetOrientationD * force * Time.deltaTime);

                GameObject bulletInstE = Instantiate(hozPrefab, hozEmiterE.position, Quaternion.identity);
                bulletInstE.GetComponent<Rigidbody2D>().AddForce(targetOrientationE * force * Time.deltaTime);

                GameObject bulletInstF = Instantiate(hozPrefab, hozEmiterF.position, Quaternion.identity);
                bulletInstF.GetComponent<Rigidbody2D>().AddForce(targetOrientationF * force * Time.deltaTime);

                GameObject bulletInstG = Instantiate(hozPrefab, hozEmiterG.position, Quaternion.identity);
                bulletInstG.GetComponent<Rigidbody2D>().AddForce(targetOrientationG * force * Time.deltaTime);

                GameObject bulletInstH = Instantiate(hozPrefab, hozEmiterH.position, Quaternion.identity);
                bulletInstH.GetComponent<Rigidbody2D>().AddForce(targetOrientationH * force * Time.deltaTime);

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void TypeMovement()
    {
        if (Time.time >= nextTypeMoveTime)
        {
            typeMove = Random.Range(0, 2);

            nextTypeMoveTime = Time.time + 1f / typeMoveRate;
        }
    }

    void Flip()
    {
        if (playerPos.position.x - 0.4f > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (playerPos.position.x + 0.4f < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void ControlVisibility()
    {
        t = Mathf.Clamp(t, 0, 1);
        spr.color = new Vector4(1, 1, 1, t);

        //control de visibilidad y colision
        if (t >= 1)
        {
            visible = true;

        }
        else if (t >= 0.5f)
        {
            myColl.enabled = true;
        }
        else
        {
            visible = false;
            myColl.enabled = false;
        }

        //control tipo de movimiento
        if (typeMove == 0)
        {
            t -= Time.deltaTime;
        }
        else if (typeMove == 1)
        {
            t += Time.deltaTime;
        }
    }

    void RangeDetector()
    {
        inRange = Physics2D.OverlapCircle(transform.position, radius, layerPlayer);
        //sie ljugador no esta en rango se empezara a curar, cuando tenga su maxima vida, asimmilara el valor maximo exacto
        if (!inRange)
        {
            if(bossHealth.currentHealth < bossHealth.maxHealth)
            {
                bossHealth.currentHealth += 1 * Time.deltaTime;
            }
            else
            {
                bossHealth.currentHealth = bossHealth.maxHealth;
            }
        }
        else
        {
            //activar musica de batalla
            GameObject.Find("Stage5Music").GetComponent<ActivateMusic>().battle = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
