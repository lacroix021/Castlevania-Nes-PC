using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonController : MonoBehaviour
{
    [Header("VARIABLES MODIFICABLES")]

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


    [Header("VARIABLES INFORMATIVAS")]
    [SerializeField] float h;
    public float v;
    [SerializeField] bool isJump;
    public bool isGrounded;
    public bool onSlope;
    [SerializeField] bool isAttack;
    public bool isCrouch;
    [SerializeField] bool isSlide;
    public bool canMove;
    public bool inStair;
    public bool climbing;
    public bool touchingCeiling;

    bool animAttack;
    bool animCrouchAttack;
    public bool animSlide;
    public bool animSub;

    /*****************************/
    public Transform feetPos;
    public BoxCollider2D colliderFeet;
    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D ceilingCheck;

    GameManager gameManager;

    SoundsSimon soundSimon;
    HealthPlayer healthPlayer;


    public static SimonController instance;

    private void Awake()
    {
        if(SimonController.instance == null)
        {
            SimonController.instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        soundSimon = GetComponent<SoundsSimon>();
        moveSpeedBase = moveSpeed;
        moveSlopeSpeed = moveSpeed / 1.3f;
        healthPlayer = GetComponent<HealthPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //DESCOMENTAREAR ESTO DESPUES DE HACER PRUEBAS
        //Y COMENTAREAR EL Inputmanager() QUE ESTA SOLO EN EL UPDATE

        
        if (!gameManager.GamePaused)
        {
            InputManager();
        }
        
        
        //InputManager();

        Animations();
        CheckGround();
        Attack();

        animAttack = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        animCrouchAttack = anim.GetCurrentAnimatorStateInfo(0).IsTag("CrouchAttack");
        animSlide = anim.GetCurrentAnimatorStateInfo(0).IsTag("Slide");
        animSub = anim.GetCurrentAnimatorStateInfo(0).IsTag("SubWeapon");
        
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
        Jump();
        Slide();
        NormalizeSlope();
        ClimbingChain();
    }

    void InputManager()
    {
        if (canMove)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            h = 0;
            v = 0;
        }
        

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouch && canMove)
        {
            isJump = true;
        }
        //Ataque
        if (Input.GetButtonDown("Fire1") && !animSlide && canMove && !climbing && !animSub)
        {
            if (Time.time >= nextAttackTime)
            {
                isAttack = true;
                soundSimon.audioWhip.clip = soundSimon.noHit;
                soundSimon.audioWhip.Play();
                soundSimon.audioWhip.loop = false;

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //agachado

        touchingCeiling = Physics2D.IsTouchingLayers(ceilingCheck, thisGround);

        
        

        if (v < 0 && isGrounded && !isSlide && canMove && !inStair)
        {
            isCrouch = true;
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && !animCrouchAttack && !animAttack && !animSub && !healthPlayer.isInvulnerable)
            {
                if (Time.time >= nextSlideTime)
                {
                    isSlide = true;
                    nextSlideTime = Time.time + 1f / slideRate;
                }
            }
        }
        else if ( v >= 0)
        {
            isCrouch = false;
        }

        //Flip sprite
        if(h > 0 && canMove)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(h < 0 && canMove)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Movement()
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

    void NormalizeSlope()
    {
        // Attempt vertical normalization
        if (isGrounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, thisGround);
            Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.green);
            
            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                // Apply the opposite force against the slope force 
                // You will need to provide your own slopeFriction to stabalize movement
                rb.velocity = new Vector2(rb.velocity.x - (hit.normal.x * slopeFriction), rb.velocity.y);

            }
            else if(hit.collider != null && hit.normal.x < 0.1f)
            {
                onSlope = false;
            }
        }
    }

    void Animations()
    {
        anim.SetFloat("VelX", Mathf.Abs(h));
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Crouch", isCrouch);
        anim.SetBool("InStair", inStair);
        anim.SetFloat("VelY", rb.velocity.y);
        anim.SetFloat("V", v);
        anim.SetBool("ClimbRope", climbing);
        anim.SetBool("Sliding", isSlide);
    }

    void Jump()
    {
        if (isJump)
        {
            anim.SetTrigger("Jump");
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            isJump = false;
        }

        //salto graduable de cuatro lineas
        if (rb.velocity.y < 0 && !isGrounded)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump") && !isGrounded)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void ClimbingChain()
    {
        if (climbing)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, v * (moveSlopeSpeed -10) * Time.deltaTime);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void Attack()
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
            if (!healthPlayer.isInvulnerable)
            {
                rb.velocity = Vector2.zero;
                if (transform.localScale.x == -1)
                    rb.AddForce(new Vector2(slideForce * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
                else if (transform.localScale.x == 1)
                    rb.AddForce(new Vector2(-slideForce * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
                StartCoroutine(StopSlide());
            }
            else
            {
                //correccion de bug para que no deslice mientras sufre daño
                rb.velocity = Vector2.zero;
            }
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
        if(!isGrounded && rb.velocity.y > 0)
        {
            colliderFeet.enabled = false;
        }
        else if(!isGrounded && rb.velocity.y < 0f)
        {
            colliderFeet.enabled = true;
        }
    }
}
