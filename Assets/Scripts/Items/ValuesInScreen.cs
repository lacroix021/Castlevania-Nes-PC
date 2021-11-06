using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesInScreen : MonoBehaviour
{
    public GameObject valueGold;
    private GameObject valueInstanced;
    public Transform positionValue;

    ItemController iControl;

    private void Start()
    {
        iControl = GetComponent<ItemController>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if(iControl.TypeItem == ItemController.tipeItem.lifeMax)
                valueInstanced = Instantiate(valueGold, positionValue.position, Quaternion.Euler(-90, 0, 0));
            else
                valueInstanced = Instantiate(valueGold, positionValue.position, Quaternion.identity);
        }
    }
}
