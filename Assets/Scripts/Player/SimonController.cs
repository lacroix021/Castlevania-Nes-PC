using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    public bool activating;
    public float h;
    public float v;
    public float hRStick;
    public float vRStick;
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

    public static SimonController instance;

    public bool pauseGame;
    public bool cancelInput;

    private void Awake()
    {
        if (SimonController.instance == null)
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
        moveSpeedBase = moveSpeed;
        moveSlopeSpeed = moveSpeed / 1.3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gamePaused)
        {
            InputManager();
            Animations();
        }
        
        Attacking();
        ClimbingChain();

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

    //NEW INPUTS
        
    public void Move(InputAction.CallbackContext context)
    {
        h = context.ReadValue<Vector2>().x;
        v = context.ReadValue<Vector2>().y;
    }

    public void RightStick(InputAction.CallbackContext context)
    {
        hRStick = context.ReadValue<Vector2>().x;
        vRStick = context.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!gameManager.gamePaused)
        {
            if (context.performed && isGrounded && !isCrouch && canMove)
            {
                isJump = true;
            }
            //salto controlado
            if (context.canceled && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            if (isCrouch && isGrounded && !isSlide && canMove && !inStair)
            {
                //slide
                if (context.performed && !animCrouchAttack && !animAttack && !animSub && !animSubCrouch)
                {
                    if (Time.time >= nextSlideTime)
                    {
                        isSlide = true;
                        AudioManager.instance.PlayAudio(AudioManager.instance.slide);
                        nextSlideTime = Time.time + 1f / slideRate;
                    }
                }
            }
        }
    }

    public void ActiveMode(InputAction.CallbackContext context)
    {
        activating = context.performed;
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameManager.PauseGame();
        }
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        cancelInput = context.performed;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!gameManager.gamePaused)
        {
            if (context.performed && !animSlide && canMove && !climbing && !animSub)
            {
                if (Time.time >= nextAttackTime)
                {
                    isAttack = true;
                    AudioManager.instance.PlayAudio(AudioManager.instance.attack);

                    nextAttackTime = Time.time + 1f / attackRate;
                }
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
        if(rb.velocity.y <= -5 && !isGrounded)
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
            //colliderFeet.gameObject.SetActive(true);
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
            rb.velocity = new Vector2(rb.velocity.x, v * (moveSlopeSpeed -10) * Time.deltaTime);
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
            rb.velocity = Vector2.zero;
            
            if (transform.localScale.x == -1)
                rb.AddForce(new Vector2(slideForce * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            else if (transform.localScale.x == 1)
                rb.AddForce(new Vector2(-slideForce * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
            StartCoroutine(StopSlide());
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
            colliderFeet.gameObject.SetActive(false);
        }
        else if(rb.velocity.y <= 0f || onSlope)
        {
            colliderFeet.gameObject.SetActive(true);
        }
    }
}
