using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerItem : MonoBehaviour
{
    public GameObject EffectDestroy;
    private GameObject EffectDestroyInstance;
    public Transform placeDrop;

    private GameObject itemInstanced;

    GameManager gManager;
    LootContainer lootCont;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.gameManager;
        lootCont = GetComponentInParent<LootContainer>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Weapon"))
        {
            int lootRarity = Random.Range(0, 101);

            if(gManager.currentTypeWhip != 2)
            {
                //si no tiene vampirekiller, dropeara latigo, si ya lo tiene dropeara corazonGrande
                itemInstanced = Instantiate(lootCont.loot[0], placeDrop.position, Quaternion.identity);
            }
            else
            {
                if(lootRarity >= 0 && lootRarity <= 50)
                {
                    //50%
                    itemInstanced = Instantiate(lootCont.loot[1], placeDrop.position, Quaternion.identity);
                }
                else if(lootRarity > 50 && lootRarity <= 75)
                {
                    //25%
                    itemInstanced = Instantiate(lootCont.loot[2], placeDrop.position, Quaternion.identity);
                }
                else if (lootRarity > 75 && lootRarity <= 92)
                {
                    //17%
                    itemInstanced = Instantiate(lootCont.loot[3], placeDrop.position, Quaternion.identity);
                }
                else if (lootRarity > 92 && lootRarity <= 100)
                {
                    //8%
                    itemInstanced = Instantiate(lootCont.loot[4], placeDrop.position, Quaternion.identity);
                }
            }

            EffectDestroyInstance = Instantiate(EffectDestroy, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
