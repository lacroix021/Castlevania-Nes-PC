using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSubDopple : MonoBehaviour
{
    public GameObject shadow;
    private float shadowTimeCD;
    public float shadowTime;
    float z = 0;
    public Color shadowColor;

    public float lifeTime;
    
    public enum typeSub
    {
        knifeDoppleganger,
        AxeDoppleganger,
        bottleDoppleganger,
        crossDoppleganger
    }

    public typeSub TypeSup;

    // Start is called before the first frame update
    void Start()
    {
        shadowTimeCD = shadowTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (TypeSup == typeSub.AxeDoppleganger || TypeSup == typeSub.crossDoppleganger)
        {
            z += 100;
        }
        else
        {
            z = 0;
        }

        if (shadowTimeCD > 0)
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
        currentShadow.GetComponent<SpriteRenderer>().color = shadowColor;

        Destroy(currentShadow, lifeTime);
    }
}
