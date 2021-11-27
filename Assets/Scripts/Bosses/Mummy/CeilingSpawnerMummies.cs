using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSpawnerMummies : MonoBehaviour
{
    public Transform spawnA;
    public Transform spawnB;

    public GameObject mummyAPrefab;
    public GameObject mummyBPrefab;
    GameObject bossSpawnedA;
    GameObject bossSpawnedB;
    public bool mummyAspawned;
    public bool mummyBspawned;

    public bool isDeadA;
    public bool isDeadB;

    public BoxCollider2D collActivator;
    public LayerMask layerPlayer;

    public bool Activator;

    public bool floorBroken;
    public Rigidbody2D[] breakeableFloor;

    // Start is called before the first frame update

    private void Start()
    {
        if (isDeadA && isDeadB)
        {
            collActivator.enabled = false;
        }
        else
        {
            collActivator.enabled = true;
        }

        
    }

    private void Update()
    {
        CheckActivator();
        ManagerControl();
    }

    void CheckActivator()
    {
        Activator = Physics2D.IsTouchingLayers(collActivator, layerPlayer);

        if (Activator)
        {
            collActivator.enabled = false;
            StartCoroutine(StartMomies());
        }
    }

    IEnumerator StartMomies()
    {
        if (!isDeadA || !isDeadB)
        {
            yield return new WaitForSeconds(2.6f);
            bossSpawnedA = Instantiate(mummyAPrefab, spawnA.position, Quaternion.identity);
            bossSpawnedA.name = mummyAPrefab.name;
            mummyAspawned = true;

            bossSpawnedB = Instantiate(mummyBPrefab, spawnB.position, Quaternion.identity);
            bossSpawnedB.name = mummyBPrefab.name;
            mummyBspawned = true;
        }
    }

    void ManagerControl()
    {
        if (isDeadA)
        {
            mummyAspawned = true;
            bossSpawnedA = null;
        }
        if (isDeadB)
        {
            mummyBspawned = true;
            bossSpawnedB = null;
        }

        if (isDeadA && isDeadB)
        {
            StartCoroutine(Sismo());

            if (floorBroken)
            {
                //ambas momias mueren, activar ruptura del piso
                for (int i = 0; i < breakeableFloor.Length; i++)
                {
                    //corregir altura de collider ya que no permite caminar
                    //y agregar una animacion de ruptura de piso mas creible, de momento cae todo el piso a la vez
                    breakeableFloor[i].bodyType = RigidbodyType2D.Dynamic;
                }
            }
            
            //esta estructura tambien debe ir registrada en el structure manager para el save y load
            //tambien poner en el event manager como la estructura de piso se cae, esto nos da oportunidad para crear nuevas zonas en el mapa
            //con la misma excusa de que se rompieron las paredes al derrotar a las momias
        }
    }

    IEnumerator Sismo()
    {
        yield return new WaitForSeconds(2);
        floorBroken = true;
    }
}