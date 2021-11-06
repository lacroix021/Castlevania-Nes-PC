using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponSystem : MonoBehaviour
{
    private float nextSubTime;
    float subRate;

    SimonController pController;
    HeartsSystem heartsSys;
    SoundsSimon soundSimon;

    public bool haveSub;

    public int typeSub = 0;

    public float direction;

    public GameObject[] subWeapons;

    public Transform subPos;
    public Transform subPosB;

    public int multiplierPow;

    /*
    type of subweapon
    0 = knife
    1 = Axe
    2 = Holy Water
    3 = Cross
    */

    // Start is called before the first frame update
    void Start()
    {
        pController = GetComponent<SimonController>();
        heartsSys = GetComponent<HeartsSystem>();
        soundSimon = GetComponent<SoundsSimon>();
    }

    // Update is called once per frame
    void Update()
    {
        InputSubWeapon();
        RateSubWeapons();
    }

    void InputSubWeapon()
    {
        if (Input.GetButtonDown("Fire2") && haveSub && !pController.animSlide)
        {
            //Dagas y cooldown
            if(typeSub == 0 && heartsSys.currentHearts >= 1)
            {
                if (Time.time >= nextSubTime)
                {
                    pController.anim.SetTrigger("Sub");
                    pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                    heartsSys.currentHearts -= 1;
                    nextSubTime = Time.time + 1f / subRate;
                }
            }
            //achas y cooldown
            else if(typeSub == 1 && heartsSys.currentHearts >= 2)
            {
                if (Time.time >= nextSubTime)
                {
                    pController.anim.SetTrigger("Sub");
                    pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                    heartsSys.currentHearts -= 2;
                    nextSubTime = Time.time + 1f / subRate;
                }
            }
            //agua bendita y cooldown
            else if (typeSub == 2 && heartsSys.currentHearts >= 2)
            {
                if (Time.time >= nextSubTime)
                {
                    pController.anim.SetTrigger("Sub");
                    pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                    heartsSys.currentHearts -= 2;
                    nextSubTime = Time.time + 1f / subRate;
                }
            }
            //cruz y cooldown
            else if (typeSub == 3 && heartsSys.currentHearts >= 2)
            {
                if (Time.time >= nextSubTime)
                {
                    pController.anim.SetTrigger("Sub");
                    pController.rb.velocity = new Vector2(0, pController.rb.velocity.y);
                    heartsSys.currentHearts -= 2;
                    nextSubTime = Time.time + 1f / subRate;
                }
            }

            heartsSys.CheckHearts();
        }
    }

    void InstantiateSubWeapon()
    {
        direction = transform.localScale.x;

        if(multiplierPow == 0 || multiplierPow == 1)
        {
            Instantiate(subWeapons[typeSub], subPos.position, Quaternion.identity);
        }
        else if(multiplierPow == 2)
        {
            Instantiate(subWeapons[typeSub], subPos.position, Quaternion.identity);
            Instantiate(subWeapons[typeSub], subPosB.position, Quaternion.identity);
        }
    }

    void RateSubWeapons()
    {
        if(typeSub == 0)
        {
            if (multiplierPow == 0)
                subRate = 0.8f;
            else if (multiplierPow == 1)
                subRate = 1.4f;
            else if (multiplierPow == 2)
                subRate = 1.8f;
        }
        else if(typeSub == 1)
        {
            if (multiplierPow == 0)
                subRate = 0.7f;
            else if (multiplierPow == 1)
                subRate = 1.4f;
            else if (multiplierPow == 2)
                subRate = 1.6f;
        }
        else if(typeSub == 2)
        {
            if (multiplierPow == 0)
                subRate = 0.7f;
            else if (multiplierPow == 1)
                subRate = 1.1f;
            else if (multiplierPow == 2)
                subRate = 1.3f;
        }
        else if(typeSub == 3)
        {
            if (multiplierPow == 0)
                subRate = 0.4f;
            else if (multiplierPow == 1)
                subRate = 0.7f;
            else if (multiplierPow == 2)
                subRate = 0.9f;
        }
    }
}
