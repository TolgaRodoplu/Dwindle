using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Interactable interactObject = other.GetComponent<Interactable>();

        if (interactObject != null)
        {
            interactObject.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactObject = other.GetComponent<Interactable>();

        

        if (interactObject != null)
        {
            interactObject.enabled = false;
        }

    }


}