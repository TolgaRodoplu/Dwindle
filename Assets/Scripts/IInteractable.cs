using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public Interactable Interact(Transform interactor);
    public Interactable Uninteract();
}
public enum InteractionType
{
    Grab
}