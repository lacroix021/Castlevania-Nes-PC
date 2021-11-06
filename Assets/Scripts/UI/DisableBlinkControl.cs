using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBlinkControl : MonoBehaviour
{
    public void DisableBlink()
    {
        this.gameObject.SetActive(false);
    }
}
