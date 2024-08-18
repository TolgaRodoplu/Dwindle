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

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.instance.activateCross += ActivateCross;
        EventSystem.instance.pressEtoGrab += SetPressEGrab;
        EventSystem.instance.pressEtoRelease += SetPressERelease;
        EventSystem.instance.holdRtoRotate += SetHoldRRotate;
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
}
