using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject shadow;
    private float shadowTimeCD;
    public float shadowTime;
    float z = 0;

    // Start is called before the first frame update
    void Start()
    {
        shadowTimeCD = shadowTime;
    }

    // Update is called once per frame
    void Update()
    {
        z += 100;

        if(shadowTimeCD > 0)
        {
            shadowTimeCD -= Time.deltaTime;
        }
        else
        {
            InstantiateShadow();
            shadowTimeCD = shadowTime;
        }
    }
    public void InstantiateShadow()
    {
        GameObject currentShadow = Instantiate(shadow, transform.position, Quaternion.Euler(0, 0, z));
        currentShadow.transform.localScale = transform.localScale;
        currentShadow.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
        currentShadow.GetComponent<SpriteRenderer>().color = new Vector4(10, 0, 10, 0.3f);
        Destroy(currentShadow, 0.568f);
    }
}
