using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float forceKnockback;
    DoppelgangerController enemyController;
    HealthBoss healthBoss;
    float colPosX;



    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<DoppelgangerController>();
        healthBoss = GetComponent<HealthBoss>();
        
    }

    void Retroceso()
    {
        enemyController.canMove = false;
        enemyController.anim.SetBool("Hurting", true);
        enemyController.rb.velocity = Vector2.zero;

        if (colPosX > transform.position.x)
        {
            enemyController.rb.AddForce(new Vector2(-forceKnockback * Time.fixedDeltaTime, 100 * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        else if (colPosX < transform.position.x)
        {
            enemyController.rb.AddForce(new Vector2(forceKnockback * Time.fixedDeltaTime, 100 * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        
        StartCoroutine(MoveAgain());
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Weapon") && healthBoss.currentHealth > 0 && coll.GetComponent<DamagePlayer>())
        {
            colPosX = coll.transform.position.x;
            Retroceso();
        }
        else if (coll.CompareTag("Weapon") && healthBoss.currentHealth > 0 && coll.gameObject.GetComponent<DamageSubWeapon>())
        {
            colPosX = coll.transform.position.x;
            Retroceso();
        }
    }

    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(0.3f);

        if (healthBoss.currentHealth > 0)
        {
            enemyController.canMove = true;
            enemyController.anim.SetBool("Hurting", false);
        }
    }
}
