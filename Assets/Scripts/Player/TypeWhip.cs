using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWhip : MonoBehaviour
{
    //public int typeWhip;
    public float DamageA;
    public float DamageB;
    public float DamageC;

    [Header("OBJETOS DEL LATIGO")]
    public SpriteRenderer whipStand;
    public SpriteRenderer whip2Stand;
    public SpriteRenderer whip3Stand;

    public SpriteRenderer whipCrouch;
    public SpriteRenderer whip2Crouch;
    public SpriteRenderer whip3Crouch;

    [Header("SPRITES LATIGO")]
    public Sprite whip1FrameA;
    public Sprite whip1FrameB;
    public Sprite whip1FrameC;

    public Sprite whip2FrameA;
    public Sprite whip2FrameB;
    public Sprite whip2FrameC;
    
    public Sprite whip3FrameC;



    [Header("COMPONENTES")]
    public DamagePlayer damageStand;
    public DamagePlayer damageCrouch;
    public BoxCollider2D colliderStand;
    public BoxCollider2D colliderCrouch;
    public Animator animWhipStand;
    public Animator animWhipCrouch;
    public Animator animWhipStandB;
    public Animator animWhipCrouchB;
    public Animator animWhipStandA;
    public Animator animWhipCrouchA;

    DatosJugador datosJugador;

    private void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeWhip();
    }

    void ChangeWhip()
    {
        //limite de latigo para que no sume mas de 2
        datosJugador.typeWhip = Mathf.Clamp(datosJugador.typeWhip, 0, 2);

        switch (datosJugador.typeWhip)
        {
            case 0:
                //stand
                animWhipStand.enabled = false;
                animWhipStandB.enabled = false;
                animWhipStandA.enabled = false;
                colliderStand.size = new Vector2(0.24f, 0.05012715f);
                colliderStand.offset = new Vector2(-0.25f, 0.2124948f);
                //damageStand.damage = DamageA;
                whipStand.sprite = whip1FrameA;
                whip2Stand.sprite = whip1FrameB;
                whip3Stand.sprite = whip1FrameC;
                whip3Stand.transform.localPosition = new Vector2(-0.249f, 0.173f);
                //crouch
                animWhipCrouch.enabled = false;
                animWhipCrouchB.enabled = false;
                animWhipCrouchA.enabled = false;
                colliderCrouch.size = new Vector2(0.2595174f, 0.1309329f);
                colliderCrouch.offset = new Vector2(-0.2397587f, 0.1179131f);
                //damageCrouch.damage = DamageA;
                whipCrouch.sprite = whip1FrameA;
                whip2Crouch.sprite = whip1FrameB;
                whip3Crouch.sprite = whip1FrameC;
                whip3Crouch.transform.localPosition = new Vector2(-0.249f, 0.108f);
                break;

            case 1:
                //stand
                animWhipStand.enabled = false;
                animWhipStandB.enabled = false;
                animWhipStandA.enabled = false;
                colliderStand.size = new Vector2(0.24f, 0.05012715f);
                colliderStand.offset = new Vector2(-0.25f, 0.2124948f);
                //damageStand.damage = DamageB;
                whipStand.sprite = whip2FrameA;
                whip2Stand.sprite = whip2FrameB;
                whip3Stand.sprite = whip2FrameC;
                whip3Stand.transform.localPosition = new Vector2(-0.249f, 0.173f);
                //crouch
                animWhipCrouch.enabled = false;
                animWhipCrouchB.enabled = false;
                animWhipCrouchA.enabled = false;
                colliderCrouch.size = new Vector2(0.2498835f, 0.1309329f);
                colliderCrouch.offset = new Vector2(-0.2349418f, 0.1179131f);
                //damageCrouch.damage = DamageB;
                whipCrouch.sprite = whip2FrameA;
                whip2Crouch.sprite = whip2FrameB;
                whip3Crouch.sprite = whip2FrameC;
                whip3Crouch.transform.localPosition = new Vector2(-0.24f, 0.108f);
                break;

            case 2:
                //stand
                animWhipStand.enabled = true;
                animWhipStandB.enabled = true;
                animWhipStandA.enabled = true;
                colliderStand.size = new Vector2(0.3791183f, 0.09986368f);
                colliderStand.offset = new Vector2(-0.3195592f, 0.2f);
                //damageStand.damage = DamageC;
                whipStand.sprite = whip2FrameA;
                whip2Stand.sprite = whip2FrameB;
                whip3Stand.sprite = whip3FrameC;
                whip3Stand.transform.localPosition = new Vector2(-0.3075f, 0.173f);
                //crouch
                animWhipCrouch.enabled = true;
                animWhipCrouchB.enabled = true;
                animWhipCrouchA.enabled = true;
                colliderCrouch.size = new Vector2(0.4046696f, 0.1391528f);
                colliderCrouch.offset = new Vector2(-0.3123348f, 0.09631589f);
                //damageCrouch.damage = DamageC;
                whipCrouch.sprite = whip2FrameA;
                whip2Crouch.sprite = whip2FrameB;
                whip3Crouch.sprite = whip3FrameC;
                whip3Crouch.transform.localPosition = new Vector2(-0.316f, 0.108f);
                break;
        }
    }
}
