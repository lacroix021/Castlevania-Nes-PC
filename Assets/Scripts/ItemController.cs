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
        x3Pow,
        shitake
    };

    public tipeItem TypeItem;
    

    [Header("ITEM VALUES")]
    public int goldRedV = 200;
    public int goldPurpleV = 400;
    public int goldWhiteV = 700;
    public int chestGoldV = 7000;
    public int healthMaxV = 5;
    public int porkHealthV = 10;
    public int shitakeHealthV = 10;
    

    HeartsSystem hSystem;
    
    GameManager gManager;
    DatosJugador datosJugador;
    HealthPlayer pHealth;

    public bool touched;

    public Collider2D collision;
    public LayerMask layerPlayer;

    // Start is called before the first frame update
    void Start()
    {
        hSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HeartsSystem>();
        gManager = GameManager.gameManager;
        datosJugador = gManager.GetComponent<DatosJugador>();

        if (TypeItem == tipeItem.lifeMax || TypeItem == tipeItem.porkChop || TypeItem == tipeItem.shitake)
            pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
    }

    private void Update()
    {
        CollisionDetector();
    }

    void CollisionDetector()
    {
        touched = Physics2D.IsTouchingLayers(collision, layerPlayer);

        if (touched)
        {
            switch (TypeItem)
            {
                case tipeItem.bigHeart:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabHearts);
                    datosJugador.currentHearts += 5;
                    hSystem.CheckHearts();
                    break;
                case tipeItem.smallHeart:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabHearts);
                    datosJugador.currentHearts += 1;
                    hSystem.CheckHearts();
                    break;
                case tipeItem.whipItem:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    datosJugador.typeWhip += 1;
                    break;
                case tipeItem.goldRed:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabGold);
                    datosJugador.gold += goldRedV;
                    break;
                case tipeItem.goldPurple:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabGold);
                    datosJugador.gold += goldPurpleV;
                    break;
                case tipeItem.goldWhite:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabGold);
                    datosJugador.gold += goldWhiteV;
                    break;
                case tipeItem.chestGold:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabGold);
                    datosJugador.gold += chestGoldV;
                    break;
                case tipeItem.lifeMax:
                    AudioManager.instance.PlayAudio(AudioManager.instance.lifeMax);
                    datosJugador.maxHealth += healthMaxV;
                    pHealth.vidaACurar = datosJugador.maxHealth;
                    pHealth.healing = true;
                    break;
                case tipeItem.porkChop:
                    AudioManager.instance.PlayAudio(AudioManager.instance.lifeMax);
                    pHealth.vidaACurar = porkHealthV;
                    pHealth.healing = true;
                    break;
                case tipeItem.shitake:
                    AudioManager.instance.PlayAudio(AudioManager.instance.lifeMax);
                    pHealth.vidaACurar = shitakeHealthV;
                    pHealth.healing = true;
                    break;
                case tipeItem.subKnife:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    datosJugador.typeSub = 0;
                    datosJugador.haveSub = true;
                    break;
                case tipeItem.subAxe:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    datosJugador.typeSub = 1;
                    datosJugador.haveSub = true;
                    break;
                case tipeItem.subHolyWater:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    datosJugador.typeSub = 2;
                    datosJugador.haveSub = true;
                    break;
                case tipeItem.subCross:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    datosJugador.typeSub = 3;
                    datosJugador.haveSub = true;
                    break;
                case tipeItem.x2Pow:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    if (datosJugador.multiplierPow == 0)
                        datosJugador.multiplierPow = 1;
                    else if (datosJugador.multiplierPow == 1)
                        datosJugador.multiplierPow = 2;
                    else if (datosJugador.multiplierPow == 2)
                        datosJugador.multiplierPow = 2;
                    break;
                case tipeItem.x3Pow:
                    AudioManager.instance.PlayAudio(AudioManager.instance.grabItem);
                    if (datosJugador.multiplierPow == 0)
                        datosJugador.multiplierPow = 2;
                    else if (datosJugador.multiplierPow == 1)
                        datosJugador.multiplierPow = 2;
                    else if (datosJugador.multiplierPow == 2)
                        datosJugador.multiplierPow = 2;
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
