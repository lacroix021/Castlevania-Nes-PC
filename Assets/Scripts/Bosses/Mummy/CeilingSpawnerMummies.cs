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

    public GameObject platformBreakable;

    SimonController playerController;
    BossMapManager bossManager;

    [Header("COSAS DE LA TRAMPA")]
    public bool interruptorOn;
    public bool insideInterruptor;
    public Collider2D colInterruptor;
    //se usa tambien el layerplayer
    public Animator animSwitch;

    public bool wallBroken;
    public Rigidbody2D[] wallBreakable;

    // Start is called before the first frame update

    private void Start()
    {
        bossManager = GameManager.gameManager.GetComponentInChildren<BossMapManager>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();

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
        SwitchCheck();

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
            GameObject.Find("Stage3Music").GetComponent<ActivateMusic>().battle = true;
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
            bossManager.CheckBoss();

            GameObject.Find("Stage3Music").GetComponent<ActivateMusic>().battle = false;    // para desactivar la musica de batalla

            //aqui poner que la pared se rompe...
            wallBroken = true;

            if (wallBroken)
            {
                wallBreakable[0].bodyType = RigidbodyType2D.Dynamic;
                wallBreakable[1].bodyType = RigidbodyType2D.Dynamic;
                wallBreakable[2].bodyType = RigidbodyType2D.Dynamic;
            }

            if (interruptorOn)
            {
                //ambas momias mueren, activar ruptura del piso
                StartCoroutine(FallFloor());

                platformBreakable.SetActive(false);
            }
            
            //esta estructura tambien debe ir registrada en el structure manager para el save y load
            //tambien poner en el event manager como la estructura de piso se cae, esto nos da oportunidad para crear nuevas zonas en el mapa
            //con la misma excusa de que se rompieron las paredes al derrotar a las momias
        }
    }

    IEnumerator FallFloor()
    {
        breakeableFloor[8].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[9].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[7].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[10].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[6].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[11].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[5].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[12].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[4].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[13].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[3].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[14].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[2].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[15].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[1].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[16].bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        breakeableFloor[0].bodyType = RigidbodyType2D.Dynamic;
        breakeableFloor[17].bodyType = RigidbodyType2D.Dynamic;
    }

    IEnumerator Sismo()
    {
        yield return new WaitForSeconds(2);
        interruptorOn = true;
    }

    void SwitchCheck()
    {
        insideInterruptor = Physics2D.IsTouchingLayers(colInterruptor, layerPlayer);

        if(insideInterruptor && playerController.activating)
        {
            animSwitch.SetBool("SwitchOn", true);
            StartCoroutine(Sismo());
        }
    }
}