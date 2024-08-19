using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloasetDoors : MonoBehaviour
{
    bool isDisplaced = false;
    public Vector3 originalPos = Vector3.zero;
    public Vector3 originalRot = Vector3.zero;
    public Vector3 displacedPos = Vector3.zero;
    public Vector3 displacedRot = Vector3.zero;
    private void Start()
    {
        EventSystem.instance.passedPoint += SwitchDoors;
    }

    void SwitchDoors()
    {
        if (!isDisplaced)
        {
            transform.localPosition = displacedPos;
            transform.rotation = Quaternion.Euler(displacedRot);
        }
        else
        {
            transform.localPosition = originalPos;
            transform.rotation = Quaternion.Euler(originalRot);
        }

        isDisplaced = !isDisplaced;
    }   
}
