using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float speedAxeX = 25;
    private float speedAxeY;
    public float rotationSpeed = 10;
    private float z;
    private float dirAxe;
    private float scaleAxe;
    
    SubWeaponSystem weaponSys;
    Rigidbody2D rb;
    DatosJugador datosJugador;

    // Start is called before the first frame update
    void Start()
    {
        datosJugador = GameManager.gameManager.GetComponent<DatosJugador>();
        //si hay multiplicador por 2 el hacha es mas grande y su rango es variable
        if (datosJugador.multiplierPow == 2)
        {
            scaleAxe = 1.5f;
            speedAxeY = Random.Range(130, 200);
        }
        else
        {
            scaleAxe = 1;
            speedAxeY = 150;
        }

        rb = GetComponent<Rigidbody2D>();
        weaponSys = GameObject.FindGameObjectWithTag("Player").GetComponent<SubWeaponSystem>();
        dirAxe = weaponSys.direction;
        transform.localScale = new Vector3(dirAxe * scaleAxe, - 1 * scaleAxe, 1);

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
