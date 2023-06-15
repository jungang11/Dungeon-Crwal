using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdapter : MonoBehaviour, IInteractable
{
    public UnityEvent takeAction; 
    public void Interact()
    {
        takeAction?.Invoke(); 
    }
}
