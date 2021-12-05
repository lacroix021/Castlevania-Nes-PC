using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrossController : MonoBehaviour
{
    public float speedCrossX = 15;
    public float rotationSpeed = 800;
    private float z;
    private float dirAxe;
    private float v;
    
    
    SubWeaponSystem weaponSys;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();        
        dirAxe = weaponSys.direction;
        transform.localScale = new Vector3(dirAxe, -1, 1);

        ImpulseCross();
    }

    public void VerticalCross(InputAction.CallbackContext context)
    {
        v = context.ReadValue<Vector2>().y;
    }

    private void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, v * (speedCrossX/9) * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        z += Time.deltaTime * rotationSpeed;
        if (dirAxe == 1)
            transform.localRotation = Quaternion.Euler(0, 0, z);
        else if (dirAxe == -1)
            transform.localRotation = Quaternion.Euler(0, 0, -z);
        StartCoroutine(ReturnCross());
    }

    void ImpulseCross()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2((dirAxe * -1) * speedCrossX * Time.fixedDeltaTime, rb.velocity.y), ForceMode2D.Impulse);
    }

    IEnumerator ReturnCross()
    {
        yield return new WaitForSeconds(0.3f);
        //rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(dirAxe * 1.5f * Time.fixedDeltaTime, rb.velocity.y), ForceMode2D.Impulse);
    }
}
