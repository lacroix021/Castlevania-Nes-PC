using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionSpawner : MonoBehaviour
{
    public Collider2D activatorCol;
    public LayerMask layerPlayer;
    public Transform spotSpawn;

    public GameObject LegionPrefab;
    GameObject legionBoss;

    public bool active;
    public bool defeated;

    public ParticleSystem particleA;
    public ParticleSystem particleB;

    public Transform boundaryFather;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        active = Physics2D.IsTouchingLayers(activatorCol, layerPlayer);
        
        if(active && !legionBoss && !defeated)
        {
            GameObject.Find("StageExtraB").GetComponent<ActivateMusic>().battle = true;
            legionBoss = Instantiate(LegionPrefab, spotSpawn.position, Quaternion.identity);
            legionBoss.transform.parent = boundaryFather.transform;
            particleA.Play();
            particleB.Play();
        }

        if (defeated)
        {
            GameObject.Find("StageExtraB").GetComponent<ActivateMusic>().battle = false;
        }

        //cuando el activador sea true, activar los particle system y spawnear al prefab del boss, y este subira a su posicion establecida y empezara la pelea
        //tambien activar la musica de batalla
        //sincronizar esto con el bossmanager para que guarde el estado cuando el boss muera y no lo spawnee mas veces
        //desactivar el activator una vez spawneado el boss para que no spawnee demas
    }
}
