using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaStatueController : MonoBehaviour
{

    public BoxCollider2D collActivator;
    public LayerMask layerPlayer;
    bool activator;
    public bool medusaSpawned;
    public bool medusaDefeated;

    public GameObject medusaBossPrefab;
    GameObject medusaInstance;

    public Transform spawnPos;
    public Animator anim;

    public GameObject particles;

    HealthBoss medusaHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (medusaDefeated)
        {
            collActivator.enabled = false;
        }
        else
        {
            collActivator.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckActivator();
        ManagerControl();
    }

    void CheckActivator()
    {
        activator = Physics2D.IsTouchingLayers(collActivator, layerPlayer);

        if (activator)
        {
            collActivator.enabled = false;
            StartCoroutine(WithoutHead());
        }
    }

    IEnumerator WithoutHead()
    {
        if (!medusaDefeated)
        {
            yield return new WaitForSeconds(2.6f);
            particles.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("Spawned", true);
            medusaInstance = Instantiate(medusaBossPrefab, spawnPos.position, Quaternion.identity);
            medusaInstance.name = medusaBossPrefab.name;
            medusaSpawned = true;
        }
    }

    void ManagerControl()
    {
        if (medusaDefeated)
        {
            anim.SetBool("Spawned", true);
            medusaSpawned = true;
            medusaInstance = null;
        }
    }
}
