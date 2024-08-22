using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject pressEtoGrab;
    public GameObject pressEtoRelease;
    public GameObject holdRtoRotate;
    public GameObject outro;
    public GameObject settings;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.instance.activateCross += ActivateCross;
        EventSystem.instance.pressEtoGrab += SetPressEGrab;
        EventSystem.instance.pressEtoRelease += SetPressERelease;
        EventSystem.instance.holdRtoRotate += SetHoldRRotate;
        EventSystem.instance.gameEnded += EnableOutro;
        EventSystem.instance.settingsOpened += EnableSettings;
    }

    private void SetPressERelease(object sender, bool e)
    {
        if (pressEtoRelease != null)
        {
            pressEtoRelease.SetActive(e);
        }
    }

    private void SetHoldRRotate(object sender, bool e)
    {
        if (holdRtoRotate != null)
        {
            holdRtoRotate.SetActive(e);
        }
    }

    private void SetPressEGrab(object sender, bool e)
    {
        if (pressEtoGrab != null)
        {
            pressEtoGrab.SetActive(e);
        }
    }

    private void ActivateCross(object sender, bool e)
    {
        if (crosshair != null) 
        {
            crosshair.SetActive(e);
        }
    }

    private void EnableOutro()
    { 
        outro.SetActive(true); 
    }

    private void EnableSettings()
    {
        settings.active = true;
    }
}
