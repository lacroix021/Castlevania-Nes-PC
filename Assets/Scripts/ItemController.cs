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
    TypeWhip typeWhip;
    GameManager gManager;
    HealthPlayer pHealth;
    SubWeaponSystem subWeaponSys;
    

    // Start is called before the first frame update
    void Start()
    {
        hSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HeartsSystem>();
        typeWhip = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TypeWhip>();
        gManager = GameManager.gameManager;

        if (TypeItem == tipeItem.lifeMax || TypeItem == tipeItem.porkChop)
            pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();

        if (TypeItem == tipeItem.subKnife || TypeItem == tipeItem.subAxe || TypeItem == tipeItem.subHolyWater || TypeItem == tipeItem.subCross || TypeItem == tipeItem.x2Pow || TypeItem == tipeItem.x3Pow)
            subWeaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();
    }

   
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if (TypeItem == tipeItem.bigHeart)
            {
                gManager.audioSource.clip = gManager.SoundTouchHeart;
                gManager.audioSource.Play();
                gManager.audioSource.loop = false;
                hSystem.currentHearts += 5;
                hSystem.CheckHearts();
            }
            else if (TypeItem == tipeItem.smallHeart)
            {
                gManager.audioSource.clip = gManager.SoundTouchHeart;
                gManager.audioSource.Play();
                gManager.audioSource.loop = false;
                hSystem.currentHearts += 1;
                hSystem.CheckHearts();
            }
            else if (TypeItem == tipeItem.whipItem)
            {
                ItemProperties();
                typeWhip.typeWhip += 1;
            }
            else if (TypeItem == tipeItem.goldRed)
            {
                GoldProperties();
                gManager.gold += goldRedV;
            }
            else if (TypeItem == tipeItem.goldPurple)
            {
                GoldProperties();
                gManager.gold += goldPurpleV;
            }
            else if (TypeItem == tipeItem.goldWhite)
            {
                GoldProperties();
                gManager.gold += goldWhiteV;
            }
            else if (TypeItem == tipeItem.chestGold)
            {
                GoldProperties();
                gManager.gold += chestGoldV;
            }
            else if (TypeItem == tipeItem.lifeMax)
            {
                gManager.audioSource.clip = gManager.soundGrabLifeMax;
                gManager.audioSource.Play();
                gManager.audioSource.loop = false;
                pHealth.maxHealth += healthMaxV;
                pHealth.vidaACurar = pHealth.maxHealth;
                pHealth.healing = true;
            }
            else if (TypeItem == tipeItem.porkChop)
            {
                gManager.audioSource.clip = gManager.soundGrabLifeMax;
                gManager.audioSource.Play();
                gManager.audioSource.loop = false;
                pHealth.vidaACurar = porkHealthV;
                pHealth.healing = true;
            }
            else if (TypeItem == tipeItem.subKnife)
            {
                ItemProperties();
                subWeaponSys.typeSub = 0;
                subWeaponSys.haveSub = true;
            }
            else if (TypeItem == tipeItem.subAxe)
            {
                ItemProperties();
                subWeaponSys.typeSub = 1;
                subWeaponSys.haveSub = true;
            }
            else if (TypeItem == tipeItem.subHolyWater)
            {
                ItemProperties();
                subWeaponSys.typeSub = 2;
                subWeaponSys.haveSub = true;
            }
            else if (TypeItem == tipeItem.subCross)
            {
                ItemProperties();
                subWeaponSys.typeSub = 3;
                subWeaponSys.haveSub = true;
            }
            else if(TypeItem == tipeItem.x2Pow)
            {
                ItemProperties();
                if (subWeaponSys.multiplierPow == 0)
                    subWeaponSys.multiplierPow = 1;
                else if (subWeaponSys.multiplierPow == 1)
                    subWeaponSys.multiplierPow = 2;
                else if (subWeaponSys.multiplierPow == 2)
                    subWeaponSys.multiplierPow = 2;
            }
            else if (TypeItem == tipeItem.x3Pow)
            {
                ItemProperties();
                if (subWeaponSys.multiplierPow == 0)
                    subWeaponSys.multiplierPow = 2;
                else if (subWeaponSys.multiplierPow == 1)
                    subWeaponSys.multiplierPow = 2;
                else if (subWeaponSys.multiplierPow == 2)
                    subWeaponSys.multiplierPow = 2;
            }

            Destroy(this.gameObject);
        }
    }

    void ItemProperties()
    {
        gManager.audioSource.clip = gManager.soundGrabItem;
        gManager.audioSource.Play();
        gManager.audioSource.loop = false;
    }

    void GoldProperties()
    {
        gManager.audioSource.clip = gManager.soundGrabGold;
        gManager.audioSource.Play();
        gManager.audioSource.loop = false;
    }
}
