using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsStetic : MonoBehaviour
{

    public GameObject fireDecoL;
    public GameObject fireDecoR;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            fireDecoL.SetActive(true);
            fireDecoR.SetActive(true);
        }
        else
        {
            fireDecoL.SetActive(false);
            fireDecoR.SetActive(false);
            fireDecoL.GetComponent<Image>().color = Color.white;
            fireDecoR.GetComponent<Image>().color = Color.white;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
        {
            fireDecoL.SetActive(true);
            fireDecoR.SetActive(true);
            fireDecoL.GetComponent<Image>().color = Color.red;
            fireDecoR.GetComponent<Image>().color = Color.red;
        }
    }
}
