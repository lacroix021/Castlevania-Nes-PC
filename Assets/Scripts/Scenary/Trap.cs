using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float normalSpeed;
    public float slowSpeed;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimSpeedNormal()
    {
        anim.speed = normalSpeed;
    }

    public void AnimSpeedSlow()
    {
        anim.speed = slowSpeed;
    }
}
