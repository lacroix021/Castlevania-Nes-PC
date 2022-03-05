using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCollStage4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(18, 9, true);
    }
}
