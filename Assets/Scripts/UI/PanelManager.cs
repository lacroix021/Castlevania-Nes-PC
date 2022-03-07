using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Button btnToStartSelect;
    SimonController player;
    public GameObject thisPanel;

    
    private void OnEnable()
    {
        btnToStartSelect.Select();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Input();
    }

    void Input()
    {
        if (player.cancelInput && thisPanel.activeSelf)
        {
            GameManager.gameManager.teleportMenu = false;
            this.gameObject.SetActive(false);
        }
    }
}
