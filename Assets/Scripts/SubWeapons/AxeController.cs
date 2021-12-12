using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float speedAxeX = 25;
    public float speedAxeY = 70;
    public float rotationSpeed = 10;
    private float z;
    private float dirAxe;
    
    SubWeaponSystem weaponSys;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();
        dirAxe = weaponSys.direction;
        transform.localScale = new Vector3(dirAxe, -1, 1);

        ImpulseAxe();
        Destroy(this.gameObject, 1);
    }

    private void FixedUpdate()
    {
        z += Time.deltaTime * rotationSpeed;
        if(dirAxe == 1)
            transform.localRotation = Quaternion.Euler(0, 0, z);
        else if(dirAxe == -1)
            transform.localRotation = Quaternion.Euler(0, 0, -z);
    }

    void ImpulseAxe()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2((dirAxe * -1) * speedAxeX * Time.fixedDeltaTime, speedAxeY * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }
}
