using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string soundEffect = null;
    public string cross = null;
    public virtual Interactable Interact(Transform interactor)
    {
        return null;
    }
    public virtual Interactable Uninteract() 
    {
        return null;
    }


}