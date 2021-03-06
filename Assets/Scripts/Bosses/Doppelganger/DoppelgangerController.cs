using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoppelgangerController : MonoBehaviour
{
    [Header("VARIABLES MODIFICABLES")]
    public float radiusPlayer;
    public float radiusMeleeAttack;
    public float radiusDistanceAttack;
    public LayerMask layerPlayer;
    public float moveSpeed;
    private float moveSpeedBase;
    private float moveSlopeSpeed;
    public float jumpForce;
    public float slideForce;
    public LayerMask thisGround;
    public float limitY;
    public float slopeFriction = 0.01f;

    float nextAttackTime = 0f;
    public float attackRate = 2f;

    float nextSlideTime = 0f;
    public float slideRate = 2f;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    float nextJumpTime = 0f;
    public float jumpRate = 0.5f;

    float jumpOrSlideTime = 0f;
    public float jumpOrSlideRate = 0.5f;

    [Header("VARIABLES INFORMATIVAS")]
    public float direction;
    public bool activating;
    public float h;
    public float v;
    
    [SerializeField] bool isJump;
    public bool isGrounded;
    public bool onSlope;
    [SerializeField] bool isAttack;
    public bool isCrouch;
    public bool isSlide;
    public bool canMove;
    public bool inStair;
    public bool climbing;
    public bool touchingCeiling;

    bool animAttack;
    bool animCrouchAttack;
    public bool animSlide;
    public bool animSub;
    public bool animSubCrouch;

    public bool playerInRange;
    public bool meleeAttackRange;
    public bool distanceAttackRange;
    public int jumpOrSlide;


    /*****************************/
    public Transform feetPos;
    public BoxCollider2D colliderFeet;
    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D ceilingCheck;
    public BoxCollider2D myCollider;
    public PhysicsMaterial2D slide;
    public PhysicsMaterial2D sticky;

    GameManager gameManager;

    //**//
    SimonController player;
    HealthBoss hBoss;

    //**//
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        moveSpeedBase = moveSpeed;
        moveSlopeSpeed = moveSpeed / 1.3f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();
        hBoss = GetComponent<HealthBoss>();

    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        Animations();
        RangeCheckers();
        Attacking();
        ClimbingChain();
        InputControllers();

        animAttack = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        animCrouchAttack = anim.GetCurrentAnimatorStateInfo(0).IsTag("CrouchAttack");
        animSlide = anim.GetCurrentAnimatorStateInfo(0).IsTag("Slide");
        animSub = anim.GetCurrentAnimatorStateInfo(0).IsTag("SubWeapon");
        animSubCrouch = anim.GetCurrentAnimatorStateInfo(0).IsTag("SubWeaponCrouch");


        //cambia la velocidad tan pronto entramos en una rampa
        if (onSlope)
        {
            moveSpeed = moveSlopeSpeed;
        }
        else
        {
            moveSpeed = moveSpeedBase;
        }
    }

    private void FixedUpdate()
    {
        Movement();
        JumpControll();
        Slide();
        NormalizeSlope();
        CheckGround();

        //cambio de material de fisicas
        if (rb.velocity.x != 0 && !animSub)
        {
            if (onSlope)
                myCollider.sharedMaterial = null;
            else
                myCollider.sharedMaterial = slide;
        }
        else
        {
            myCollider.sharedMaterial = sticky;
        }
    }

    void InputControllers()
    {
        //ataque basico
        if (meleeAttackRange && !isSlide && canMove)
        {
            if (Time.time >= nextAttackTime)
            {
                isAttack = true;
                AudioManager.instance.PlayAudio(AudioManager.instance.attack);

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //jugador en rango movimiento de persecusion
        if (playerInRange)
        {
            if(player.transform.position.x > transform.position.x +0.3f && !isSlide && canMove)
            {
                h = 1;
            }
            else if(player.transform.position.x < transform.position.x -0.3f && !isSlide && canMove)
            {
                h = -1;
            }

            if (!player.isGrounded && isGrounded && canMove)
            {
                StartCoroutine(TimerBool());
            }
        }
        else
        {
            h = 0;
        }
    }

    IEnumerator TimerBool()
    {
        //este booleano debe desactivarse en dicha funcion donde se use
        yield return new WaitForSeconds(0.4f);
        if(Time.time >= jumpOrSlideTime)
        {
            jumpOrSlide = Random.Range(0, 5);
            jumpOrSlideTime = Time.time + 1 / jumpOrSlideRate;
        }

        if (jumpOrSlide != 0)
        {
            if(Time.time >= nextJumpTime)
            {
                isJump = true;
                nextJumpTime = Time.time + 1 / jumpRate;
            }
        }
        else
        {
            if (Time.time >= nextSlideTime && isGrounded && !animSub)
            {
                isSlide = true;
                AudioManager.instance.PlayAudio(AudioManager.instance.slide);
                nextSlideTime = Time.time + 1f / slideRate;
            }
        }
    }
    void InputManager()
    {
        //agachado

        touchingCeiling = Physics2D.IsTouchingLayers(ceilingCheck, thisGround);

        if (v < -0.2f && isGrounded && !isSlide && canMove && !inStair)
        {
            isCrouch = true;
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        else if (v >= 0)
        {
            isCrouch = false;
        }


        //Flip sprite
        if (h > 0 && canMove)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h < 0 && canMove)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //direccion de subarmas
        if (transform.localScale.x == -1)
            direction = 1;
        else
            direction = -1;

    }

    void Movement()
    {
        if (!gameManager.gamePaused)
        {
            //detiene el deslizamiento si ibamos moviendonos y atacamos repentinamente
            if (animAttack && isGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            //movimiento normal
            if (!animAttack && !animCrouchAttack && !isCrouch && canMove && !animSub)
            {
                rb.velocity = new Vector2(h * moveSpeed * Time.deltaTime, rb.velocity.y);
            }
        }
        //limitador de velocidad de caida PROBAR EN SALTOS NORMALES YA QUE SE PROBO CON EL PRECIPICIO DE NIVEL 3
        if (rb.velocity.y <= -5 && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -4);
        }
    }

    void NormalizeSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, thisGround);
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.green);

        if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
        {
            onSlope = true;
            
            // Apply the opposite force against the slope force 
            // You will need to provide your own slopeFriction to stabalize movement
            rb.velocity = new Vector2(rb.velocity.x - (hit.normal.x * slopeFriction), rb.velocity.y);

        }
        else if (hit.collider != null && hit.normal.x < 0.1f)
        {
            onSlope = false;
        }
    }

    void Animations()
    {
        if (canMove)
        {
            anim.SetFloat("VelX", Mathf.Abs(h));
        }

        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Crouch", isCrouch);
        anim.SetBool("InStair", inStair);
        anim.SetFloat("VelY", rb.velocity.y);
        anim.SetFloat("V", v);
        anim.SetBool("ClimbRope", climbing);
        anim.SetBool("Sliding", isSlide);
    }

    void JumpControll()
    {
        if (isJump)
        {
            anim.SetTrigger("Jump");
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            isJump = false;
        }
    }

    void ClimbingChain()
    {
        if (climbing && activating)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, v * (moveSlopeSpeed - 10) * Time.deltaTime);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void Attacking()
    {
        if (isAttack)
        {
            anim.SetTrigger("Attack");

            isAttack = false;
        }
    }

    void Slide()
    {
        if (isSlide)
        {
            v = -1;
            rb.velocity = Vector2.zero;

            if (transform.localScale.x == -1)
                rb.AddForce(new Vector2(slideForce * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            else if (transform.localScale.x == 1)
                rb.AddForce(new Vector2(-slideForce * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            
            StartCoroutine(StopSlide());
        }
        else
        {
            v = 0;
        }
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(0.3f);
        //cuando tiene techo encima seguira deslizandose hasta que no tenga techo encima
        if (!touchingCeiling)
        {
            isSlide = false;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.IsTouchingLayers(colliderFeet, thisGround);
        

        //se inactiva el detector de piso cuando se salta y se activa nuevamente cuando va cayendo el jugador
        if (!isGrounded && rb.velocity.y > 0)
        {
            colliderFeet.gameObject.SetActive(false);
        }
        else if (rb.velocity.y <= 0f || onSlope)
        {
            colliderFeet.gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusPlayer);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusMeleeAttack);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusDistanceAttack);
    }

    void RangeCheckers()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, radiusPlayer, layerPlayer);     //rango para buscar al jugador
        meleeAttackRange = Physics2D.OverlapCircle(transform.position, radiusMeleeAttack, layerPlayer);  //rango para atacar melee al jugador

        if (!meleeAttackRange)
        {
            distanceAttackRange = Physics2D.OverlapCircle(transform.position, radiusDistanceAttack, layerPlayer);  //rango para atacar melee al jugador
        }
        else
        {
            distanceAttackRange = false;
        }

        if (!playerInRange)
        {
            if(hBoss.currentHealth < hBoss.maxHealth)
                hBoss.currentHealth += 1 * Time.deltaTime;
        }
        if(playerInRange && hBoss.currentHealth > 0)
        {
            GameObject.Find("StageExtraA").GetComponent<ActivateMusic>().battle = true;
        }
    }
}
