using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float speedKnife;
    private float dirKnife;

    SubWeaponSystem weaponSys;
    Rigidbody2D rb;

    //160
    //200
    //300

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();
        dirKnife = weaponSys.direction;
        transform.localScale = new Vector3(dirKnife, -1 , 1);
    }

    private void FixedUpdate()
    {
        ImpulseKnife();
    }

    void ImpulseKnife()
    {
        if(weaponSys.multiplierPow == 0)
        {
            speedKnife = 160;
            rb.velocity = new Vector2((dirKnife * -1) * speedKnife * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (weaponSys.multiplierPow == 1)
        {
            speedKnife = 200;
            rb.velocity = new Vector2((dirKnife * -1) * speedKnife * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (weaponSys.multiplierPow == 2)
        {
            speedKnife = 300;
            rb.velocity = new Vector2((dirKnife * -1) * speedKnife * Time.fixedDeltaTime, rb.velocity.y);
        }
    }
}
