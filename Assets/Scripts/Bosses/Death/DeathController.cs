using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public int typeMove;

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

    //range detector
    public bool inRange;
    public LayerMask layerPlayer;
    public float radius;
    HealthBoss bossHealth;

    //emiters
    public GameObject hozEmiterA;
    public GameObject hozEmiterB;
    public GameObject hozEmiterC;
    public GameObject hozEmiterD;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spr = GetComponent<SpriteRenderer>();
        myColl = GetComponent<Collider2D>();
        bossHealth = GetComponent<HealthBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Invisible();
        ControlVisibility();
        TypeMovement();
        RangeDetector();
        ActiveEmiters();
    }

    void RandomPos()
    {
        if (Time.time >= nextRandomPosTime)
        {
            string tempPosX = Random.Range(-1.32f, 1.33f).ToString("F2");
            string tempPosY = Random.Range(-0.56f, 0.35f).ToString("F2");

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
        //si el jugador no esta en rango se empezara a curar, cuando tenga su maxima vida, asimmilara el valor maximo exacto
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


    void ActiveEmiters()
    {
        if(bossHealth.currentHealth < 70 * bossHealth.maxHealth / 100)
        {
            //print("menos del 70%");
            hozEmiterB.SetActive(true);
        }

        if(bossHealth.currentHealth < 50 * bossHealth.maxHealth / 100)
        {
            //print("menos del 50%");
            hozEmiterC.SetActive(true);
        }
        
        if(bossHealth.currentHealth < 30 * bossHealth.maxHealth / 100)
        {
            //print("menos del 30%");
            hozEmiterD.SetActive(true);
            spr.color = new Color(255,0,0,t);
        }
        if(bossHealth.currentHealth > 70 * bossHealth.maxHealth / 100)
        {
            //full life
            spr.color = new Color(255, 255, 255, t);
            hozEmiterB.SetActive(false);
            hozEmiterC.SetActive(false);
            hozEmiterD.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
