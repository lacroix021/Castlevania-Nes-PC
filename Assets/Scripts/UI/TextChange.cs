using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    public Text text;
    public HealthBoss currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        text.text = currentHealth.currentHealth.ToString("F0");
    }
}
