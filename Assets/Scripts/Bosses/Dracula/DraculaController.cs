using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaController : MonoBehaviour
{
    public int typeMove;
    float t;
    float posX;
    float nextRandomPosTime;
    public float randomPosRate;
    bool visible;
    public SpriteRenderer sprBody;
    public SpriteRenderer sprHead;
    Collider2D myColl;
    float nextTypeMoveTime;
    public float typeMoveRate;
    public float posXMin;
    public float posXMax;
    Transform playerPos;
    public ParticleSystem particleSys;

    //attack
    Animator anim;
    float nextAttackTime;
    public float attackRate;

    public GameObject firePrefab;
    public GameObject fireBPrefab;
    public Transform firePosA;
    public Transform firePosB;
    public Transform firePosC;

    //head
    public GameObject headDracula;
    HealthBoss healthBoss;
    public GameObject beastPrefab;
    public Transform internalPos;


    // Start is called before the first frame update
    void Start()
    {
        myColl = GetComponent<Collider2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        healthBoss = GetComponent<HealthBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        Invisible();
        ControlVisibility();
        TypeMovement();
        //RangeDetector();
        //ActiveEmiters();
        //BodyDead();
    }

    void Invisible()
    {
        //mientras esta invisible cambia de posicion
        if (t <= 0)
        {
            RandomPos();
            transform.localPosition = new Vector3(posX, transform.localPosition.y, 0);
        }
    }

    void RandomPos()
    {
        if (Time.time >= nextRandomPosTime)
        {
            posX = Random.Range(posXMin, posXMax);
            
            nextRandomPosTime = Time.time + 1f / randomPosRate;
        }
    }

    void ControlVisibility()
    {
        t = Mathf.Clamp(t, 0, 1);
        sprBody.color = new Vector4(1, 1, 1, t);
        sprHead.color = new Vector4(1, 1, 1, t);

        //control de visibilidad y colision
        if (t >= 1)
        {
            visible = true;
            Attack();
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

    void TypeMovement()
    {
        if (Time.time >= nextTypeMoveTime)
        {
            typeMove = Random.Range(0, 2);
            if(typeMove == 1)
            {
                Flip();
                if (!visible)
                {
                    particleSys.Play();
                    AudioManager.instance.PlayAudio(AudioManager.instance.batsFlying);
                }
            }
            else
            {
                if (visible)
                {
                    particleSys.Play();
                    AudioManager.instance.PlayAudio(AudioManager.instance.batsFlying);
                }
            }
            nextTypeMoveTime = Time.time + 1f / typeMoveRate;
        }
    }

    void Attack()
    {
        if (visible)
        {
            if(Time.time >= nextAttackTime)
            {
                TypeFire();
                anim.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void TypeFire()
    {
        float typeFire = Random.Range(0, 2);

        if(typeFire == 0)
        {
            StartCoroutine(TimeFire());
        }
        else
        {
            StartCoroutine(TimeFireTwo());
        }
    }
    IEnumerator TimeFire()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);
        GameObject fireA = Instantiate(firePrefab, firePosA.position, firePosA.rotation);
        GameObject fireB = Instantiate(firePrefab, firePosB.position, firePosB.rotation);
        GameObject fireC = Instantiate(firePrefab, firePosC.position, firePosC.rotation);
    }

    IEnumerator TimeFireTwo()
    {
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);
        GameObject fireD = Instantiate(fireBPrefab, firePosA.position, firePosA.rotation);
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlayAudio(AudioManager.instance.bulletFire);
        GameObject fireE = Instantiate(fireBPrefab, firePosC.position, firePosC.rotation);
    }

    void Flip()
    {
        if (playerPos.position.x - 0.2f > transform.position.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (playerPos.position.x + 0.2f < transform.position.x)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void BodyDead()
    {
        Vector2 lastPos = internalPos.position;
        GameObject beast = Instantiate(beastPrefab, lastPos, Quaternion.identity);
    }
}
