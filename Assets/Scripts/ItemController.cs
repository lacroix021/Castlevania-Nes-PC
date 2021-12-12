using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public enum tipeItem
    {
        bigHeart,
        smallHeart,
        whipItem,
        subKnife,
        subAxe,
        subHolyWater,
        subCross,
        goldRed,
        goldPurple,
        goldWhite,
        chestGold,
        lifeMax,
        porkChop,
        x2Pow,
        x3Pow
    };

    public tipeItem TypeItem;
    

    [Header("ITEM VALUES")]
    public int goldRedV = 200;
    public int goldPurpleV = 400;
    public int goldWhiteV = 700;
    public int chestGoldV = 7000;
    public int healthMaxV = 5;
    public int porkHealthV = 10;
    

    HeartsSystem hSystem;
    
    GameManager gManager;
    DatosJugador datosJugador;
    HealthPlayer pHealth;
    
    

    // Start is called before the first frame update
    void Start()
    {
        hSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HeartsSystem>();
        gManager = GameManager.gameManager;
        datosJugador = gManager.GetComponent<DatosJugador>();

        if (TypeItem == tipeItem.lifeMax || TypeItem == tipeItem.porkChop)
            pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
    }

   
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if (TypeItem == tipeItem.bigHeart)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.grabHearts);
                datosJugador.currentHearts += 5;
                hSystem.CheckHearts();
            }
            else if (TypeItem == tipeItem.smallHeart)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.grabHearts);
                datosJugador.currentHearts += 1;
                hSystem.CheckHearts();
            }
            else if (TypeItem == tipeItem.whipItem)
            {
                ItemProperties();
                datosJugador.typeWhip += 1;
            }
            else if (TypeItem == tipeItem.goldRed)
            {
                GoldProperties();
                datosJugador.gold += goldRedV;
            }
            else if (TypeItem == tipeItem.goldPurple)
            {
                GoldProperties();
                datosJugador.gold += goldPurpleV;
            }
            else if (TypeItem == tipeItem.goldWhite)
            {
                GoldProperties();
                datosJugador.gold += goldWhiteV;
            }
            else if (TypeItem == tipeItem.chestGold)
            {
                GoldProperties();
                datosJugador.gold += chestGoldV;
            }
            else if (TypeItem == tipeItem.lifeMax)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.lifeMax);
                datosJugador.maxHealth += healthMaxV;
                pHealth.vidaACurar = datosJugador.maxHealth;
                pHealth.healing = true;
            }
            else if (TypeItem == tipeItem.porkChop)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.lifeMax);
                pHealth.vidaACurar = porkHealthV;
                pHealth.healing = true;
            }
            else if (TypeItem == tipeItem.subKnife)
            {
                ItemProperties();
                datosJugador.typeSub = 0;
                datosJugador.haveSub = true;
            }
            else if (TypeItem == tipeItem.subAxe)
            {
                ItemProperties();
                datosJugador.typeSub = 1;
                datosJugador.haveSub = true;
            }
            else if (TypeItem == tipeItem.subHolyWater)
            {
                ItemProperties();
                datosJugador.typeSub = 2;
                datosJugador.haveSub = true;
            }
            else if (TypeItem == tipeItem.subCross)
            {
                ItemProperties();
                datosJugador.typeSub = 3;
                datosJugador.haveSub = true;
            }
            else if(TypeItem == tipeItem.x2Pow)
            {
                ItemProperties();
                if (datosJugador.multiplierPow == 0)
                    datosJugador.multiplierPow = 1;
                else if (datosJugador.multiplierPow == 1)
                    datosJugador.multiplierPow = 2;
                else if (datosJugador.multiplierPow == 2)
                    datosJugador.multiplierPow = 2;
            }
            else if (TypeItem == tipeItem.x3Pow)
            {
                ItemProperties();
                if (datosJugador.multiplierPow == 0)
                    datosJugador.multiplierPow = 2;
                else if (datosJugador.multiplierPow == 1)
                    datosJugador.multiplierPow = 2;
                else if (datosJugador.multiplierPow == 2)
                    datosJugador.multiplierPow = 2;
            }

            Destroy(this.gameObject);
        }
    }

    void ItemProperties()
    {
        AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
    }

    void GoldProperties()
    {
        AudioManager.instance.PlayAudio(AudioManager.instance.grabGold);
    }
}
