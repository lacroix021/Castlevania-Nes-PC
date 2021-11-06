using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<HealthPlayer>().currentHealth > 0)
        {
            collision.gameObject.GetComponent<HealthPlayer>().currentHealth = 0;
            collision.gameObject.GetComponent<HealthPlayer>().HealthCheck();
            collision.gameObject.GetComponent<SimonController>().canMove = false;
            collision.gameObject.GetComponent<SimonController>().rb.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(9, 10, true);
        }
    }
}
