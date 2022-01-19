using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBorrableB : MonoBehaviour
{
    scriptBorrable scriptA;

    // Start is called before the first frame update
    void Start()
    {
        scriptA = GameObject.FindObjectOfType<scriptBorrable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scriptA.myColor == scriptBorrable.colores.azul)
        {
            print("el color es: " + scriptBorrable.colores.azul);
        }
        else if (scriptA.myColor == scriptBorrable.colores.rojo)
        {
            print("el color es: " + scriptBorrable.colores.rojo);
        }
        else if (scriptA.myColor == scriptBorrable.colores.negro)
        {
            print("el color es: " + scriptBorrable.colores.negro);
        }
    }
}
