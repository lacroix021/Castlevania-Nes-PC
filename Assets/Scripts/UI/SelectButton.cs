using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Button quitButton;
    public Button navButton;
    

    private void Start()
    {
        StartCoroutine(Select());
    }

    IEnumerator Select()
    {
        yield return new WaitForSeconds(0.5f);
        navButton.Select();
    }
}


