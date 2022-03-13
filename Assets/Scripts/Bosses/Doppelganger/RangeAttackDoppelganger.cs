using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackDoppelganger : MonoBehaviour
{
    private float nextSubTime;
    public float subRate;
    public int typeSub;

    DoppelgangerController enemyController;
    HealthPlayer health;


    public GameObject[] subWeapons;

    public Transform subPos;
    public Transform subPosB;

    /*
    type of subweapon
    0 = knife
    1 = Axe
    2 = Holy Water
    3 = Cross
    */

    public bool canCrush = false;
    float nextCrushTime;
    public float crushRate = 0.2f;
    public Transform crushPos;
    public GameObject prefabICrushAxe;
    GameObject instanceCrush;

    float randomTypeTime;
    public float randomTypeRate;


    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<DoppelgangerController>();
        health = GetComponent<HealthPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        InputController();
    }


    void InputController()
    {
        if(Time.time >= randomTypeTime)
        {
            typeSub = Random.Range(0, 6);   //randomiza el tipo de sub arma de 0 a 3, mayor que eso es sin subarmas

            randomTypeTime = Time.time + 1 / randomTypeRate;
        }

        if (enemyController.distanceAttackRange && enemyController.canMove && !enemyController.isSlide)
        {
            //Dagas y cooldown
            if (typeSub < 4)
            {
                if (Time.time >= nextSubTime)
                {
                    enemyController.anim.SetTrigger("Sub");
                    enemyController.rb.velocity = new Vector2(0, enemyController.rb.velocity.y);
                    nextSubTime = Time.time + 1f / subRate;
                }
            }
        }

        //item crush
        if (canCrush && enemyController.distanceAttackRange && enemyController.v > 0 && enemyController.canMove)
        {
            if (Time.time >= nextCrushTime)
            {
                health.isInvulnerable = true;
                Physics2D.IgnoreLayerCollision(9, 10, true);
                enemyController.rb.velocity = Vector2.zero;
                enemyController.rb.AddForce(Vector2.up * 100 * Time.fixedDeltaTime, ForceMode2D.Impulse);
                StartCoroutine(MidAir());
                enemyController.anim.SetTrigger("ItemCrush");
                enemyController.canMove = false;
                StartCoroutine(PostItemCrush());
                ParticleCrush();
                nextCrushTime = Time.time + 1f / crushRate;
            }
        }
    }

    void InstantiateDSubWeapon()
    {
        if(typeSub < 4)
        {
            Instantiate(subWeapons[typeSub], subPos.position, Quaternion.identity);
        }
    }


    void ParticleCrush()
    {
        if (typeSub == 1)
        {
            if (!instanceCrush)
                instanceCrush = Instantiate(prefabICrushAxe, crushPos.position, Quaternion.identity);
        }
    }

    IEnumerator PostItemCrush()
    {
        yield return new WaitForSeconds(1.8f);
        enemyController.canMove = true;
        health.isInvulnerable = false;
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    IEnumerator MidAir()
    {
        yield return new WaitForSeconds(0.2f);
        enemyController.rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.7f);
        enemyController.rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
