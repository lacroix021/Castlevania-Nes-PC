using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesInScreen : MonoBehaviour
{
    public GameObject floatingObject;
    private GameObject valueInstanced;
    public Transform positionValue;

    ItemController iControl;

    public bool touched;
    public Collider2D collision;
    public LayerMask layerPlayer;

    private void Start()
    {
        iControl = GetComponent<ItemController>();
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
            //corregir aqui ya que con este metodo no se estan mostrando las particulas, hay que incluir esto en el script de los items
            //con una condicion dependiendo que item sea, lo que es oro y life max son los que emiten particulas o cuanto oro gano el jugador

            print("entro aqui ahoras");
            if (iControl.TypeItem == ItemController.tipeItem.lifeMax)
                valueInstanced = Instantiate(floatingObject, positionValue.position, Quaternion.Euler(-90, 0, 0));
            else
                valueInstanced = Instantiate(floatingObject, positionValue.position, Quaternion.identity);

        }
    }
}
