using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fridge : Interactable
{
    [SerializeField] private BoolObject paused;
    [SerializeField] private UnityEvent onInteract;

    public override void Interact()
    {
      if(!paused.value)
      {
        onInteract.Invoke();
      }
    }
}
