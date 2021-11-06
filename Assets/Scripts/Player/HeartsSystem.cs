using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsSystem : MonoBehaviour
{

    public int MaxHearts;
    public int currentHearts;

    

    public void CheckHearts()
    {
        currentHearts = Mathf.Clamp(currentHearts, 0, MaxHearts);
    } 
}
