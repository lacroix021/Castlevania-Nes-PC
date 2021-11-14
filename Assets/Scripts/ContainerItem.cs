using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerItem : MonoBehaviour
{
    public GameObject EffectDestroy;
    
    public Transform placeDrop;
        
    DatosJugador datosJugador;

    LootContainer lootCont;
    

    // Start is called before the first frame update
    void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        lootCont = GetComponentInParent<LootContainer>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Weapon"))
        {
            int lootRarity = Random.Range(0, 101);

            if(datosJugador.typeWhip != 2)
            {
                //si no tiene vampirekiller, dropeara latigo, si ya lo tiene dropeara corazonGrande
                Instantiate(lootCont.loot[0], placeDrop.position, Quaternion.identity);
            }
            else
            {
                if(lootRarity >= 0 && lootRarity <= 50)
                {
                    //50%
                    Instantiate(lootCont.loot[1], placeDrop.position, Quaternion.identity);
                }
                else if(lootRarity > 50 && lootRarity <= 75)
                {
                    //25%
                    Instantiate(lootCont.loot[2], placeDrop.position, Quaternion.identity);
                }
                else if (lootRarity > 75 && lootRarity <= 92)
                {
                    //17%
                    Instantiate(lootCont.loot[3], placeDrop.position, Quaternion.identity);
                }
                else if (lootRarity > 92 && lootRarity <= 100)
                {
                    //8%
                    Instantiate(lootCont.loot[4], placeDrop.position, Quaternion.identity);
                }
            }

            Instantiate(EffectDestroy, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
