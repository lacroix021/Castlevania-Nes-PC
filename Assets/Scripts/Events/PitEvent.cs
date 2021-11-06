using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitEvent : MonoBehaviour
{
    public bool pitEnable;

    public GameObject coverPit;
    public GameObject coverPlatform;
    public GameObject chain;

    EventManager eventManager;

    private void Awake()
    {
        eventManager = GameObject.Find("EventManager").GetComponentInChildren<EventManager>();
    }

    private void Start()
    {
        PitEnable();
    }

    public void PitEnable()
    {
        if (pitEnable)
        {
            coverPit.SetActive(false);
            coverPlatform.SetActive(false);
            chain.SetActive(true);
            eventManager.CheckEvents();
        }
    }
}
